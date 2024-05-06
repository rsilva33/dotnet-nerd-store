namespace NerdStore.Catalog.Application.Abstractions;

public interface IProductAppService : IDisposable
{
    Task<IEnumerable<ProductViewModel>> GetByCategory(int code);
    Task<ProductViewModel> GetById(Guid id);
    Task<IEnumerable<ProductViewModel>> GetAll();
    Task<IEnumerable<CategoryViewModel>> GetCategories();

    Task AddProduct(ProductViewModel productViewModel);
    Task UpdateProduct(ProductViewModel productViewModel);

    Task<ProductViewModel> DebitStock(Guid id, int quantity);
    Task<ProductViewModel> ReplenishStock(Guid id, int quantity);
}
