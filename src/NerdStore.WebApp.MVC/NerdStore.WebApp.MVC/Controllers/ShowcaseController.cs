namespace NerdStore.WebApp.MVC.Controllers;

public class ShowcaseController : Controller
{
    private readonly IProductAppService _productAppService;

    public ShowcaseController(IProductAppService productAppService)
    {
        _productAppService = productAppService;
    }

    [HttpGet]
    [Route("")]
    [Route("showcase")]
    public async Task<IActionResult> Index() =>
        View(await _productAppService.GetAll());

    [HttpGet]
    [Route("product-detail/{id}")]
    public async Task<IActionResult> ProductDetail(Guid id) =>
        View(await _productAppService.GetById(id));
}
