using ExchangeRates.Models;

namespace ExchangeRates.Tests.Models
{
    public class ExchangeRateTests
    {
        [Fact]
        public void ExchangeRate_Should_Set_Properties_Correctly()
        {
            Guid guid = Guid.NewGuid();
            // Arrange
            var exchangeRate = new ExchangeRate
            {
                Id = guid,
                FromCurrency = "USD",
                ToCurrency = "EUR",
                Rate = 1.2,
                Ask = 1.3,
                Bid = 1.1
            };

            // Act & Assert
            Assert.Equal(guid.ToString(), exchangeRate.Id.ToString());
            Assert.Equal("USD", exchangeRate.FromCurrency);
            Assert.Equal("EUR", exchangeRate.ToCurrency);
            Assert.Equal(1.2, exchangeRate.Rate);
            Assert.Equal(1.3, exchangeRate.Ask);
            Assert.Equal(1.1, exchangeRate.Bid);
        }


        [Fact]
        public void ToExchangeRateDTO_Should_Return_ExchangeRateDTO()
        {
            // Arrange
            var exchangeRate = new ExchangeRate
            {
                Id = Guid.NewGuid(),
                FromCurrency = "USD",
                ToCurrency = "EUR",
                Rate = 1.2,
                Ask = 1.3,
                Bid = 1.1
            };
            // Act
            var exchangeRateDTO = exchangeRate.ToExchangeRateDTO();
            // Assert
            Assert.Equal(exchangeRate.Id.ToString(), exchangeRateDTO.Id);
            Assert.Equal(exchangeRate.FromCurrency, exchangeRateDTO.FromCurrency);
            Assert.Equal(exchangeRate.ToCurrency, exchangeRateDTO.ToCurrency);
            Assert.Equal(exchangeRate.Rate, exchangeRateDTO.Rate);
            Assert.Equal(exchangeRate.Ask, exchangeRateDTO.Ask);
            Assert.Equal(exchangeRate.Bid, exchangeRateDTO.Bid);
        }

    }
}
