namespace CryptoCurrency;

public static class Program
{
    private static readonly CryptocurrencyHandler CryptocurrencyHandler = new();
    public static void Main()
    {
        Converter converter = new();
        
        // Initialize cryptocurrencies
        // Bitcoin
        converter.SetPricePerUnit(
            CryptocurrencyHandler.GetCryptocurrencyNameFromEnum(CryptocurrencyConfig.CryptocurrencyName.Bitcoin),
            28312.70);
        // Auroracoin
        converter.SetPricePerUnit(
            CryptocurrencyHandler.GetCryptocurrencyNameFromEnum(CryptocurrencyConfig.CryptocurrencyName.Auroracoin),
            0.054);
        // Dogecoin
        converter.SetPricePerUnit(
            CryptocurrencyHandler.GetCryptocurrencyNameFromEnum(CryptocurrencyConfig.CryptocurrencyName.Dogecoin),
            0.064);
        
        // Conversion
        var amount = converter.Convert(
            CryptocurrencyHandler.GetCryptocurrencyNameFromEnum(CryptocurrencyConfig.CryptocurrencyName.Bitcoin),
            CryptocurrencyHandler.GetCryptocurrencyNameFromEnum(CryptocurrencyConfig.CryptocurrencyName.Dogecoin),
            2);

        Console.ReadLine();
    }
}