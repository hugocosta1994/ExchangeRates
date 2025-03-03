using ExchangeRates.Models;

namespace ExchangeRates.Data.Repository
{
    public interface IUnitOfWorkDb
    {
        IRepository<ExchangeRate> ExchangeRateRepository { get; }

        void Dispose();
        Task<int> SaveChangesAsync();
    }
}