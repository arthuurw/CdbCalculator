using CdbCalculator.Domain.Calculations.Result;

namespace CdbCalculator.Domain.Calculations;

public static class IncomeTaxCalculator
{
    private static readonly (int MaxMonths, decimal TaxRate)[] TaxBrackets =
    [
        (6, 0.225m),
        (12, 0.20m),
        (24, 0.175m),
        (int.MaxValue, 0.15m)
    ];

    public static CdbNetResult Calculate (CdbGrossResult grossResult, int months)
    {
        decimal taxRate = GetTaxRate(months);
        decimal earnings = grossResult.GrossAmount - grossResult.InitialAmount;
        decimal taxAmount = Math.Round(earnings * taxRate, 2, MidpointRounding.ToEven);
        decimal netAmount = Math.Round(grossResult.GrossAmount - taxAmount, 2, MidpointRounding.ToEven);

        return new CdbNetResult(
            grossResult.GrossAmount,
            netAmount,
            taxAmount,
            taxRate
        );
    }

    private static decimal GetTaxRate(int months)
    {
        foreach (var (MaxMonths, TaxRate) in TaxBrackets)
        {
            if (months <= MaxMonths)
            {
                return TaxRate;
            }
        }

        return TaxBrackets[^1].TaxRate;
    }
}