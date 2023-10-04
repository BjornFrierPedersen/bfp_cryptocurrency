namespace CryptoCurrency;

public static class CryptocurrencyExtensions
{
    public static Cryptocurrency GetCryptocurrency(
        this Dictionary<CryptocurrencyConfig.CryptocurrencyName, Cryptocurrency> source, CryptocurrencyConfig.CryptocurrencyName name)
    {
        source.TryGetValue(name, out var cryptocurrency);
        return cryptocurrency ?? throw new ArgumentException(
            $"{name.ToString()} has not yet been created and can therefore not be a part of a trade. " +
            $"Please create the cryptocurrency before attempting to trade.");
    }
    
    public static void UpsertCryptocurrency(
        this Dictionary<CryptocurrencyConfig.CryptocurrencyName, Cryptocurrency> source, Cryptocurrency cryptocurrency)
    {
        source.TryGetValue(cryptocurrency.Name, out var existingCryptocurrency);
        if (existingCryptocurrency == null)
        {
            // Insert
            Console.WriteLine(
                $"{nameof(cryptocurrency.Name)} cryptocurrency does not exist in the internal list, adding it with the price of {cryptocurrency.Price} USD.");
            source.Add(cryptocurrency.Name, cryptocurrency);
            return;
        }

        // Update
        Console.WriteLine(
            $"Updating {nameof(cryptocurrency.Name)} cryptocurrency, adjusting the price from {existingCryptocurrency.Price} USD to {cryptocurrency.Price} USD.");
        source[cryptocurrency.Name] = cryptocurrency;
    }
    
    
    
    
   
}