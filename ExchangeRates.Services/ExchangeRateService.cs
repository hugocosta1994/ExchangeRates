using ExchangeRates.AlphaVantage;
using ExchangeRates.Data.Repository;
using ExchangeRates.Models;

namespace ExchangeRates.Services
{
    /// <summary>
    /// Service for managing exchange rates.
    /// </summary>
    public class ExchangeRateService : IExchangeRateService
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly AlphaVantageService _alphaVantageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExchangeRateService"/> class.
        /// </summary>
        /// <param name="unitOfWorkDb">The unit of work database.</param>
        /// <param name="alphaVantageService">The Alpha Vantage service.</param>
        public ExchangeRateService(IUnitOfWorkDb unitOfWorkDb, AlphaVantageService alphaVantageService)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _alphaVantageService = alphaVantageService;
        }

        /// <summary>
        /// Gets the exchange rate asynchronously.
        /// </summary>
        /// <param name="fromCurrency">The source currency.</param>
        /// <param name="toCurrency">The target currency.</param>
        /// <param name="createIfNotExist">If set to <c>true</c>, creates the exchange rate based on Alpha Vantage values.</param>
        /// <returns>The exchange rate.</returns>
        public async Task<ExchangeRate> GetAsync(string fromCurrency, string toCurrency, bool createIfNotExist = false)
        {
            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();

            var exchangeRateFromDb = await GetExchangeRateAsync(fromCurrency, toCurrency);

            if (createIfNotExist && exchangeRateFromDb == null)
            {
                var response = await _alphaVantageService.GetAsync(fromCurrency, toCurrency);

                if (response == null)
                    return null;

                ExchangeRate exchangeRate = new ExchangeRate
                {
                    FromCurrency = fromCurrency,
                    ToCurrency = toCurrency,
                    Rate = double.Parse(response.RealtimeCurrencyExchangeRate.ExchangeRate),
                    Ask = double.Parse(response.RealtimeCurrencyExchangeRate.AskPrice),
                    Bid = double.Parse(response.RealtimeCurrencyExchangeRate.BidPrice)
                };

                await SaveAsync(exchangeRate);
                exchangeRateFromDb = exchangeRate;
            }

            return exchangeRateFromDb;
        }

        /// <summary>
        /// Gets the exchange rate from the database asynchronously.
        /// </summary>
        /// <param name="fromCurrency">The source currency.</param>
        /// <param name="toCurrency">The target currency.</param>
        /// <returns>The exchange rate.</returns>
        private async Task<ExchangeRate?> GetExchangeRateAsync(string fromCurrency, string toCurrency)
        {
            return await _unitOfWorkDb.ExchangeRateRepository
                .GetFirstOrDefaultAsync(x => x.FromCurrency == fromCurrency && x.ToCurrency == toCurrency);
        }

        /// <summary>
        /// Gets all exchange rates asynchronously.
        /// </summary>
        /// <returns>A collection of exchange rates.</returns>
        public async Task<IEnumerable<ExchangeRate>> GetAllAsync()
        {
            return await _unitOfWorkDb.ExchangeRateRepository.GetAllAsync();
        }

        /// <summary>
        /// Saves the exchange rate asynchronously.
        /// </summary>
        /// <param name="exchangeRate">The exchange rate to save.</param>
        private async Task SaveAsync(ExchangeRate exchangeRate)
        {
            exchangeRate.Created();
            await _unitOfWorkDb.ExchangeRateRepository.AddAsync(exchangeRate);
            await _unitOfWorkDb.SaveChangesAsync();
        }

        /// <summary>
        /// Removes the exchange rate by identifier asynchronously.
        /// </summary>
        /// <param name="id">The identifier of the exchange rate to remove.</param>
        public async Task RemoveAsync(Guid id)
        {
            await _unitOfWorkDb.ExchangeRateRepository.RemoveAsync(id);
            await _unitOfWorkDb.SaveChangesAsync();
        }
    }
}
