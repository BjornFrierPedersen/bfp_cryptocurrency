using CryptoCurrency;
using Medstuderende_Loesninger.Loesninger;

namespace Medstuderende_Loesninger.Tests;

public class ChristianAnkerstjerneTests : BaseMedstuderendeTests
{
    private static readonly ChristianAnkerstjerne Converter = new();
    
    public ChristianAnkerstjerneTests() : base(Converter, Converter.currencyPriceList)
    { }
}

public class AlanNyledTests : BaseMedstuderendeTests
{
    private static readonly AlanNyled Converter = new();
    
    public AlanNyledTests() : base(Converter, Converter._cryptoPrices)
    { }
}

public class BjørnPedersenTests : BaseMedstuderendeTests
{
    private static readonly Converter Converter = new();
    
    public BjørnPedersenTests() : base(Converter, Converter.Cryptocurrencies)
    { }
}

public class SaidKayedTests : BaseMedstuderendeTests
{
    private static readonly SaidKayed Converter = new();
    
    public SaidKayedTests() : base(Converter, Converter.kryptoValutas)
    { }
}

public class JanDahlMadsenTests : BaseMedstuderendeTests
{
    private static readonly JanDahlMadsen Converter = new();
    
    public JanDahlMadsenTests() : base(Converter, Converter.currencyList)
    { }
}

public class JacobDamgaardMøllerTests : BaseMedstuderendeTests
{
    private static readonly JacobDamgaardMøller Converter = new();
    
    public JacobDamgaardMøllerTests() : base(Converter, Converter.Currencies)
    { }
} 

public class MatthæusEllehammerSheinTests : BaseMedstuderendeTests
{
    private static readonly MatthæusEllehammerShein Converter = new();
    
    public MatthæusEllehammerSheinTests() : base(Converter, Converter.cryptoCurrencies)
    { }
}

public class KeldGraugaardTests : BaseMedstuderendeTests
{
    private static readonly KeldGraugaard Converter = new();

    public KeldGraugaardTests() : 
        base(Converter, Converter.CryptoCurrencies)
    { }
}