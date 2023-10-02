namespace CryptoCurrency;

public class Cryptocurrency
{
    public required CryptocurrencyConfig.CryptocurrencyName Name { get; init; }
    public required double Price { get; set; }
}