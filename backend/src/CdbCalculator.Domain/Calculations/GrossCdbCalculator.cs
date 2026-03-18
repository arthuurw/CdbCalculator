using CdbCalculator.Domain.Calculations.Result;

namespace CdbCalculator.Domain.Calculations;

public static class GrossCdbCalculator
{
    const decimal Cdi = 0.009m;
    const decimal Tb = 1.08m;

    public static CdbGrossResult Calculate(decimal initialAmount, int months)
    {
        if (initialAmount <= 0)
            throw new ArgumentOutOfRangeException(nameof(initialAmount), initialAmount, "O valor inicial deve ser maior que zero.");

        if (months <= 1)
            throw new ArgumentOutOfRangeException(nameof(months), months, "O prazo deve ser maior que um mês.");

        var monthlyFactor = 1 + (Cdi * Tb);
        var grossAmount = initialAmount;

        for (var month = 0; month < months; month++)
        {
            grossAmount *= monthlyFactor;
        }

        grossAmount = Math.Round(grossAmount, 2, MidpointRounding.AwayFromZero);

        return new CdbGrossResult(
            initialAmount,
            grossAmount
        );
    }
}