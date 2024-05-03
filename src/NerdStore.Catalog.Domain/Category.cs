namespace NerdStore.Catalog.Domain;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public int Code { get; private set; }

    // EF Relation
    public ICollection<Product> Products { get; set; }

    protected Category() { }

    public Category(string name, int code)
    {
        Name = name;
        Code = code;

        Validate();
    }

    public override string ToString() => 
        $"{Name} - {Code}";

    private void Validate()
    {
        AssertionConcern.ValidateIfEmpty(Name, "The Category name field cannot be empty.");
        AssertionConcern.ValidateIfEqual(Code, 0, "The Code field cannot be 0.");
    }

}
