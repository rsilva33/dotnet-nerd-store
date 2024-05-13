using NerdStore.Catalog.Domain.Events;
using System.Reflection.Metadata;

namespace NerdStore.Catalog.Domain.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _productRepository;
    private readonly IMediatrHandler _bus;

    public StockService(IProductRepository productRepository, IMediatrHandler bus)
    {
        _productRepository = productRepository;
        _bus = bus;
    }

    public async Task<bool> DebitStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if(product is null)
            return false;

        if(!product.HasStock(quantity))
            return false;
        product.DebitStock(quantity);

        //TODO: Parameterize the stock quantity below
        if (product.StockQuantity < 10)
            await _bus.PublishEvent(new ProductBelowStockEvent(product.Id, product.StockQuantity));

        _productRepository.Update(product);

        return await _productRepository.UnitOfWork.CommitAsync(); 
    }

    public async Task<bool> ReplenishStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if(product is null)
            return false;

        product.ReplenishStock(quantity);

        _productRepository.Update(product);

        return await _productRepository.UnitOfWork.CommitAsync();
    }

    public void Dispose() => 
        _productRepository.Dispose();
}
