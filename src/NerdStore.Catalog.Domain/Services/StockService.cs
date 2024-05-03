namespace NerdStore.Catalog.Domain.Services;

public class StockService : IStockService
{
    private readonly IProductRepository _productRepository;

    public StockService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<bool> DebitStock(Guid productId, int quantity)
    {
        var product = await _productRepository.GetById(productId);

        if(product is null)
            return false;

        if(!product.HasStock(quantity))
            return false;

        product.DebitStock(quantity);

        _productRepository.Update(product);

        return await _productRepository.UnitOfWork.CommitAsync(); 
    }

    public async Task<bool> RepenishStock(Guid productId, int quantity)
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
