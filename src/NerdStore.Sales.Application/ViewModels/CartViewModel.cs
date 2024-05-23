namespace NerdStore.Sales.Application.ViewModels;

public class CartViewModel
{
    public Guid OrderId { get; set; }
    public Guid ClientId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Amount { get; set; }
    public decimal DiscountValue { get; set; }
    public string VoucherCode { get; set; } = string.Empty;

    public List<CartItemViewModel> Items { get; set; } = [];
    public CartPaymentViewModel? Payment { get; set; }
}
