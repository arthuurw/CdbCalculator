using CdbCalculator.Application.Investments.CalculateCdb.Interfaces;
using CdbCalculator.Domain.Calculations;

namespace CdbCalculator.Application.Investments.CalculateCdb;

public sealed class CalculateCdbService : ICalculateCdbService
{
    public CalculateCdbResponse Calculate(CalculateCdbRequest request)
    {
        CalculateCdbValidator.Validate(request);

        var grossResult = GrossCdbCalculator.Calculate(
            request.InitialAmount,
            request.Months);

        var netResult = IncomeTaxCalculator.Calculate(
            grossResult,
            request.Months);

        return new CalculateCdbResponse(
            grossResult.GrossAmount,
            netResult.NetAmount
        );
    }
}