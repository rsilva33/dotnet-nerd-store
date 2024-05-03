namespace NerdStore.Catalog.Domain;
public class Dimensions
{
    public decimal Height { get; private set; }
    public decimal Width { get; private set; }
    public decimal Depth { get; private set; }

    public Dimensions(decimal height, decimal width, decimal depth)
    {
        Validate(height, width, depth);

        Height = height;
        Width = width;
        Depth = depth;
    }

    private void Validate(decimal height, decimal width, decimal depth)
    {
        AssertionConcern.ValidateIfLessThan(height, 1, "The Height field cannot be less than or equal to 0.");
        AssertionConcern.ValidateIfLessThan(width, 1, "The Width field cannot be less than or equal to 0.");
        AssertionConcern.ValidateIfLessThan(depth, 1, "The Depth field cannot be less than or equal to 0.");
    }

    public string FormattedDescription() => 
        $"WxHxD: {Width} x {Height} x {Depth}";

    public override string ToString() =>
        FormattedDescription();
}
