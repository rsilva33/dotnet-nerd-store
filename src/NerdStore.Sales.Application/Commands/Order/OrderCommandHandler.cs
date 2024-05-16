using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Abstractions;

namespace NerdStore.Sales.Application.Commands.Order;

public class OrderCommandHandler : IRequestHandler<AddItemOrderCommand, bool>
{
    private readonly IOrderRepository _orderRepository;

    public OrderCommandHandler(IOrderRepository orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<bool> Handle(AddItemOrderCommand message, CancellationToken cancellationToken)
    {
        if (!ValidateCommand(message))
            return false;

        var order = await _orderRepository.GetOrderDraftByCustomerId(message.ClientId);
        var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitaryValue);

        if(order is null)
        {
            order = Domain.Order.OrderFactory.NewOrderDraft(message.ClientId);
            order.AddItem(orderItem);

            _orderRepository.Add(order);
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
        }

        //order.AdicionarEvento(new PedidoItemAdicionadoEvent(order.ClientId, order.Id, message.ProductId, message.Nome, message.ValorUnitario, message.Quantidade));
        return await _orderRepository.UnitOfWork.CommitAsync();
    }

    private bool ValidateCommand(Command message)
    {
        if (message.IsValid())
            return true;

        foreach (var error in message.ValidationResult.Errors)
        {
            //lanca um evento de erro
        }

        return false;
    }
}
