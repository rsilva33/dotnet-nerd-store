using MediatR;

namespace NerdStore.Sales.Domain;

public class OrderItem : Entity
{
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string ProductName { get; private set; } = string.Empty;
    public int Quantity { get; private set; }
    public decimal UnitaryValue  { get; private set; }        

    //EF Rel.
    public Order Order { get; set; }

    protected OrderItem() { }

    public OrderItem(Guid productId, string productName, int quantity, decimal unitaryValue)
    {
        ProductId = productId;
        ProductName = productName;
        Quantity = quantity;
        UnitaryValue = unitaryValue;
    }

    internal void AssociateOrder(Guid orderId) =>
        OrderId = orderId;

    public decimal CalculateValue() =>
        Quantity * UnitaryValue;

    internal void AddUnits(int units) =>
        Quantity += units;

    internal void UpdateUnits(int units) =>
        Quantity = units;

    public override bool IsValid() =>
        true;
}