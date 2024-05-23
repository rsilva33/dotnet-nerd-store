using NerdStore.Sales.Application.ViewModels;
using NerdStore.Sales.Domain.Abstractions;
using NerdStore.Sales.Domain.Enums;

namespace NerdStore.Sales.Application.Queries.Order;

public class OrderQueries : IOrderQueries
{
    private readonly IOrderRepository _orderRepository;

    public OrderQueries(IOrderRepository orderRepository) =>
        _orderRepository = orderRepository;

    public async Task<CartViewModel?> GetClientCart(Guid clientId)
    {
        var order = await _orderRepository.GetOrderDraftByCustomerId(clientId);

        if (order is null)
            return null;

        var cart = new CartViewModel
        {
            ClientId = order.ClientId,
            Amount = order.Amount,
            OrderId = order.Id,
            DiscountValue = order.Discount,
            Subtotal = order.Discount + order.Amount
        };

        if (order.VoucherId != null)
        {
            cart.VoucherCode = order.Voucher.Code;
        }

        foreach (var item in order.OrderItems)
        {
            cart.Items.Add(new CartItemViewModel
            {
                ProductId = item.ProductId,
                ProductName = item.ProductName,
                Quantity = item.Quantity,
                UnitaryValue = item.UnitaryValue,
                Amount = item.UnitaryValue * item.Quantity
            });
        }

        return cart;
    }

    public async Task<IEnumerable<OrderViewModel>?> GetClientOrder(Guid clientId)
    {
        var orders = await _orderRepository.GetListByClientId(clientId);

        orders = orders.Where(p => p.OrderStatus == OrderStatus.Paid || p.OrderStatus == OrderStatus.Canceled)
            .OrderByDescending(p => p.Code);

        if (!orders.Any()) 
            return null;

        var ordersView = new List<OrderViewModel>();

        foreach (var order in orders)
        {
            ordersView.Add(new OrderViewModel
            {
                Id = order.Id,
                Amount = order.Amount,
                OrderStatus = (int)order.OrderStatus,
                Code = order.Code,
                CreatedAt = order.CreatedAt
            });
        }

        return ordersView;
    }
}
