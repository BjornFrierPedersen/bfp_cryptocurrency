namespace CryptoCurrency;

public class Converter
{
    private readonly CryptocurrencyHandler _cryptocurrencyHandler = new();

    protected Dictionary<CryptocurrencyConfig.CryptocurrencyName, Cryptocurrency> Cryptocurrencies { get; } = new();
    
    /// <summary>
    /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
    /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
    /// bliver den gamle værdi overskrevet af den nye værdi
    /// </summary>
    /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
    /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
    public void SetPricePerUnit(String currencyName, double price)
    {
        if (price <= 0) throw new ArgumentException($"{nameof(price)} must be larger than 0");
            
        var cryptocurrencyNameMap = _cryptocurrencyHandler.GetCryptocurrencyEnumFromString(currencyName);
        var currency = new Cryptocurrency { Name = cryptocurrencyNameMap, Price = price };
        
        Cryptocurrencies.UpsertCryptocurrency(currency);
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
        if(amount <= 0) throw new ArgumentException($"{nameof(amount)} must be larger than 0");
        
        var fromCryptocurrencyNameMap = _cryptocurrencyHandler.GetCryptocurrencyEnumFromString(fromCurrencyName);
        var toCryptocurrencyNameMap = _cryptocurrencyHandler.GetCryptocurrencyEnumFromString(toCurrencyName);

        var fromCryptocurrency = Cryptocurrencies.GetCryptocurrency(fromCryptocurrencyNameMap);
        var toCryptocurrency = Cryptocurrencies.GetCryptocurrency(toCryptocurrencyNameMap);

        var sumOfConverted = Math.Round(fromCryptocurrency.Price * amount, 2);
        var convertedAmount = Math.Round(sumOfConverted / toCryptocurrency.Price, 2);

        Console.WriteLine(
            $"Converting {amount} of {fromCurrencyName} cryptocurrency totaling {sumOfConverted} USD to {convertedAmount} shares of {toCurrencyName} cryptocurrency.");
        return convertedAmount;
    }
}