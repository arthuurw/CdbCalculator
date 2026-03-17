using CdbCalculator.Domain.Calculations;
using CdbCalculator.Domain.Calculations.Result;

namespace CdbCalculator.UnitTests.Domain;

public sealed class GrossCdbCalculatorTests
{
    [Fact]
    public void Calculate_ShouldReturnCorrectInitialAmount()
    {
        decimal initialAmount = 1000m;
        int months = 2;

        CdbGrossResult result = GrossCdbCalculator.Calculate(initialAmount, months);

        Assert.Equal(initialAmount, result.InitialAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnCorrectGrossAmount_ForTwoMonths()
    {
        decimal initialAmount = 1000m;
        int months = 2;

        CdbGrossResult result = GrossCdbCalculator.Calculate(initialAmount, months);

        decimal expected = Math.Round(1000m * 1.00972m * 1.00972m, 2, MidpointRounding.AwayFromZero);

        Assert.Equal(expected, result.GrossAmount);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsOne()
    {
        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => GrossCdbCalculator.Calculate(1000m, 1));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldReturnCorrectGrossAmount_ForMultipleMonths()
    {
        decimal initialAmount = 1000m;
        int months = 3;

        CdbGrossResult result = GrossCdbCalculator.Calculate(initialAmount, months);

        decimal factor = 1.00972m;
        decimal expected = initialAmount;

        for (int i = 0; i < months; i++)
        {
            expected *= factor;
        }

        expected = Math.Round(expected, 2, MidpointRounding.AwayFromZero);

        Assert.Equal(expected, result.GrossAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnGreaterGrossAmount_WhenMonthsIncrease()
    {
        decimal initialAmount = 1000m;

        CdbGrossResult resultForSixMonths = GrossCdbCalculator.Calculate(initialAmount, 6);
        CdbGrossResult resultForTwelveMonths = GrossCdbCalculator.Calculate(initialAmount, 12);

        Assert.True(resultForTwelveMonths.GrossAmount > resultForSixMonths.GrossAmount);
    }

    [Fact]
    public void Calculate_ShouldRoundGrossAmount_ToTwoDecimalPlaces()
    {
        decimal initialAmount = 1234.56m;
        int months = 5;

        CdbGrossResult result = GrossCdbCalculator.Calculate(initialAmount, months);

        Assert.Equal(Math.Round(result.GrossAmount, 2, MidpointRounding.AwayFromZero), result.GrossAmount);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenInitialAmountIsZero()
    {
        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => GrossCdbCalculator.Calculate(0m, 2));

        Assert.Equal("initialAmount", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenInitialAmountIsNegative()
    {
        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => GrossCdbCalculator.Calculate(-100m, 2));

        Assert.Equal("initialAmount", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsZero()
    {
        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => GrossCdbCalculator.Calculate(1000m, 0));

        Assert.Equal("months", exception.ParamName);
    }

    [Fact]
    public void Calculate_ShouldThrowArgumentOutOfRangeException_WhenMonthsIsNegative()
    {
        ArgumentOutOfRangeException exception =
            Assert.Throws<ArgumentOutOfRangeException>(() => GrossCdbCalculator.Calculate(1000m, -1));

        Assert.Equal("months", exception.ParamName);
    }
}