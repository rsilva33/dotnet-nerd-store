namespace NerdStore.Catalog.Application.Services;

public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly IStockService _stockService;
    private readonly IMapper _mapper;

    public ProductAppService(IProductRepository productRepository, IStockService stockService,IMapper mapper)
    {
        _productRepository = productRepository;
        _stockService = stockService;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductViewModel>> GetAll() =>
        _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetAll());

    public async Task<IEnumerable<ProductViewModel>> GetByCategory(int code) =>
        _mapper.Map<IEnumerable<ProductViewModel>>(await _productRepository.GetByCategory(code));

    public async Task<ProductViewModel> GetById(Guid id) => 
        _mapper.Map<ProductViewModel>(await  _productRepository.GetById(id));

    public async Task<IEnumerable<CategoryViewModel>> GetCategories() =>
        _mapper.Map<IEnumerable<CategoryViewModel>>(await _productRepository.GetAllCategories());

    public async Task AddProduct(ProductViewModel productViewModel)
    {
        var product = _mapper.Map<Product>(productViewModel);
        _productRepository.Add(product);

        await _productRepository.UnitOfWork.CommitAsync();
    }

    public async Task UpdateProduct(ProductViewModel productViewModel)
    {
        var product = _mapper.Map<Product>(productViewModel);
        _productRepository.Update(product);

        await _productRepository.UnitOfWork.CommitAsync();
    }

    public async Task<ProductViewModel> DebitStock(Guid id, int quantity)
    {
        if (!_stockService.DebitStock(id, quantity).Result)
            throw new DomainException("Failed to debit inventory.");

        return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
    }

    public async Task<ProductViewModel> ReplenishStock(Guid id, int quantity)
    {
        if (!_stockService.ReplenishStock(id, quantity).Result)
            throw new DomainException("Failed to restock inventory.");

        return _mapper.Map<ProductViewModel>(await _productRepository.GetById(id));
    }

    public void Dispose()
    {
        _productRepository?.Dispose();
        _stockService?.Dispose();
    }
}
