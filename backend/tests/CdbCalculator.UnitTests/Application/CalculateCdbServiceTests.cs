using CdbCalculator.Application.Investments.CalculateCdb;

namespace CdbCalculator.UnitTests.Application;

public sealed class CalculateCdbServiceTests
{
    private readonly CalculateCdbService _sut = new();

    [Fact]
    public void Calculate_ShouldReturnGrossAndNetAmounts_WhenRequestIsValid()
    {
        CalculateCdbRequest request = new(1000m, 12);

        CalculateCdbResponse response = _sut.Calculate(request);

        Assert.True(response.GrossAmount > 1000m);
        Assert.True(response.NetAmount > 1000m);
        Assert.True(response.GrossAmount > response.NetAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnExpectedValues_ForKnownScenario()
    {
        CalculateCdbRequest request = new(1000m, 2);

        CalculateCdbResponse response = _sut.Calculate(request);

        decimal factor = 1.00972m;
        decimal grossAmount = 1000m;

        for (int i = 0; i < 2; i++)
        {
            grossAmount *= factor;
        }

        grossAmount = Math.Round(grossAmount, 2, MidpointRounding.ToEven);

        decimal earnings = grossAmount - 1000m;
        decimal taxAmount = Math.Round(earnings * 0.225m, 2, MidpointRounding.ToEven);
        decimal netAmount = Math.Round(grossAmount - taxAmount, 2, MidpointRounding.ToEven);

        Assert.Equal(grossAmount, response.GrossAmount);
        Assert.Equal(netAmount, response.NetAmount);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenInitialAmountIsZero()
    {
        CalculateCdbRequest request = new(0m, 12);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(request));

        Assert.Equal("initialAmount", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenInitialAmountIsNegative()
    {
        CalculateCdbRequest request = new(-100m, 12);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(request));

        Assert.Equal("initialAmount", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsOne()
    {
        CalculateCdbRequest request = new(1000m, 1);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(request));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsZero()
    {
        CalculateCdbRequest request = new(1000m, 0);

        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Calculate(request));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldReturnRoundedAmounts()
    {
        CalculateCdbRequest request = new(1234.56m, 7);

        CalculateCdbResponse response = _sut.Calculate(request);

        Assert.Equal(Math.Round(response.GrossAmount, 2, MidpointRounding.ToEven), response.GrossAmount);
        Assert.Equal(Math.Round(response.NetAmount, 2, MidpointRounding.ToEven), response.NetAmount);
    }
}