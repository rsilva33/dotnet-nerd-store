using NerdStore.Core.Abstractions;

namespace NerdStore.Catalog.Data.Repository;

internal class ProductRepository : IProductRepository
{
    public readonly CatalogContext _context;

    public ProductRepository(CatalogContext context)
    {
        _context = context;
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Product>> GetAll() =>
        await _context.Products.AsNoTracking().ToListAsync();

    public async Task<Product> GetById(Guid id) =>
        await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<IEnumerable<Category>> GetAllCategories() =>
        await _context.Categories.AsNoTracking().ToListAsync();

    public async Task<IEnumerable<Product>> GetByCategory(int code) =>
        await _context.Products.AsNoTracking().Include(p => p.Category).Where(c => c.Category.Code == code).ToListAsync();

    public void Add(Category category) =>
        _context.Categories.Add(category);

    public void Update(Category category) =>
        _context.Categories.Update(category);

    public void Delete(Category category)
    {
        throw new NotImplementedException();
    }

    public void Add(Product product) => 
        _context.Products.Add(product);

    public void Update(Product product) =>
        _context.Products.Update(product);

    public void Delete(Product product)
    {
        throw new NotImplementedException();
    }

    public void Dispose() => 
        _context?.Dispose();

}
