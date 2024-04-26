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
        AssertionConcern.ValidateIfSmallerEqualsMinimum(height, 1, "");
        AssertionConcern.ValidateIfSmallerEqualsMinimum(width, 1, "");
        AssertionConcern.ValidateIfSmallerEqualsMinimum(depth, 1, "");
    }

    public string FormattedDescription() => 
        $"WxHxD: {Width} x {Height} x {Depth}";

    public override string ToString() =>
        FormattedDescription();
}
