namespace CdbCalculator.Application.Investments.CalculateCdb.Interfaces;

public interface ICalculateCdbService
{
    CalculateCdbResponse Calculate(CalculateCdbRequest request);
}