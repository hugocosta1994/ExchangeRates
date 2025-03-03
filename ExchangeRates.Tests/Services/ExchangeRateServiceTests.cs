using ExchangeRates.AlphaVantage;
using ExchangeRates.Data.Repositories;
using ExchangeRates.Data.Repository;
using ExchangeRates.Models;
using ExchangeRates.Services;
using Moq;

namespace ExchangeRates.Tests.Services
{
    public class ExchangeRateServiceTests
    {
        private Mock<IUnitOfWorkDb> _mockUnitOfWork;
        private Mock<AlphaVantageService> _mockAlphaVantageService;
        private readonly ExchangeRateService _service;

        public ExchangeRateServiceTests()
        {
            this._mockUnitOfWork = new Mock<IUnitOfWorkDb>();
            this._mockAlphaVantageService = new Mock<AlphaVantageService>("demo");
            this._service = new ExchangeRateService(this._mockUnitOfWork.Object, _mockAlphaVantageService.Object);
        }

        [Fact]
        public async Task GetAllAsync_Should_Return_All_ExchangeRates()
        {
            // Arrange
            var exchangeRates = new List<ExchangeRate>
                    {
                        new ExchangeRate { Id = Guid.NewGuid(), FromCurrency = "USD", ToCurrency = "EUR", Rate = 1.2, Ask = 1.3, Bid = 1.1 },
                        new ExchangeRate { Id = Guid.NewGuid(), FromCurrency = "USD", ToCurrency= "GBP", Rate = 0.8, Ask = 0.85, Bid = 0.75 }
                    };

            //this._mockUnitOfWork.Setup(repo => repo.ExchangeRateRepository.GetAllAsync(null, null, null)).ReturnsAsync(exchangeRates);
            this._mockUnitOfWork
                .Setup(repo => repo.ExchangeRateRepository.GetAllAsync(null, null, null))
                .ReturnsAsync(exchangeRates);

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.Equal(2, result.Count());
            Assert.Equal("EUR", result.ElementAt(0).ToCurrency);
            Assert.Equal("GBP", result.ElementAt(1).ToCurrency);
        }

        [Fact]
        public async Task GetAsync_Should_Return_ExchangeRate()
        {
            // Arrange
            var exchangeRate = new ExchangeRate { Id = Guid.NewGuid(), FromCurrency = "USD", ToCurrency = "EUR", Rate = 1.2, Ask = 1.3, Bid = 1.1 };
            this._mockUnitOfWork
                .Setup(repo => repo.ExchangeRateRepository.GetFirstOrDefaultAsync(x => x.FromCurrency == "USD" && x.ToCurrency == "EUR", null))
                .ReturnsAsync(exchangeRate);

            // Act
            var result = await _service.GetAsync("USD", "EUR");
            var result2 = await _service.GetAsync("usd", "eur");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("USD", result.FromCurrency);
            Assert.Equal("EUR", result.ToCurrency);


            Assert.NotNull(result2);
            Assert.Equal("USD", result2.FromCurrency);
            Assert.Equal("EUR", result2.ToCurrency);
        }

        [Fact]
        public async Task RemoveAsync_Should_Call_Repository_Remove()
        {
            // Arrange
            Guid guid = Guid.NewGuid();
            var exchangeRate = new ExchangeRate { Id = guid, FromCurrency = "USD", ToCurrency = "EUR", Rate = 1.2, Ask = 1.3, Bid = 1.1 };
            this._mockUnitOfWork
                .Setup(repo => repo.ExchangeRateRepository.GetAsync(It.IsAny<Guid>()))
                .ReturnsAsync(exchangeRate);

            // Act
            await _service.RemoveAsync(guid);

            // Assert
            this._mockUnitOfWork
                .Verify(repo => repo.ExchangeRateRepository.RemoveAsync(guid), Times.Once);
        }
    }
}
