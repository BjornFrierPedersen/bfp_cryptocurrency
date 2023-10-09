namespace Medstuderende_Loesninger.Loesninger;

public class ChristianAnkerstjerne : ICryptocurrencyConverter
{
    public Dictionary<string, double> currencyPriceList = new() { };

    public void SetPricePerUnit(String currencyName, double price) {
        if(currencyName == "") {
            throw new ArgumentException("Error: Please enter a currency name.");
        }

        if(price <= 0) {
            throw new ArgumentException("Error: Please enter a price greater than zero.");
        }

        currencyPriceList[currencyName] = price;
    }

    public double GetPricePerUnit(String currencyName) {
        return currencyPriceList[currencyName];
    }

    public double Convert(String fromCurrencyName, String toCurrencyName, double amount) {
        double fromCurrencyUSDValuePerUnit = GetPricePerUnit(fromCurrencyName);
        double toCurrencyUSDValuePerUnit = GetPricePerUnit(toCurrencyName);
        double toCurrencyUSDTotalValue = fromCurrencyUSDValuePerUnit * amount / toCurrencyUSDValuePerUnit;

        return toCurrencyUSDTotalValue;
    }
}