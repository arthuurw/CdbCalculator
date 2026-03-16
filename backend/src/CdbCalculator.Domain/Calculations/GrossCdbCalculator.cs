using CdbCalculator.Domain.Calculations.Result;

namespace CdbCalculator.Domain.Calculations;

public static class GrossCdbCalculator
{
    const decimal Cdi = 0.009m;
    const decimal Tb = 1.08m;

    public static CdbGrossResult Calculate(decimal initialAmount, int months)
    {
        if (initialAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialAmount), initialAmount, "Initial amount must be greater than zero.");

        if (months <= 0)
            throw new ArgumentOutOfRangeException(nameof(months), months, "Months must be greater than zero.");

        var monthlyFactor = 1 + (Cdi * Tb);
        var grossAmount = initialAmount;

        for (var month = 0; month < months; month++)
        {
            grossAmount *= monthlyFactor;
        }

        grossAmount = Math.Round(grossAmount, 2, MidpointRounding.ToEven);

        return new CdbGrossResult(
            initialAmount,
            grossAmount
        );
    }
}