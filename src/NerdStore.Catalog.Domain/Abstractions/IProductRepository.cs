using NerdStore.Core.Abstractions.Data;

namespace NerdStore.Catalog.Domain.Abstractions;
public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<IEnumerable<Product>> GetByCategory(int code);
    Task<IEnumerable<Category>> GetAllCategories();

    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(Product product);

    void AddCategory(Category category);
    void UpdateCategory(Category category);
    void DeleteCategory(Category category);

    void Dispose();
}
