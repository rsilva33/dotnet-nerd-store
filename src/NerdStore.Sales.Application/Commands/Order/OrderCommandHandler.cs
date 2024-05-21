using NerdStore.Core.Abstractions;
using NerdStore.Core.Messages.CommomMessages.Notifications;
using NerdStore.Sales.Application.Events;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Abstractions;

namespace NerdStore.Sales.Application.Commands.Order;

public class OrderCommandHandler : IRequestHandler<AddItemOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;
    private readonly IMediatorHandler _mediatorHandler;
        
    public OrderCommandHandler(IOrderRepository orderRepository, IMediatorHandler mediatorHandler)
    {
        _orderRepository = orderRepository;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<bool> Handle(AddItemOrderCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message))
            return false;

        var order = await _orderRepository.GetOrderDraftByCustomerId(message.ClientId);
        var orderItem = new OrderItem(message.ProductId, message.ProductName, message.Quantity, message.UnitaryValue);

        if(order is null)
        {
            order = Domain.Order.OrderFactory.NewOrderDraft(message.ClientId);
            order.AddItem(orderItem);

            _orderRepository.Add(order);
            order.AddEvent(new DraftOrderStartedEvent(message.ClientId, message.ProductId));
        }
        else
        {
            var pedidoItemExistente = order.OrderExistingItem(orderItem);
            order.AddItem(orderItem);

            if (pedidoItemExistente)
            {
                _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(p => p.ProductId == orderItem.ProductId));
            }
            else
            {
                _orderRepository.AddItem(orderItem);
            }

            order.AddEvent(new UpdateOrderEvent(order.ClientId, order.Id, order.Amount));
        }

        order.AddEvent(new OrderItemAddedEvent(order.ClientId, order.Id, message.ProductId, message.ProductName, message.UnitaryValue, message.Quantity));
        
        return await _orderRepository.UnitOfWork.CommitAsync();
    }

    private bool ValidateCommand(Command message)
    {
        if (message.IsValid())
            return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            _mediatorHandler.PublishNotification(new DomainNotification(message.MessageType, error.ErrorMessage));
        }

        return false;
    }
}
