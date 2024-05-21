namespace NerdStore.Sales.Application.ViewModels;

public class OrderViewModel
{
    public Guid Id { get; set; }
    public int Code { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public int OrderStatus { get; set; }
}
