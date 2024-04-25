namespace NerdStore.Catalog.Domain;

public class Product : Entity, IAggreageteRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public bool Active { get; private set; }
    public decimal Value { get; private set; }
    public string Image { get; private set; } = string.Empty;
    public int Stock_Quantity { get; private set; }
    public Guid CategoryId { get; private set; }

    public Category Category { get; private set; }

    public Product(string name, string description, bool active, decimal value, string image, Guid categoryId)
    {
        Name = name;
        Description = description;
        Active = active;
        Value = value;
        Image = image;
        CategoryId = categoryId;
    }

    public bool Activate() => Active = true;
    public bool Disable() => Active = false;

    public void ChangeDescription(string description) =>
        Description = description;

    public void ReplenishStock(int quantity) =>
        Stock_Quantity += quantity;

    public bool HasStock(int quantity) =>
        Stock_Quantity >= quantity;

    public void ChangeCategory(Category category)
    {
        Category = category;
        CategoryId = category.Id;
    }

    public void DebitStock(int quantity)
    {
        if (quantity < 0)
            quantity *= -1;

        Stock_Quantity -= quantity;
    }
}
