namespace CryptoCurrency;

public class CryptocurrencyHandler
{
    private readonly CryptocurrencyConfig _cryptocurrencyConfig = new();

    public CryptocurrencyConfig.CryptocurrencyName GetCryptocurrencyEnumFromString(string name)
    {
        var cryptocurrencyNameMap = _cryptocurrencyConfig.CryptocurrencyNameMap
            .FirstOrDefault(c => c.Value.Equals(name));

        return cryptocurrencyNameMap.Value != null
            ? cryptocurrencyNameMap.Key
            : throw new ArgumentException($"{name} is not a valid cryptocurrency name");
    }

    public string GetCryptocurrencyNameFromEnum(CryptocurrencyConfig.CryptocurrencyName name)
    {
        _cryptocurrencyConfig.CryptocurrencyNameMap.TryGetValue(name, out var cryptocurrencyName);

        return cryptocurrencyName ??
               throw new ArgumentException(
                   $"No enum value for {cryptocurrencyName} exists in the list of cryptocurrencies");
    }
}