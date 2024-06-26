﻿

namespace NerdStore.Catalog.Domain;

public class Product : Entity, IAggreageteRoot
{
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public string Image { get; private set; } = string.Empty;
    public bool Active { get; private set; }
    public decimal Value { get; private set; }
    public int StockQuantity { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public Guid CategoryId { get; private set; }

    public Dimensions Dimensions { get; private set; }
    public Category Category { get; private set; }

    protected Product() { }

    public Product(string name, string description, bool active, decimal value, string image, DateTime created_at,Guid categoryId, Dimensions dimensions)
    {
        Name = name;
        Description = description;
        Active = active;
        Value = value;
        Image = image;
        CreatedAt = created_at;
        CategoryId = categoryId;
        Dimensions = dimensions;

        Validate();
    }

    public bool Activate() => Active = true;

    public bool Disable() => Active = false;

    public void ChangeDescription(string description)
    {
        AssertionConcern.ValidateIfEmpty(description, "The Product Description field cannot be empty.");
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

        StockQuantity -= quantity;
    }

    public void ReplenishStock(int quantity) =>
    StockQuantity += quantity;

    public bool HasStock(int quantity) =>
        StockQuantity >= quantity;

    public void Validate()
    {
        AssertionConcern.ValidateIfEmpty(Name, "The Product name field cannot be empty.");
        AssertionConcern.ValidateIfEmpty(Description, "The Product Description field cannot be empty.");
        AssertionConcern.ValidateIfEqual(CategoryId, Guid.Empty, "The Product CategoryId field cannot be empty.");
        AssertionConcern.ValidateIfLessThan(Value, 1, "The Product Value field cannot be less than 0.");
        AssertionConcern.ValidateIfEmpty(Image, "The Product Image field cannot be empty.");
    }
}
