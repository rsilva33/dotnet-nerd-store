﻿namespace NerdStore.Catalog.Application.ViewModels;

public class CategoryViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string Name { get; private set; } = string.Empty;

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public int Code { get; private set; }
}