namespace NerdStore.Sales.Application.Commands.Order;

public class AddItemOrderCommand : Command
{
    public Guid ClientId { get; private set; }
    public Guid ProductId { get; private set; }
    public string Name { get; private set; }
    public int Quantity { get; private set; }
    public decimal UnitaryValue { get; private set; }

    public AddItemOrderCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitaryValue)
    {
        ClientId = clientId;
        ProductId = productId;
        Name = name;
        Quantity = quantity;
        UnitaryValue = unitaryValue;
    }

    public override bool IsValid()
    {
        ValidationResult = new AddItemOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class AddItemOrderValidation : AbstractValidator<AddItemOrderCommand>
{
    public AddItemOrderValidation()
    {
        RuleFor(c => c.ClientId)
                .NotEqual(Guid.Empty)
                .WithMessage("Invalid customer id.");

        RuleFor(c => c.ProductId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid product id.");

        RuleFor(c => c.Name)
            .NotEmpty()
            .WithMessage("The product name was not provided.");

        RuleFor(c => c.Quantity)
            .GreaterThan(0)
            .WithMessage("The minimum quantity of an item is 1.");

        RuleFor(c => c.Quantity)
            .LessThan(15)
            .WithMessage("The maximum quantity of an item is 15.");

        RuleFor(c => c.UnitaryValue)
            .GreaterThan(0)
            .WithMessage("The item value must be greater than 0.");
    }
}
