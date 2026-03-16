namespace CdbCalculator.Application.Investments.CalculateCdb;

public static class CalculateCdbValidator
{
    public static void Validate(CalculateCdbRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        decimal initialAmount = request.InitialAmount;
        int months = request.Months;

        ValidateGreaterThanZero(initialAmount, nameof(initialAmount), "Initial amount must be greater than zero.");
        ValidateGreaterThanOne(months, nameof(months), "Months must be greater than one.");
    }

    private static void ValidateGreaterThanZero(decimal value, string parameterName, string message)
    {
        if (value <= 0)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, message);
        }
    }

    private static void ValidateGreaterThanOne(int value, string parameterName, string message)
    {
        if (value <= 1)
        {
            throw new ArgumentOutOfRangeException(parameterName, value, message);
        }
    }
}