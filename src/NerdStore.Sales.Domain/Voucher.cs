namespace NerdStore.Sales.Domain;

public class Voucher : Entity
{
    public string Code { get; private set; } = string.Empty;
    public decimal? Percentage { get; private set; }
    public decimal? DiscountValue { get; private set; }
    public int Quantity { get; private set; }
    public VoucherDiscountType VoucherDiscountType { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UseDate { get; private set; }
    public DateTime ExpiryDate { get; private set; }
    public bool Avtive { get; private set; }
    public bool Used { get; private set; }

    //EF Rel.
    public ICollection<Order> Orders { get; set; }
}
