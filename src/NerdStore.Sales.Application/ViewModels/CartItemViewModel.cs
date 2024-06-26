﻿namespace NerdStore.Sales.Application.ViewModels;

public class CartItemViewModel
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitaryValue { get; set; }
    public decimal Amount { get; set; }
}
