using CdbCalculator.Application.Investments.CalculateCdb;

namespace CdbCalculator.UnitTests.Application;

public class CalculateCdbResponseTests
{
    [Fact]
    public void Constructor_ShouldAssignGrossAndNetAmounts()
    {
        CalculateCdbResponse response = new(1234.56m, 1200.10m);

        Assert.Equal(1234.56m, response.GrossAmount);
        Assert.Equal(1200.10m, response.NetAmount);
    }

    [Fact]
    public void Equality_ShouldWorkForSameValues()
    {
        CalculateCdbResponse first = new(1000m, 950m);
        CalculateCdbResponse second = new(1000m, 950m);

        Assert.Equal(first, second);
    }
}