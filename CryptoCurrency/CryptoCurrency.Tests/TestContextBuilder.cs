namespace CryptoCurrency.Tests;

public class TestContextBuilder : Converter
{
    private readonly CryptocurrencyHandler _handler = new();
    public IEnumerable<Cryptocurrency> CryptocurrencyList =>
        Cryptocurrencies.Select(c => c.Value).ToList();

    public TestContextBuilder WithCryptocurrency(CryptocurrencyConfig.CryptocurrencyName name, double price)
    {
        SetPricePerUnit(_handler.GetCryptocurrencyNameFromEnum(name), price);
        return this;
    }

    public void SetPricePerUnit(CryptocurrencyConfig.CryptocurrencyName name, double price)
    {
        SetPricePerUnit(_handler.GetCryptocurrencyNameFromEnum(name), price);
    }

    public double Convert(CryptocurrencyConfig.CryptocurrencyName fromCryptocurrency,
        CryptocurrencyConfig.CryptocurrencyName toCryptocurrency, double amount)
    {
        var fromCryptocurrencyString = _handler.GetCryptocurrencyNameFromEnum(fromCryptocurrency);
        var toCryptocurrencyString = _handler.GetCryptocurrencyNameFromEnum(toCryptocurrency);
        return Convert(fromCryptocurrencyString, toCryptocurrencyString, amount);
    }
}