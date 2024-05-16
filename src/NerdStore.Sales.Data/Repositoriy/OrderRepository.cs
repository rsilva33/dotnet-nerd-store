using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Domain;
using NerdStore.Core.Abstractions;
using NerdStore.Sales.Domain;
using NerdStore.Sales.Domain.Abstractions;
using NerdStore.Sales.Domain.Enums;

namespace NerdStore.Sales.Data.Repository;
public class OrderRepository : IOrderRepository
{
    private readonly SalesContext _context;

    public OrderRepository(SalesContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public void Add(Order order) =>
        _context.Orders.Add(order);

    public void AddItem(OrderItem orderItem) =>
        _context.OrderItems.Add(orderItem);


    public async Task<Order> GetById(Guid id) =>
        await _context.Orders.FindAsync(id);

    public async Task<OrderItem> GetItemById(Guid id) =>
        await _context.OrderItems.FindAsync(id);

    public async Task<OrderItem> GetItemByOrder(Guid orderId, Guid productId) =>
        await _context.OrderItems.FirstOrDefaultAsync(p => p.ProductId == productId && p.OrderId == orderId);

    public async Task<IEnumerable<Order>> GetListByClientId(Guid clientId) =>
        await _context.Orders.AsNoTracking().Where(p => p.ClientId == clientId).ToListAsync();

    public async Task<Order> GetOrderDraftByCustomerId(Guid clientId)
    {
        var pedido = await _context.Orders.FirstOrDefaultAsync(p => p.ClientId == clientId && p.OrderStatus == OrderStatus.Draft);

        if (pedido is null)
            return null;

        await _context.Entry(pedido)
            .Collection(i => i.OrderItems).LoadAsync();

        if (pedido.VoucherId != null)
        {
            await _context.Entry(pedido)
                .Reference(i => i.Voucher).LoadAsync();
        }

        return pedido;
    }

    public async Task<Voucher> GetVoucherByCode(string code) =>
        await _context.Vouchers.FirstOrDefaultAsync(p => p.Code == code);

    public void RemoveItem(OrderItem orderItem) =>
        _context.OrderItems.Remove(orderItem);

    public void Update(Order order) =>
        _context.Orders.Update(order);

    public void UpdateItem(OrderItem orderItem) =>
         _context.OrderItems.Update(orderItem);

    public void Dispose() =>
        _context.Dispose();

}
