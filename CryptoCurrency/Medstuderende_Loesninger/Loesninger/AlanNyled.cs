namespace Medstuderende_Loesninger.Loesninger;

public class AlanNyled : ICryptocurrencyConverter
{
     public Dictionary<string, double> _cryptoPrices = new Dictionary<string, double>(StringComparer.OrdinalIgnoreCase);

     /// <summary>
     /// Angiver prisen for en enhed af en kryptovaluta. Prisen angives i dollars.
     /// Hvis der tidligere er angivet en værdi for samme kryptovaluta, 
     /// bliver den gamle værdi overskrevet af den nye værdi
     /// </summary>
     /// <param name="currencyName">Navnet på den kryptovaluta der angives</param>
     /// <param name="price">Prisen på en enhed af valutaen målt i dollars. Prisen kan ikke være negativ</param>
     public void SetPricePerUnit(String currencyName, double price)
     {
         if (string.IsNullOrWhiteSpace(currencyName))
         {
             throw new ArgumentException("Valutanavnet kan ikke være tomt eller kun bestå af mellemrum.");
         }

         if (price <= 0)
         {
             throw new ArgumentException("Prisen kan ikke være 0 eller negativ.");
         }

         _cryptoPrices[currencyName] = price;
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
    public double Convert(String fromCurrencyName, String toCurrencyName, double amount) {
        if (!_cryptoPrices.ContainsKey(fromCurrencyName)) {
            throw new ArgumentException($"Kryptovaluta {fromCurrencyName} eksisterer ikke.");
        }
        if (!_cryptoPrices.ContainsKey(toCurrencyName)) {
            throw new ArgumentException($"Kryptovaluta {toCurrencyName} eksisterer ikke.");
        }

        double usdValue = amount * _cryptoPrices[fromCurrencyName];
        return usdValue / _cryptoPrices[toCurrencyName];
    }
}