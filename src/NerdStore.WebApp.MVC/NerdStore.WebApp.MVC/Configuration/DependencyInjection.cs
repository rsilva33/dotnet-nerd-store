namespace NerdStore.WebApp.MVC.Configuration;

public static class DependencyInjection
{
    public static void RegisterServices(this IServiceCollection services)
    {
        //Domain Bus (mediatork)
        services.AddScoped<IMediatrHandler, MediatrHandler>();

        //Catalog
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IProductAppService, ProductAppService>();
        services.AddScoped<IStockService, StockService>();
        services.AddScoped<CatalogContext>();

        services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
    }
}
