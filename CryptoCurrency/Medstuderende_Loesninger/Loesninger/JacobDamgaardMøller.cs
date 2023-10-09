namespace Medstuderende_Loesninger.Loesninger;

public class JacobDamgaardMøller : ICryptocurrencyConverter
{
    public Dictionary<string, double> Currencies { get; set; }
    //Main method
    public static void Main(string[] args)
    {
    }

    public JacobDamgaardMøller()
    {
        Currencies = new Dictionary<string, double>();
    }

    /// <summary>
    /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
    /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
    /// bliver den gamle værdi overskrevet af den nye værdi
    /// </summary>
    /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
    /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
    public void SetPricePerUnit(String currencyName, double price)
    {
        // Prisen kan ikke være negativ!
        if (price < 0)
        {
            throw new System.ArgumentException("Prisen kan ikke være negativ");
        }
        // Opdater pris på valutaen, hvis den allerede findes
        if (Currencies.ContainsKey(currencyName))
        {
            Currencies[currencyName] = price;
        }
        else
        {
            Currencies.Add(currencyName, price);
        }
    }

    public double GetPricePerUnit(String currencyName)
    {
        if (Currencies.ContainsKey(currencyName) == false)
        {
            throw new System.ArgumentException($"Valutaen findes ikke: {currencyName}.");
        }
        return Currencies[currencyName];
    }

    /// <summary>
    /// Konverterer fra en kryptovaluta til en anden. 
    /// Hvis en af de angivne valutaer ikke findes, kaster funktionen en ArgumentException
    /// 
    /// </summary>
    /// <param name="fromCurrencyName">Navnet på den valuta, der konverterers fra</param>
    /// <param name="toCurrencyName">Navnet på den valuta, der konverteres til</param>
    /// <param name="amount">Beløbet angivet i valutaen angivet i fromCurrencyName</param>
    /// <returns>Værdien af beløbet i toCurrencyName</returns>
    public double Convert(String fromCurrencyName, String toCurrencyName, double amount)
    {
        double fromCurrencyPrice = GetPricePerUnit(fromCurrencyName);
        double toCurrencyPrice = GetPricePerUnit(toCurrencyName);
        double totalCurrencyPriceFrom = fromCurrencyPrice * amount;
        return totalCurrencyPriceFrom / toCurrencyPrice;
    }
}