using CdbCalculator.Domain.Calculations;
using CdbCalculator.Domain.Calculations.Result;

namespace CdbCalculator.UnitTests.Domain;

public sealed class IncomeTaxCalculatorTests
{
    [Theory]
    [InlineData(6, 0.225)]
    [InlineData(7, 0.20)]
    [InlineData(12, 0.20)]
    [InlineData(13, 0.175)]
    [InlineData(24, 0.175)]
    [InlineData(25, 0.15)]
    public void Calculate_ShouldApplyCorrectTaxRate_BasedOnInvestmentTerm(int months, decimal expectedTaxRate)
    {
        CdbGrossResult grossResult = new(1000m, 1100m);

        CdbNetResult result = IncomeTaxCalculator.Calculate(grossResult, months);

        Assert.Equal(expectedTaxRate, result.TaxRate);
    }

    [Fact]
    public void Calculate_ShouldCalculateTaxAmount_OnlyOverEarnings()
    {
        CdbGrossResult grossResult = new(1000m, 1100m);
        int months = 6;

        CdbNetResult result = IncomeTaxCalculator.Calculate(grossResult, months);

        decimal earnings = 100m;
        decimal expectedTaxAmount = Math.Round(earnings * 0.225m, 2, MidpointRounding.ToEven);

        Assert.Equal(expectedTaxAmount, result.TaxAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnCorrectNetAmount()
    {
        CdbGrossResult grossResult = new(1000m, 1100m);
        int months = 12;

        CdbNetResult result = IncomeTaxCalculator.Calculate(grossResult, months);

        decimal earnings = 100m;
        decimal taxAmount = Math.Round(earnings * 0.20m, 2, MidpointRounding.ToEven);
        decimal expectedNetAmount = Math.Round(1100m - taxAmount, 2, MidpointRounding.ToEven);

        Assert.Equal(expectedNetAmount, result.NetAmount);
    }

    [Fact]
    public void Calculate_ShouldPreserveGrossAmount_InResult()
    {
        CdbGrossResult grossResult = new(1000m, 1150m);
        int months = 25;

        CdbNetResult result = IncomeTaxCalculator.Calculate(grossResult, months);

        Assert.Equal(grossResult.GrossAmount, result.GrossAmount);
    }

    [Fact]
    public void Calculate_ShouldReturnLowerNetAmount_WhenTaxRateIsHigher()
    {
        CdbGrossResult grossResult = new(1000m, 1200m);

        CdbNetResult resultForSixMonths = IncomeTaxCalculator.Calculate(grossResult, 6);
        CdbNetResult resultForTwentyFiveMonths = IncomeTaxCalculator.Calculate(grossResult, 25);

        Assert.True(resultForTwentyFiveMonths.NetAmount > resultForSixMonths.NetAmount);
    }

    [Fact]
    public void Calculate_ShouldRoundTaxAmount_AndNetAmount_ToTwoDecimalPlaces()
    {
        CdbGrossResult grossResult = new(1234.56m, 1299.99m);
        int months = 13;

        CdbNetResult result = IncomeTaxCalculator.Calculate(grossResult, months);

        Assert.Equal(Math.Round(result.TaxAmount, 2, MidpointRounding.ToEven), result.TaxAmount);
        Assert.Equal(Math.Round(result.NetAmount, 2, MidpointRounding.ToEven), result.NetAmount);
    }
}