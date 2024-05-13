namespace NerdStore.Sales.Domain;

public class Order : Entity, IAggreageteRoot
{
    public int Code { get; private set; }
    public Guid ClientId { get; private set; }
    public Guid? VoucherId { get; private set; }
    public bool VoucherUsed { get; private set; }
    public decimal Discount { get; private set; }
    public decimal Amount { get; private set; }
    public DateOnly CreatedAt { get; private set; }
    public OrderStatus OrderStatus { get; private set; }

    //NAO POSSO EXPOR A LISTA PARA QUE ELA SEJA MANIPOULADA POR OUTRA.
    private readonly List<OrderItem> _ordemItems;
    //LISTA EXTERNA SOMENTE LEITURA
    public IReadOnlyCollection<OrderItem> OrderItems => _ordemItems;

    //EF Relational
    public virtual Voucher Voucher { get; private set; }

    protected Order() =>
        _ordemItems = new List<OrderItem>();

    public Order(Guid clientId, bool voucherUsed, decimal discount, decimal amount)
    {
        ClientId = clientId;
        VoucherUsed = voucherUsed;
        Discount = discount;
        Amount = amount;
        _ordemItems = new List<OrderItem>();
    }

    public bool OrderExistingItem(OrderItem item) =>
        _ordemItems.Any(p => p.ProductId == item.ProductId);

    public void ApplyVoucher(Voucher voucher)
    {
        Voucher = voucher;
        VoucherUsed = true;
        CalculateOrderValue();
    }

    public void CalculateOrderValue()
    {
        Amount = OrderItems.Sum(p => p.CalculateValue());
        CalculateTotalDiscountValue();
    }

    public void CalculateTotalDiscountValue()
    {
        if (!VoucherUsed) return;

        decimal discount = 0;
        var value = Amount;

        if (Voucher.VoucherDiscountType is VoucherDiscountType.Porcentage)
        {
            if(Voucher.Percentage.HasValue)
            {
                discount = (value * Voucher.Percentage.Value) / 100;
                value -= discount;
            }
        }
        else
        {
            if (Voucher.Percentage.HasValue)
            {
                discount = Voucher.Percentage.Value;
                value -= discount;
            }
        }

        Amount = value < 0 ? 0 : value;
        Discount = discount;
    }

    public void AddItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        item.AssociateOrder(Id);

        if (OrderExistingItem(item))
        {
            var itemExists = _ordemItems.FirstOrDefault(p => p.ProductId == item.ProductId);
            itemExists.AddUnits(item.Quantity);
            item = itemExists;

            _ordemItems.Remove(itemExists);
        }

        item.CalculateValue();
        _ordemItems.Add(item);

        CalculateOrderValue();
    }

    public void UpdateItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        item.AssociateOrder(Id);

        var itemExists = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

        if (itemExists is null)
            throw new DomainException("The item does not belong to the order.");

        _ordemItems.Remove(itemExists);
        _ordemItems.Add(item);

        CalculateOrderValue();
    }
    
    public void RemoveItem(OrderItem item)
    {
        if (!item.IsValid()) return;

        var itemExists = OrderItems.FirstOrDefault(p => p.ProductId == item.ProductId);

        if(itemExists is null)
            throw new DomainException("The item does not belong to the order.");

        _ordemItems.Remove(itemExists);

        CalculateOrderValue();
    }

    public void UpdateUnits(OrderItem item, int units)
    {
        item.UpdateUnits(units);
        UpdateItem(item);
    }

    public void MakeDraft() =>
        OrderStatus = OrderStatus.Draft;

    public void StartOrder() =>
        OrderStatus = OrderStatus.Started;

    public void FinalizeOrder() =>
        OrderStatus = OrderStatus.Paid;

    public void CancelOrder() =>
        OrderStatus = OrderStatus.Canceled;

    //Classe aninhada
    public static class OrderFactory
    {
        public static Order NewOrderDraft(Guid clientId)
        {
            var order = new Order
            {
                ClientId = clientId
            };

            order.MakeDraft();
            return order;
        }
    }
}
