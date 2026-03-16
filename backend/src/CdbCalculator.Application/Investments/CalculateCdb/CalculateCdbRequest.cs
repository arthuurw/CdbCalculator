namespace CdbCalculator.Application.Investments.CalculateCdb;

public sealed record CalculateCdbRequest(
    /// <summary>Initial investment amount</summary>
    /// <example>1000.00</example>
    decimal InitialAmount,

    /// <summary>Investment term in months</summary>
    /// <example>12</example>
    int Months
    );