namespace NerdStore.Catalog.Domain.Events;

public class ProductEventHandler : INotificationHandler<ProductBelowStockEvent>
{
    private readonly IProductRepository _productRepository;

    public ProductEventHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task Handle(ProductBelowStockEvent message, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetById(message.AggregateId);

        //Enviar um email para aquisicao de mais estoque.
    }
}
