using CryptoCurrency;

namespace CryptoCurrencyTests;

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
}