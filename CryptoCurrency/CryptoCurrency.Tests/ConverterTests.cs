using FluentAssertions;
using Xunit;
using static CryptoCurrency.CryptocurrencyConfig;

namespace CryptoCurrency.Tests;

public class ConverterTests 
{
    //SetPricePerUnit
    //Ækvivalenspartition 1: Når prisen skal indstilles for en ny kryptocurrency (tilføjer en ny kryptocurrency til listen).
    //Ækvivalenspartition 2: Når prisen skal indstilles for en eksisterende kryptocurrency med en gyldig ny pris (opdaterer prisen).
    //Ækvivalenspartition 3: Når prisen skal indstilles for en eksisterende kryptocurrency med en ugyldig ny pris (f.eks. 0 eller negativ værdi).

    // Ækvivalenspartition 1
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
    
    // Ækvivalenspartition 2
    [Fact]
    public void When_setting_price_for_existing_cryptocurrency_with_valid_price_the_price_gets_updated()
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
    
    // Ækvivalenspartition 3
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void When_setting_price_for_existing_cryptocurrency_with_invalid_price_throws_ArgumentException(double updatedPrice)
    {
        // Arrange
        var converter = new TestContextBuilder()
            .WithCryptocurrency(CryptocurrencyName.Litecoin, 67.72);

        // Act 
        var updatingWithInvalidPrice = () => { converter.SetPricePerUnit(CryptocurrencyName.Litecoin, updatedPrice); };

        // Assert
        updatingWithInvalidPrice.Should().Throw<ArgumentException>();
    }

    //Convert
    //Ækvivalenspartition 1: Når konvertering udføres mellem to kryptokurser, der begge findes i den interne liste (gyldig konvertering).
    //Ækvivalenspartition 2: Når konvertering forsøges fra en kryptocurrency, der findes i listen, til en kryptocurrency, der ikke findes i listen (ugyldig konvertering).
    //Ækvivalenspartition 3: Når konvertering forsøges med en ugyldig mængde (f.eks. negativ eller nul mængde).

    // Ækvivalenspartition 1
    [Fact]
    public void Converting_one_bitcoin_shares_to_litecoin_shares_when_they_exist_in_the_internal_list_returns_valid_result()
    {
        // Arrange
        var converter = new TestContextBuilder()
            .WithCryptocurrency(CryptocurrencyName.Bitcoin, 28312.70)
            .WithCryptocurrency(CryptocurrencyName.Litecoin, 67.44);
        
        // Act 
        var amountOfConvertedLitecoinShares = 
            converter.Convert(CryptocurrencyName.Bitcoin, CryptocurrencyName.Litecoin, 1);

        // Assert
        amountOfConvertedLitecoinShares.Should().Be(419.82);
    }
    
    // Ækvivalenspartition 2
    [Fact]
    public void Converting_from_a_cryptocurrency_that_exists_to_one_that_does_not_exist_throws_ArgumentException()
    {
        // Arrange
        var converter = new TestContextBuilder()
            .WithCryptocurrency(CryptocurrencyName.Bitcoin, 28312.70);
        
        // Act 
        var conversionWithNonexistingCryptecurrency = () =>
        {
            converter.Convert(CryptocurrencyName.Bitcoin, CryptocurrencyName.Litecoin, 2);
        };

        // Assert
        conversionWithNonexistingCryptecurrency.Should().Throw<ArgumentException>();
    }
    
    // Ækvivalenspartition 3
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Converting_from_a_cryptocurrency_with_a_invalid_amount_throws_ArgumentException(int amount)
    {
        // Arrange
        var converter = new TestContextBuilder()
            .WithCryptocurrency(CryptocurrencyName.Bitcoin, 28312.70)
            .WithCryptocurrency(CryptocurrencyName.Litecoin, 67.44);
        
        // Act 
        var conversionWithNonValidAmount = () =>
        {
            converter.Convert(CryptocurrencyName.Bitcoin, CryptocurrencyName.Litecoin, amount);
        };

        // Assert
        conversionWithNonValidAmount.Should().Throw<ArgumentException>();
    }
}