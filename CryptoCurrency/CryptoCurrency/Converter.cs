namespace CryptoCurrency;

public class Converter
{
    private readonly CryptocurrencyConfig _cryptocurrencyConfig = new ();
    private readonly CryptocurrencyHandler _cryptocurrencyHandler = new();
    
    public Dictionary<CryptocurrencyConfig.CryptocurrencyName, Cryptocurrency> Cryptocurrencies { get; private set; } = new();
    
    /// <summary>
    /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
    /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
    /// bliver den gamle værdi overskrevet af den nye værdi
    /// </summary>
    /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
    /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
    public void SetPricePerUnit(String currencyName, double price)
    {
        if (price < 0) throw new ArgumentException($"{nameof(price)} cannot be a negative number");
            
        var cryptocurrencyNameMap = _cryptocurrencyHandler.GetCryptocurrencyEnumFromString(currencyName);
        
        Cryptocurrencies.TryGetValue(cryptocurrencyNameMap, out var existingCryptocurrency);
        if (existingCryptocurrency == null)
        {
            Console.WriteLine($"{nameof(cryptocurrencyNameMap)} cryptocurrency does not exist in the internal list, adding it with the price of {price} USD.");
            Cryptocurrencies.Add(cryptocurrencyNameMap,
                new Cryptocurrency { Name = cryptocurrencyNameMap, Price = price });
            return;
        }

        Console.WriteLine($"Updating {nameof(cryptocurrencyNameMap)} cryptocurrency, adjusting the price from {existingCryptocurrency.Price} USD to {price} USD.");
        existingCryptocurrency.Price = price;
        Cryptocurrencies[cryptocurrencyNameMap] = existingCryptocurrency;
    }

    /// <summary>
    /// Konverterer fra en kryptovaluta til en anden. 
    /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
    /// </summary>
    /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
    /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
    /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
    /// <returns>Værdien af beløbet i toCurrencyName</returns>
    public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
    {
        var fromCryptocurrencyNameMap = _cryptocurrencyHandler.GetCryptocurrencyEnumFromString(fromCurrencyName);
        var toCryptocurrencyNameMap = _cryptocurrencyHandler.GetCryptocurrencyEnumFromString(toCurrencyName);

        var fromCryptocurrency = GetCryptocurrencyFromInternalStorage(fromCryptocurrencyNameMap);
        var toCryptocurrency = GetCryptocurrencyFromInternalStorage(toCryptocurrencyNameMap);

        var sumOfConverted = Math.Round(fromCryptocurrency.Price * amount, 2);
        var convertedAmount = Math.Round(sumOfConverted / toCryptocurrency.Price, 2);

        Console.WriteLine(
            $"Converting {amount} of {fromCurrencyName} cryptocurrency totaling {sumOfConverted} USD to {convertedAmount} of {toCurrencyName} cryptocurrency.");
        return convertedAmount;
    }
    
    private Cryptocurrency GetCryptocurrencyFromInternalStorage(CryptocurrencyConfig.CryptocurrencyName name)
    {
        Cryptocurrencies.TryGetValue(name, out var cryptocurrency);

        return cryptocurrency ?? throw new ArgumentException(
            $"{name.ToString()} has not yet been created and can therefore not be a part of a trade. " +
            $"Please create the cryptocurrency before attempting to trade.");
    }

}