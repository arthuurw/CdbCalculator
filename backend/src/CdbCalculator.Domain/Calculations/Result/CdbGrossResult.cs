namespace CdbCalculator.Domain.Calculations.Result;

public sealed record CdbGrossResult(
    decimal InitialAmount,
    decimal GrossAmount
    );