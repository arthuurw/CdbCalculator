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
    public void Calculate_ShouldReturnCorrectGrossAmount_ForOneMonth()
    {
        decimal initialAmount = 1000m;
        int months = 1;

        CdbGrossResult result = GrossCdbCalculator.Calculate(initialAmount, months);

        decimal expected = Math.Round(1000m * 1.00972m, 2, MidpointRounding.ToEven);

        Assert.Equal(expected, result.GrossAmount);
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

        expected = Math.Round(expected, 2, MidpointRounding.ToEven);

        Assert.Equal(expected, result.GrossAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnSameAmount_WhenMonthsIsZero()
    {
        decimal initialAmount = 1000m;
        int months = 0;

        CdbGrossResult result = GrossCdbCalculator.Calculate(initialAmount, months);

        Assert.Equal(1000m, result.InitialAmount);
        Assert.Equal(1000m, result.GrossAmount);
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

        Assert.Equal(Math.Round(result.GrossAmount, 2, MidpointRounding.ToEven), result.GrossAmount);
    }
}