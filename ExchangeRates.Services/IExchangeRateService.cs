using ExchangeRates.Models;

namespace ExchangeRates.Services
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<ExchangeRate>> GetAllAsync();
        Task<ExchangeRate> GetAsync(string fromCurrency, string toCurrency, bool createIfNotExist = false);
        Task RemoveAsync(Guid id);
    }
}