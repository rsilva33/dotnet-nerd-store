using NerdStore.Sales.Application.Commands.Order;
using NerdStore.Sales.Application.Queries.Order;

namespace NerdStore.WebApp.MVC.Controllers;

public class CartController : ControllerBase
{
    private readonly IProductAppService _productAppService;
    private readonly IOrderQueries _orderQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public CartController(
                              INotificationHandler<DomainNotification> notifications,
                              IProductAppService productAppService,
                              IMediatorHandler mediatorHandler,
                              IOrderQueries orderQueries
                              ) 
       : base(notifications, mediatorHandler)
    {
        _productAppService = productAppService;
        _mediatorHandler = mediatorHandler;
        _orderQueries = orderQueries;
    }

    [Route("my-cart")]
    public async Task<IActionResult> Index() =>
        View(await _orderQueries.GetClientCart(ClientId));

    [HttpPost]
    [Route("my-cart")]
    public async Task<IActionResult> AddItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetById(id);
        if (product is null) 
            return BadRequest();

        if (product.StockQuantity < quantity)
        {
            TempData["Error"] = "Product out of stock";
            return RedirectToAction("ProductDetail", "Showcase", new { id });
        }

        var command = new AddItemOrderCommand(ClientId, product.Id, product.Name, quantity, product.Value);
        await _mediatorHandler.SendCommand(command);

        if (ValidatedOperation())
            return RedirectToAction("Index");

        TempData["Errors"] = GetMessagesError();

        return RedirectToAction("ProductDetail", "Showcase", new { id });
    }

    [HttpPost]
    [Route("remove-item")]
    public async Task<IActionResult> RemoveItem(Guid id)
    {
        var product = await _productAppService.GetById(id);
        if (product == null) return BadRequest();

        var command = new RemoveItemOrderCommand(ClientId, id);
        await _mediatorHandler.SendCommand(command);

        if (ValidatedOperation())
        {
            return RedirectToAction("Index");
        }

        return View("Index", await _orderQueries.GetClientCart(ClientId));
    }

    [HttpPost]
    [Route("update-item")]
    public async Task<IActionResult> UpdateItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetById(id);
        if (product == null) return BadRequest();

        var command = new UpdateItemOrderCommand(ClientId, id, quantity);
        await _mediatorHandler.SendCommand(command);

        if (ValidatedOperation())
        {
            return RedirectToAction("Index");
        }

        return View("Index", await _orderQueries.GetClientCart(ClientId));
    }

    [HttpPost]
    [Route("apply-voucher")]
    public async Task<IActionResult> ApplyVoucher(string voucherCode)
    {
        var command = new ApplyVoucherOrderCommand(ClientId, voucherCode);
        await _mediatorHandler.SendCommand(command);

        if (ValidatedOperation())
        {
            return RedirectToAction("Index");
        }

        return View("Index", await _orderQueries.GetClientCart(ClientId));
    }

    //[Route("purchase-summary")]
    //public async Task<IActionResult> PurchaseSummary()
    //{
    //    return View(await _orderQueries.GetClientCart(ClientId));
    //}

    //[HttpPost]
    //[Route("start-order")]
    //public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
    //{
    //    var cart = await _orderQueries.GetClientCart(ClientId);

    //    var command = new StartOrderCommand(cart.OrderId, ClientId, cart.ValorTotal, cartViewModel.Pagamento.NomeCartao,
    //        cartViewModel.Pagamento.NumeroCartao, cartViewModel.Pagamento.ExpiracaoCartao, cartViewModel.Pagamento.CvvCartao);

    //    await _mediatorHandler.EnviarComando(command);

    //    if (ValidatedOperation())
    //    {
    //        return RedirectToAction("Index", "Order");
    //    }

    //    return View("ResumoDaCompra", await _orderQueries.ObterCarrinhoCliente(ClientId));
    //}
}
