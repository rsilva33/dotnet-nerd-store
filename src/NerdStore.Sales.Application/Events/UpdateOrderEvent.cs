namespace NerdStore.Sales.Application.Events;

public class UpdateOrderEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid OrderId { get; private set; }
    public decimal Amount { get; private set; }

    public UpdateOrderEvent(Guid clientId, Guid orderId, decimal amount)
    {
        AggregateId = orderId;
        ClientId = clientId;
        OrderId = orderId;
        Amount = amount;
    }
}
