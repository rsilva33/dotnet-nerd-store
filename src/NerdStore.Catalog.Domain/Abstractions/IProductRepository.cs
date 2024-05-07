namespace NerdStore.Catalog.Domain.Abstractions;

public interface IProductRepository : IRepository<Product>
{
    Task<IEnumerable<Product>> GetAll();
    Task<Product> GetById(Guid id);
    Task<IEnumerable<Product>> GetByCategory(int code);
    Task<IEnumerable<Category>> GetAllCategories();

    void Add(Product product);
    void Update(Product product);
    void Delete(Product product);

    void Add(Category category);
    void Update(Category category);
    void Delete(Category category);
}
