namespace CdbCalculator.Application.Investments.CalculateCdb;

public static class CalculateCdbValidator
{
    public static void Validate(CalculateCdbRequest request)
    {
        ArgumentNullException.ThrowIfNull(nameof(request));

        if (request.InitialAmount <= 0)
        {
            throw new ArgumentException("Initial amount must be greater than zero.", nameof(request));
        }

        if (request.Months <= 1)
        {
            throw new ArgumentException("Months must be greater than one.", nameof(request));
        }
    }
}