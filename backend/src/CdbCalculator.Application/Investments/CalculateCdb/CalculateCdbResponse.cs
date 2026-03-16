namespace CdbCalculator.Application.Investments.CalculateCdb;

public sealed record CalculateCdbResponse(
    /// <example>1123.66</example>
    decimal GrossAmount,
    /// <example>1098.93</example>
    decimal NetAmount
    );