
namespace NerdStore.Sales.Application.Events;

public class OrderHandlerEvent :
    INotificationHandler<DraftOrderStartedEvent>,
    INotificationHandler<OrderItemAddedEvent>,
    INotificationHandler<UpdateOrderEvent>
{
    public Task Handle(DraftOrderStartedEvent notification, CancellationToken cancellationToken) =>
        Task.CompletedTask;

    public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken) =>
        Task.CompletedTask;


    public Task Handle(UpdateOrderEvent notification, CancellationToken cancellationToken) => 
        Task.CompletedTask;
}
