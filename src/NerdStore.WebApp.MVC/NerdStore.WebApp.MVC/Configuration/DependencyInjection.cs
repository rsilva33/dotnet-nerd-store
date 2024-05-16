using NerdStore.Sales.Application.Commands.Order;
using NerdStore.Sales.Data;
using NerdStore.Sales.Data.Repository;
using NerdStore.Sales.Domain.Abstractions;

namespace NerdStore.WebApp.MVC.Configuration;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //Domain Bus (mediatork)
        services.AddScoped<IMediatorHandler, MediatorHandler>();

        //Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<CatalogContext>();

        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();

        //Sales
        services.AddScoped<SalesContext>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IRequestHandler<AddItemOrderCommand, bool>, OrderCommandHandler>();
    }
}
