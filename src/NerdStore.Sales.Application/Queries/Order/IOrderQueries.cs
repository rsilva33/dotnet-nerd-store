using NerdStore.Sales.Application.ViewModels;

namespace NerdStore.Sales.Application.Queries.Order;

public interface IOrderQueries
{
    Task<CartViewModel> GetClientCart(Guid clientId);
    Task<IEnumerable<OrderViewModel>> GetClientOrder(Guid clientId);
}
