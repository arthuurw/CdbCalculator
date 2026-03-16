namespace CdbCalculator.Application.Investments.CalculateCdb;

public sealed record CalculateCdbResponse(
    decimal GrossAmount,
    decimal NetAmount
    );