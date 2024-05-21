namespace NerdStore.Sales.Application.Events;

public class DraftOrderStartedEvent : Event
{
    public Guid ClientId { get; private set; }
    public Guid OrderId { get; private set; }

    public DraftOrderStartedEvent(Guid clientId, Guid orderId)
    {
        AggregateId = orderId;
        ClientId = clientId;
        OrderId = orderId;
    }
}