namespace NerdStore.WebApp.MVC.Controllers.Admin;

public class AdminProductsController : Controller
{
    private readonly IProductAppService _productAppService;

    public AdminProductsController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    [Route("admin-products")]
    public async Task<IActionResult> Index() =>
        View(await _productAppService.GetAll());

    [Route("new-product")]
    public async Task<IActionResult> NewProduct() => 
        View(await PopularCategories(new ProductViewModel()));

    [HttpPost]
    [Route("new-product")]
    public async Task<IActionResult> NewProduct(ProductViewModel productViewModel)
    {
        if (!ModelState.IsValid) 
            return View(await PopularCategories(productViewModel));

        await _productAppService.AddProduct(productViewModel);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("edit-product")]
    public async Task<IActionResult> UpdateProduct(Guid id) =>
        View(await PopularCategories(await _productAppService.GetById(id)));

    [HttpPost]
    [Route("edit-product")]
    public async Task<IActionResult> UpdateProduct(Guid id, ProductViewModel productViewModel)
    {
        var produto = await _productAppService.GetById(id);
        productViewModel.Stock_Quantity = produto.Stock_Quantity;

        ModelState.Remove("Stock_Quantity");

        if (!ModelState.IsValid) 
            return View(await PopularCategories(productViewModel));

        await _productAppService.UpdateProduct(productViewModel);

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("products-update-stock")]
    public async Task<IActionResult> UpdateStock(Guid id) =>
        View("Stock", await _productAppService.GetById(id));

    [HttpPost]
    [Route("products-update-stock")]
    public async Task<IActionResult> UpdateStock(Guid id, int quantity)
    {
        if (quantity > 0)
        {
            await _productAppService.ReplenishStock(id, quantity);
        }
        else
        {
            await _productAppService.DebitStock(id, quantity);
        }

        return View("Index", await _productAppService.GetAll());
    }


    private async Task<ProductViewModel> PopularCategories(ProductViewModel product)
    {
        product.Categories = await _productAppService.GetCategories();
        return product;
    }
}
