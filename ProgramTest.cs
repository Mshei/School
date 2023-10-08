using System;
using System.Data;

using Xunit;

public class ProgramTest {
    [Fact]
    public void test_Crypto_Conversion()
    {
        Converter converter = new Converter();
        converter.initCryptoCurrencies();

        double cryptoAmount = converter.Convert("Bitcoin", "DAI", 10);

        Assert.Equal(27421.70, cryptoAmount);
    }

    [Fact]
    public void test_Crypto_Conversion2nd()
    {
        Converter converter = new Converter();
        converter.initCryptoCurrencies();

        double cryptoAmount = converter.Convert("SOL", "DOT", 211);

        Assert.Equal(1200.92, cryptoAmount);
    }

    [Fact]
    public void test_Crypto_Conversion3rd()
    {
        Converter converter = new Converter();
        converter.initCryptoCurrencies();

        double cryptoAmount = converter.Convert("DOT", "SOL", 211);

        Assert.Equal(37.07, cryptoAmount);
    }

    [Theory]
    [InlineData("Bitcoin", 200)]
    [InlineData("DAI", 500)]
    public void test_Create_new_Crypto_Rate_Added(string _CryptoCurrency, double _Rate)
    {
        Converter converter = new Converter();
        converter.initCryptoCurrencies();

        converter.SetPricePerUnit(_CryptoCurrency, _Rate);

        double cryptoAmount = converter.getCryptoCurrency(_CryptoCurrency);

        Assert.Equal(_Rate, cryptoAmount);
    }

    [Theory]
    [InlineData("BAI", 700)]
    [InlineData("QAI", 900)]
    public void test_Replace_Previous_Crypto_Rate(string _CryptoCurrency, double _Rate)
    {
        Converter converter = new Converter();
        converter.initCryptoCurrencies();

        converter.SetPricePerUnit(_CryptoCurrency, _Rate);

        double cryptoAmount = converter.getCryptoCurrency(_CryptoCurrency);

        Assert.Equal(_Rate, cryptoAmount);
    }

    [Fact]
    public void test_NonExisting_Crypto_Conversion()
    {
        Converter converter = new Converter();
        converter.initCryptoCurrencies();

        double cryoptoAmount = converter.Convert("Bitcoin", "FAKE", 10);

        Assert.True(cryoptoAmount == 0);
    }

    [Fact]
    public void test_negative_Crypto_Currency_Rate_Conversion_ThrowsArgumentException()
    {
        //arrange
        Converter converter = new Converter();
        converter.initCryptoCurrencies();
        // act & assert
        Assert.Throws<ArgumentException>(() => converter.SetPricePerUnit("SOL", -50));
    }


}