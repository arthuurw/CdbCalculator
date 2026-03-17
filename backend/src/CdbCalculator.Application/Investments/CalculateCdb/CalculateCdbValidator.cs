namespace CdbCalculator.Application.Investments.CalculateCdb;

public static class CalculateCdbValidator
{
    public static void Validate(CalculateCdbRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        decimal initialAmount = request.InitialAmount;
        int months = request.Months;

        ValidateMinimumAmount(initialAmount, nameof(initialAmount), "O valor inicial deve ser no mínimo R$0,01.");
        ValidateGreaterThanOne(months, nameof(months), "O prazo deve ser maior que um mês.");
    }

    private static void ValidateMinimumAmount(decimal value, string parameterName, string message)
    {
        if (value < 0.01m)
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