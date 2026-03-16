using CdbCalculator.Application.Investments.CalculateCdb;

namespace CdbCalculator.UnitTests.Application;

public sealed class CalculateCdbRequestTests
{
    [Fact]
    public void Constructor_ShouldAssignInitialAmountAndMonths()
    {
        CalculateCdbRequest request = new(1000m, 12);

        Assert.Equal(1000m, request.InitialAmount);
        Assert.Equal(12, request.Months);
    }

    [Fact]
    public void Equality_ShouldWorkForSameValues()
    {
        CalculateCdbRequest first = new(1000m, 12);
        CalculateCdbRequest second = new(1000m, 12);

        Assert.Equal(first, second);
    }
}
