namespace CdbCalculator.Application.Investments.CalculateCdb;

public sealed record CalculateCdbRequest(
    decimal InitialAmount,
    int Months
    );