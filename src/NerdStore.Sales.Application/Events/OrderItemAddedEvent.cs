namespace NerdStore.Sales.Application.Events;

public class OrderItemAddedEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public decimal UnitaryValue { get; set; }
    public int Quantity { get; set; }

    public OrderItemAddedEvent(Guid clientId, Guid orderId, Guid productId, string name, decimal unitaryValue, int quantity)
    {
        AggregateId = orderId;
        ClientId = clientId;
        OrderId = orderId;
        Name = name;
        ProductId = productId;
        UnitaryValue = unitaryValue;
        Quantity = quantity;
    }
}
