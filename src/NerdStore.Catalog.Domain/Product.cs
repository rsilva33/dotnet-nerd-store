namespace NerdStore.Catalog.Domain;

public class Product : Entity, IAggreageteRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public bool Active { get; private set; }
    public decimal Value { get; private set; }
    public int Stock_Quantity { get; private set; }
    public DateTime Created_At { get; private set; }
    public Guid CategoryId { get; private set; }

    public Dimensions Dimensions { get; private set; }
    public Category Category { get; private set; }

    public Product(string name, string description, bool active, decimal value, string image, DateTime created_at,Guid categoryId, Dimensions dimensions)
    {
        Name = name;
        Description = description;
        Active = active;
        Value = value;
        Image = image;
        Created_At = created_at;
        CategoryId = categoryId;
        Dimensions = dimensions;

        Validate();
    }

    public bool Activate() => Active = true;

    public bool Disable() => Active = false;

    public void ChangeDescription(string description)
    {
        AssertionConcern.ValidateIfEmpty(description, "");
        Description = description;
    }

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void DebitStock(int quantity)
    {
        if (quantity < 0)
            quantity *= -1;

        if (HasStock(quantity))
            throw new DomainException("Insufficient stock.");

        Stock_Quantity -= quantity;
    }

    public void ReplenishStock(int quantity) =>
    Stock_Quantity += quantity;

    public bool HasStock(int quantity) =>
        Stock_Quantity >= quantity;

    public void Validate()
    {
        AssertionConcern.ValidateIfEmpty(Name, "The Product name field cannot be empty.");
        AssertionConcern.ValidateIfEmpty(Description, "The Product Description field cannot be empty.");
        AssertionConcern.ValidateIfDifferent(CategoryId, Guid.Empty, "The product CategoryId field cannot be empty.");
        AssertionConcern.ValidateIfSmallerEqualsMinimum(Value, 0, "The Product Value field cannot be less than 0.");
        AssertionConcern.ValidateIfEmpty(Image, "The Product Image field cannot be empty.");
    }
}
