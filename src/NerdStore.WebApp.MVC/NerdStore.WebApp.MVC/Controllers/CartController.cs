using NerdStore.Sales.Application.Commands.Order;

namespace NerdStore.WebApp.MVC.Controllers;
public class CartController : ControllerBase
{
    private readonly IProductAppService _productAppService;
    //private readonly IPedidoQueries _orderQueries;
    private readonly IMediatorHandler _mediatorHandler;

    public CartController(
                              //INotificationHandler<DomainNotification> notifications,
                              IProductAppService productAppService,
                              IMediatorHandler mediatorHandler
                              //IPedidoQueries pedidoQueries
                              ) 
       //: base(notifications, mediatorHandler)
    {
        _productAppService = productAppService;
        _mediatorHandler = mediatorHandler;
        //_orderQueries = pedidoQueries;
    }

    //[Route("my-cart")]
    //public async Task<IActionResult> Index()
    //{
    //    return View(await _orderQueries.ObterCarrinhoCliente(ClientId));
    //}

    [HttpPost]
    [Route("my-cart")]
    public async Task<IActionResult> AddItem(Guid id, int quantity)
    {
        var product = await _productAppService.GetById(id);
        if (product == null) return BadRequest();

        if (product.StockQuantity < quantity)
        {
            TempData["Erro"] = "Produto com estoque insuficiente";
            return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
        }

        var command = new AddItemOrderCommand(ClientId, product.Id, product.Name, quantity, product.Value);
        await _mediatorHandler.SendCommand(command);

        //if (ValidatedOperation())
        //{
        //    return RedirectToAction("Index");
        //}

        //TempData["Erros"] = ObterMensagensErro();
        return RedirectToAction("ProdutoDetalhe", "Vitrine", new { id });
    }

    //[HttpPost]
    //[Route("remove-item")]
    //public async Task<IActionResult> RemoveItem(Guid id)
    //{
    //    var product = await _productAppService.GetById(id);
    //    if (product == null) return BadRequest();

    //    var command = new RemoverItemPedidoCommand(ClientId, id);
    //    await _mediatorHandler.EnviarComando(command);

    //    if (ValidatedOperation())
    //    {
    //        return RedirectToAction("Index");
    //    }

    //    return View("Index", await _orderQueries.ObterCarrinhoCliente(ClienteId));
    //}

    //[HttpPost]
    //[Route("update-item")]
    //public async Task<IActionResult> UpdateItem(Guid id, int quantity)
    //{
    //    var product = await _productAppService.ObterPorId(id);
    //    if (product == null) return BadRequest();

    //    var command = new AtualizarItemPedidoCommand(ClienteId, id, quantity);
    //    await _mediatorHandler.EnviarComando(command);

    //    if (ValidatedOperation())
    //    {
    //        return RedirectToAction("Index");
    //    }

    //    return View("Index", await _orderQueries.ObterCarrinhoCliente(ClientId));
    //}

    //[HttpPost]
    //[Route("apply-voucher")]
    //public async Task<IActionResult> ApplyVoucher(string voucherCode)
    //{
    //    var command = new AplicarVoucherPedidoCommand(ClientId, voucherCode);
    //    await _mediatorHandler.EnviarComando(command);

    //    if (ValidatedOperation())
    //    {
    //        return RedirectToAction("Index");
    //    }

    //    return View("Index", await _orderQueries.ObterCarrinhoCliente(ClientId));
    //}

    //[Route("purchase-summary")]
    //public async Task<IActionResult> PurchaseSummary()
    //{
    //    return View(await _orderQueries.ObterCarrinhoCliente(ClientId));
    //}

    //[HttpPost]
    //[Route("start-order")]
    //public async Task<IActionResult> StartOrder(CartViewModel cartViewModel)
    //{
    //    var cart = await _orderQueries.ObterCarrinhoCliente(ClientId);

    //    var command = new StartOrderCommand(cart.PedidoId, ClientId, cart.ValorTotal, cartViewModel.Pagamento.NomeCartao,
    //        cartViewModel.Pagamento.NumeroCartao, cartViewModel.Pagamento.ExpiracaoCartao, cartViewModel.Pagamento.CvvCartao);

    //    await _mediatorHandler.EnviarComando(command);

    //    if (ValidatedOperation())
    //    {
    //        return RedirectToAction("Index", "Pedido");
    //    }

    //    return View("ResumoDaCompra", await _orderQueries.ObterCarrinhoCliente(ClientId));
    //}
}
