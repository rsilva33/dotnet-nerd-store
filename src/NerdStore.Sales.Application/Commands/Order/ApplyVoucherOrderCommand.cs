namespace NerdStore.Sales.Application.Commands.Order;

public class ApplyVoucherOrderCommand : Command
{
    public Guid ClientId { get; private set; }
    public string CodeVoucher { get; private set; }

    public ApplyVoucherOrderCommand(Guid clienteId, string codigoVoucher)
    {
        ClientId = clienteId;
        CodeVoucher = codigoVoucher;
    }

    public override bool IsValid()
    {
        ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
        return ValidationResult.IsValid;
    }
}

public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
{
    public ApplyVoucherOrderValidation()
    {
        RuleFor(c => c.ClientId)
            .NotEqual(Guid.Empty)
            .WithMessage("Invalid client Id.");

        RuleFor(c => c.CodeVoucher)
        .NotEmpty()
            .WithMessage("The voucher code cannot be empty.");
    }
}