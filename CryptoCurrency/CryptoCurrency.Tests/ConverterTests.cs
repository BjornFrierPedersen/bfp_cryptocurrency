using CryptoCurrencyTests;
using FluentAssertions;
using Xunit;
using static CryptoCurrency.CryptocurrencyConfig;

namespace CryptoCurrency.Tests;

public class ConverterTests 
{
    // SetPricePerUnit
    // To Ækvivilenpartioner:
    // Den første (1) med en helt ny værdi og den næste (2) med 2 ækvivilensgrænseværdier - 1 (Helt ny), 2 (Pris 1 og -1)
    
    [Fact]
    public void When_setting_price_for_new_cryptocurrency_a_new_cryptocurrency_is_added_to_the_list()
    {
        // Arrange
        var converter = new TestContextBuilder();
        var amountOfCryptocurrenciesBefore = converter.CryptocurrencyList.Count();

        // Act 
        converter.SetPricePerUnit(CryptocurrencyName.Litecoin, 67.72);
        var amountOfCryptocurrenciesAfter = converter.CryptocurrencyList.Count();

        // Assert
        amountOfCryptocurrenciesBefore.Should().Be(0);
        amountOfCryptocurrenciesAfter.Should().Be(1);
    }
    
    [Fact]
    public void When_setting_price_for_existing_cryptocurrency_the_price_gets_updated()
    {
        // Arrange
        var converter = new TestContextBuilder()
            .WithCryptocurrency(CryptocurrencyName.Litecoin, 67.72);
        var priceBefore = converter.CryptocurrencyList.First().Price;

        // Act 
        converter.SetPricePerUnit(CryptocurrencyName.Litecoin, 1);
        var priceAfter = converter.CryptocurrencyList.First().Price;

        // Assert
        priceBefore.Should().Be(67.72);
        priceAfter.Should().Be(1);
    }
    
    [Fact]
    public void Setting_a_negative_price_for_existing_cryptocurrency_throws_ArgumentException()
    {
        // Arrange
        var converter = new TestContextBuilder()
            .WithCryptocurrency(CryptocurrencyName.Litecoin, 67.72);

        // Act 
        var settingNegativePriceForCryptocurrency = () => { converter.SetPricePerUnit(CryptocurrencyName.Litecoin, -1); };

        // Assert
        settingNegativePriceForCryptocurrency.Should().Throw<ArgumentException>();
    }
    
    // Convert
    // 
}