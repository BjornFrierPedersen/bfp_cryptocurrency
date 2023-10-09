namespace Medstuderende_Loesninger.Loesninger;

public class MatthæusEllehammerShein : ICryptocurrencyConverter
{
    public Dictionary<string, double> cryptoCurrencies;
    public string fromCurrency { get; private set; }
    public double exchangeAmount { get; private set; }
    public double cryptoAmount { get; private set; }
    
    public MatthæusEllehammerShein()
    {
        cryptoCurrencies = new Dictionary<string, double>()
        {
            { "Bitcoin", 27421.70 },
            //Bitcoin
            { "ETH", 1611.91 },
            //Ethereum
            { "USDT", 1.0 },
            //Tether USDt
            { "BNB", 211.10 },
            //BNB
            { "XRP", 0.5235 },
            //XRP
            { "USDC", 1.0 },
            //USDC
            { "SOL", 22.88 },
            //Solana
            { "ADA", 0.2595 },
            //Cardano
            { "DOGE", 0.061 },
            //Dogecoin
            { "TRX", 0.08839 },
            //TRON
            { "DAI", 1.00 },
            //Dai
            { "MATIC", 0.5493 },
            //Polygon
            { "DOT", 4.02 },
            //Polkadot
            { "LTC", 64.15 },
            //Litecoin
            { "WBTC", 27445.60 },
            //Wrapped Bitcoin
            { "BCH", 227.19 },
            //Bitcoin Cash
            { "SHIB", 0.00000718 },
            //Shiba Inu
            { "LINK", 7.54 },
            //Chainlink
            { "AVAX", 9.91 },
            //Avalanche
            { "TUSD", 1.11 }
            //TrueUSD

        };
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
        if (price <= 0)
        {
            throw new ArgumentException("Price must be a postive number");
        }

        if (cryptoCurrencies.ContainsKey(currencyName))
        {
            cryptoCurrencies[currencyName] = price;
        }
        else
        {
            cryptoCurrencies.Add(currencyName, price);
        }
    }

    public double getCryptoCurrency(String currencyName)
    {
        double cryptoCurrency = 0;

        if (cryptoCurrencies.ContainsKey(currencyName))
        {
            cryptoCurrency = cryptoCurrencies[currencyName];
        }

        return cryptoCurrency;

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
        try
        {
            double tmpCryptoRateFrom = 0.0, tmpCryptoRateTo = 0.0;

            if (cryptoCurrencies.ContainsKey(fromCurrencyName))
            {
                tmpCryptoRateFrom = cryptoCurrencies[fromCurrencyName];
            }

            if (cryptoCurrencies.ContainsKey(toCurrencyName))
            {
                tmpCryptoRateTo = cryptoCurrencies[toCurrencyName];
            }

            if (fromCurrencyName == null || toCurrencyName == null)
                return 0;


            double converterCryptoValue = amount * tmpCryptoRateFrom;

            return Math.Round(converterCryptoValue / tmpCryptoRateTo, 2);

        }
        catch
        {
            return 0;
        }
    }
}