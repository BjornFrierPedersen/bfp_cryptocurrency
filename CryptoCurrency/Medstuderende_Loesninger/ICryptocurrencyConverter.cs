namespace Medstuderende_Loesninger;

public interface ICryptocurrencyConverter
{
    void SetPricePerUnit(String currencyName, double price);
    double Convert(String fromCurrencyName, String toCurrencyName, double amount);
}