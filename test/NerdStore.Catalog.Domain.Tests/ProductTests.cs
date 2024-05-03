namespace NerdStore.Catalog.Domain.Tests;

public class ProductTests
{
    [Fact]
    public void Product_Validate_Validations_Return_Exceptions()
    {
        // Arrange & Act & Assert

        var ex = Assert.Throws<DomainException>(() =>
            new Product(string.Empty, "Description", false, 100, "Image", DateTime.Now, Guid.Empty, new Dimensions(1, 1, 1))
        );

        Assert.Equal("The Product name field cannot be empty.", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Name", string.Empty, false, 100, "Image", DateTime.Now, Guid.Empty, new Dimensions(1, 1, 1))
        );

        Assert.Equal("The Product Description field cannot be empty.", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Name", "Description", false, 0, "Image", DateTime.Now, Guid.NewGuid(), new Dimensions(1, 1, 1))
        );

        Assert.Equal("The Product Value field cannot be less than 0.", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Name", "Description", false, 100, "Image", DateTime.Now, Guid.Empty, new Dimensions(1, 1, 1))
        );

        Assert.Equal("The Product CategoryId field cannot be empty.", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Name", "Description", false, 100, string.Empty, DateTime.Now, Guid.NewGuid(), new Dimensions(1, 1, 1))
        );

        Assert.Equal("The Product Image field cannot be empty.", ex.Message);

        ex = Assert.Throws<DomainException>(() =>
            new Product("Name", "Description", false, 100, "Image", DateTime.Now, Guid.Empty, new Dimensions(0, 1, 1))
        );

        Assert.Equal("The Height field cannot be less than or equal to 0.", ex.Message);
    }
}
