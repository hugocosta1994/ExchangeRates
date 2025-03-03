using ExchangeRates.Data.Data;
using ExchangeRates.Models;

namespace ExchangeRates.Data.Repository
{
    public class UnitOfWorkDb : IUnitOfWorkDb
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWorkDb(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


        private IRepository<ExchangeRate> _ExchangeRateRepository;
        public IRepository<ExchangeRate> ExchangeRateRepository => _ExchangeRateRepository = _ExchangeRateRepository ?? new Repository<ExchangeRate>(_context);
    }
}
