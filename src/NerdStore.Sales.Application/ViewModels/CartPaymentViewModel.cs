namespace NerdStore.Sales.Application.ViewModels;

public class CartPaymentViewModel
{
    public string NameCard { get; set; } = string.Empty;
    public string CardNumber { get; set; } = string.Empty;  
    public string ExpirationCard { get; set; } = string.Empty;
    public string CvvCard { get; set; } = string.Empty;
}
