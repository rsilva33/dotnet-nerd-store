namespace NerdStore.Core.Validations;

public class AssertionConcern
{
    public static void ValidateIfEqual(object obj1, object obj2, string message)
    {
        if (obj1.Equals(obj2))
            throw new DomainException(message);
    }

    public static void ValidateIfDifferent(object obj1, object obj2, string message)
    {
        if (!obj1.Equals(obj2))
            throw new DomainException(message);
    }
    public static void ValidateIfDifferent(string pattern, string value, string message)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(value))
            throw new DomainException(message);
    }

    public static void ValidateSize(string value, int max, string message)
    {
        var length = value.Trim().Length;
        if (length > max)
            throw new DomainException(message);
    }

    public static void ValidateSize(string value, int min, int max, string message)
    {
        var length = value.Trim().Length;
        if (length < min || length > max)
            throw new DomainException(message);
    }

    public static void ValidateCharacters(string value, int max, string message)
    {
        var lenght = value.Trim().Length;

        if (lenght > max)
            throw new DomainException(message);
    }

    public static void ValidateCharacters(string value, int min, int max, string message)
    {
        var lenght = value.Trim().Length;

        if (lenght < min || lenght > max)
            throw new DomainException(message);
    }

    public static void ValidateExpression(string pattern, string value, string message)
    {
        var regex = new Regex(pattern);

        if (!regex.IsMatch(value))
            throw new DomainException(message);
    }

    public static void ValidateIfEmpty(string value, string message)
    {
        if (value is null || value.Trim().Length is 0)
            throw new DomainException(message);
    }

    public static void ValidateIfNull(string obj, string message)
    {
        if (obj is null)
            throw new DomainException(message);
    }

    public static void ValidateMinMax(double value, double min, double max, string message)
    {
        if (value < min || value > max)
            throw new DomainException(message);
    }

    public static void ValidateMinMax(float value, float min, float max, string message)
    {
        if (value < min || value > max)
            throw new DomainException(message);
    }

    public static void ValidateMinMax(int value, int min, int max, string message)
    {
        if (value < min || value > max)
            throw new DomainException(message);
    }

    public static void ValidateMinMax(long value, long min, long max, string message)
    {
        if (value < min || value > max)
            throw new DomainException(message);
    }

    public static void ValidateMinMax(decimal value, decimal min, decimal max, string message)
    {
        if (value < min || value > max)
            throw new DomainException(message);
    }
    public static void ValidateIfLessThan(long value, long min, string message)
    {
        if (value < min)
            throw new DomainException(message);
    }

    public static void ValidateIfLessThan(double value, double min, string message)
    {
        if (value < min)
            throw new DomainException(message);
    }

    public static void ValidateIfLessThan(decimal value, decimal min, string message)
    {
        if (value < min)
            throw new DomainException(message);
    }

    public static void ValidateIfLessThan(int value, int min, string message)
    {
        if (value < min)
            throw new DomainException(message);
    }


    public static void ValidateIfFalse(bool boolValue, string message)
    {
        if (!boolValue)
            throw new DomainException(message);
    }

    public static void ValidateIfTrue(bool boolValue, string message)
    {
        if (boolValue)
            throw new DomainException(message);
    }
}
