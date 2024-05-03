namespace NerdStore.Catalog.Domain.Abstractions;

public interface IStockService : IDisposable
{
    Task<bool> DebitStock(Guid productId, int quantity);
    Task<bool> RepenishStock(Guid productId, int quantity);
}
