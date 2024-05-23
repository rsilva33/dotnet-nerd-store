namespace NerdStore.Sales.Application.Commands.Order;

public class UpdateItemOrderCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }

    public UpdateItemOrderCommand(Guid clientId, Guid productId, int quantity)
    {
        ClientId = clientId;
        ProductId = productId;
        Quantity = quantity;
    }

    public override bool IsValid()
    {
        ValidationResult = new UpdateItemOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class UpdateItemOrderValidation : AbstractValidator<UpdateItemOrderCommand>
{
    public UpdateItemOrderValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client id.");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product id.");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage("The minimum quantity of an item is 1.");

        RuleFor(c => c.Quantity)
            .LessThan(15)
            .WithMessage("The maximum quantity of an item is 15.");
    }
}

