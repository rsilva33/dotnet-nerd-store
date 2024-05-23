using NerdStore.Sales.Application.Queries.Order;

namespace NerdStore.WebApp.MVC.Extensions;

public class CartViewComponent : ViewComponent
{
    private readonly IOrderQueries _orderQueries;

    // TODO: Obter cliente logado
    protected Guid ClienteId = Guid.Parse("4885e451-b0e4-4490-b959-04fabc806d32");


    public CartViewComponent(IOrderQueries orderQueries) =>
        _orderQueries = orderQueries;

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var cart = await _orderQueries.GetClientCart(ClienteId);
        var itens = cart?.Items.Count ?? 0;

        return View(itens);
    }
}
