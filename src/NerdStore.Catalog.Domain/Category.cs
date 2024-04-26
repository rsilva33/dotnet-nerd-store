namespace NerdStore.Catalog.Domain;

public class Category : Entity
{
    public string Name { get; private set; } = string.Empty;
    public int Code { get; private set; }

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
        AssertionConcern.ValidateIfEmpty(Name, "");
        AssertionConcern.ValidateIfEqual(Code, 0,"");
    }

}
