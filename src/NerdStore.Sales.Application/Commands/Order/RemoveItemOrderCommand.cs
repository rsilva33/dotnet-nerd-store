namespace NerdStore.Sales.Application.Commands.Order;

public class RemoveItemOrderCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }

    public RemoveItemOrderCommand(Guid clientId, Guid productId)
    {
        ClientId = clientId;
        ProductId = productId;
    }

    public override bool IsValid()
    {
        ValidationResult = new RemoveItemOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class RemoveItemOrderValidation : AbstractValidator<RemoveItemOrderCommand>
{
    public RemoveItemOrderValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client Id.");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product Id.");
    }
}