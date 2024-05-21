﻿namespace NerdStore.Sales.Application.ViewModels;

public class CartViewModel
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal Subtotal { get; set; }
    public decimal Amount { get; set; }
    public decimal DiscountValue { get; set; }
    public string VoucherCode { get; set; }

    public List<CartItemViewModel> Items { get; set; } = new List<CartItemViewModel>();
    public CartPaymentViewModel Payment { get; set; }
}
