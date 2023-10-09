using CryptoCurrency;
using FluentAssertions;
using Medstuderende_Loesninger.Loesninger;
using Xunit;

namespace Medstuderende_Loesninger.Tests;

public abstract class BaseMedstuderendeTests
{
    private readonly ICryptocurrencyConverter _converter;
    private readonly Dictionary<string, double> _currenciesDict;
    private List<KeyValuePair<string, double>> _currenciesKeyValueStore;
    private Dictionary<CryptocurrencyConfig.CryptocurrencyName, Cryptocurrency> _currenciesCustDict;
    private double CurrencyFirstValue()
    {
        if (_currenciesDict != null && _currenciesDict.Any()) return _currenciesDict.First().Value; 
        if(_currenciesKeyValueStore != null && _currenciesKeyValueStore.Any()) return _currenciesKeyValueStore.First().Value;
        if(_currenciesCustDict != null && _currenciesCustDict.Any()) return _currenciesCustDict.First().Value.Price;
        throw new NullReferenceException();
    }
    private double CurrencyCount()
    {
        if (_currenciesDict != null) return _currenciesDict.Select(c => c.Value).ToList().Count; 
        if(_currenciesKeyValueStore != null) return _currenciesKeyValueStore.Select(c => c.Value).ToList().Count;
        if(_currenciesCustDict != null) return _currenciesCustDict.Select(c => c.Value).ToList().Count;
        throw new NullReferenceException();
    }
    
    // Constructors for the different cases of cryptocurrency list/dictionary
    protected BaseMedstuderendeTests(ICryptocurrencyConverter converter, Dictionary<string, double> currencies)
    {
        _converter = converter;
        _currenciesDict = currencies;
        _currenciesDict.Clear();
    }
    protected BaseMedstuderendeTests(KeldGraugaard converter, List<KeyValuePair<string, double>> cryptoCurrenciesKeyValueStore)
    {
        _converter = converter;
        _currenciesKeyValueStore = cryptoCurrenciesKeyValueStore;
        _currenciesKeyValueStore.Clear();
    }
    protected BaseMedstuderendeTests(Converter converter, Dictionary<CryptocurrencyConfig.CryptocurrencyName, Cryptocurrency> cryptoCurrenciesKeyValueStore)
    {
        _converter = converter;
        _currenciesCustDict = cryptoCurrenciesKeyValueStore;
        _currenciesCustDict.Clear();
    }

    // All of the tests below this line
    // ******************************************************************************************************************************
    [Fact]
    public void When_setting_price_for_new_cryptocurrency_a_new_cryptocurrency_is_added_to_the_list()
    {
        // Arrange
        var amountOfCryptocurrenciesBefore = CurrencyCount();

        // Act 
        _converter.SetPricePerUnit("Litecoin", 67.72);
        var amountOfCryptocurrenciesAfter = CurrencyCount();

        // Assert
        amountOfCryptocurrenciesBefore.Should().Be(0);
        amountOfCryptocurrenciesAfter.Should().Be(1);
    }
    
    [Fact]
    public void When_setting_price_for_existing_cryptocurrency_with_valid_price_the_price_gets_updated()
    {
        // Arrange
        _converter.SetPricePerUnit("Litecoin", 67.72);
        var priceBefore = CurrencyFirstValue();

        // Act 
        _converter.SetPricePerUnit("Litecoin", 1);
        var priceAfter = CurrencyFirstValue();

        // Assert
        priceBefore.Should().Be(67.72);
        priceAfter.Should().Be(1);
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void When_setting_price_for_existing_cryptocurrency_with_invalid_price_throws_ArgumentException(double updatedPrice)
    {
        // Arrange
        _converter.SetPricePerUnit("Litecoin", 67.72);

        // Act 
        var updatingWithInvalidPrice = () => { _converter.SetPricePerUnit("Litecoin", updatedPrice); };

        // Assert
        updatingWithInvalidPrice.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Converting_one_bitcoin_to_litecoins_when_they_exist_in_the_internal_list_returns_converted_amount()
    {
        // Arrange
        _converter.SetPricePerUnit("Bitcoin", 28312.70);
        _converter.SetPricePerUnit("Litecoin", 67.72);
        
        // Act 
        var amountOfConvertedLitecoinShares = 
            Math.Round(_converter.Convert("Bitcoin", "Litecoin", 1), 2);

        // Assert
        amountOfConvertedLitecoinShares.Should().Be(418.08);
    }
    
    [Fact]
    public void Converting_from_a_cryptocurrency_that_exists_to_one_that_does_not_exist_throws_ArgumentException()
    {
        // Arrange
        _converter.SetPricePerUnit("Bitcoin", 28312.70);
        
        // Act 
        var conversionWithNonexistingCryptecurrency = () =>
        {
            _converter.Convert("Bitcoin", "Litecoin", 2);
        };

        // Assert
        conversionWithNonexistingCryptecurrency.Should().Throw<ArgumentException>();
    }
    
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Converting_from_a_cryptocurrency_with_a_invalid_amount_throws_ArgumentException(int amount)
    {
        // Arrange
        _converter.SetPricePerUnit("Bitcoin", 28312.70);
        _converter.SetPricePerUnit("Litecoin", 67.72);
        
        // Act 
        var conversionWithInvalidAmount = () =>
        {
            _converter.Convert("Bitcoin", "Litecoin", amount);
        };

        // Assert
        conversionWithInvalidAmount.Should().Throw<ArgumentException>();
    }
}