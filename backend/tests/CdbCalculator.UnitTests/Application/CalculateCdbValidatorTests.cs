using CdbCalculator.Application.Investments.CalculateCdb;

namespace CdbCalculator.UnitTests.Application;

public sealed class CalculateCdbValidatorTests
{
    [Fact]
    public void Validate_ShouldThrowArgumentNullException_WhenRequestIsNull()
    {
        CalculateCdbRequest? request = null;

        ArgumentNullException exception =
            Assert.Throws<ArgumentNullException>(() => CalculateCdbValidator.Validate(request!));

        Assert.Equal("request", exception.ParamName);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentOutOfRangeException_WhenInitialAmountIsZero()
    {
        CalculateCdbRequest request = new(0m, 12);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => CalculateCdbValidator.Validate(request));

        Assert.Equal("initialAmount", exception.ParamName);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentOutOfRangeException_WhenInitialAmountIsNegative()
    {
        CalculateCdbRequest request = new(-100m, 12);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => CalculateCdbValidator.Validate(request));

        Assert.Equal("initialAmount", exception.ParamName);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsOne()
    {
        CalculateCdbRequest request = new(1000m, 1);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => CalculateCdbValidator.Validate(request));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsZero()
    {
        CalculateCdbRequest request = new(1000m, 0);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => CalculateCdbValidator.Validate(request));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Validate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsNegative()
    {
        CalculateCdbRequest request = new(1000m, -1);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => CalculateCdbValidator.Validate(request));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Validate_ShouldNotThrow_WhenRequestIsValid()
    {
        CalculateCdbRequest request = new(1000m, 12);

        Exception? exception = Record.Exception(() => CalculateCdbValidator.Validate(request));

        Assert.Null(exception);
    }
}