namespace CdbCalculator.Domain.Calculations.Result;

public sealed record CdbNetResult(
    decimal GrossAmount,
    decimal NetAmount,
    decimal TaxAmount,
    decimal TaxRate
    );