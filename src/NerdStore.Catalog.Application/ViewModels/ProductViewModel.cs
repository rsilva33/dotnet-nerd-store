namespace NerdStore.Catalog.Application.ViewModels;

public class ProductViewModel
{
    [Key]
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public string Image { get; set; } = string.Empty;

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public bool Active { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public decimal Value { get; set; }

    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public DateTime Created_At { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}..")]
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public int Stock_Quantity { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}..")]
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public decimal Height { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}..")]
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public decimal Width { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Field {0} must have a minimum value of {1}..")]
    [Required(ErrorMessage = "Field {0} is mandatory.")]
    public decimal Depth { get; set; }

    public IEnumerable<CategoryViewModel>? Categories{ get; set; }
}
