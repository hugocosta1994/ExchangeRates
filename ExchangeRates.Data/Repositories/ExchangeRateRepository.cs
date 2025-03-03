using ExchangeRates.Data.Data;
using ExchangeRates.Data.Repository;
using ExchangeRates.Models;

namespace ExchangeRates.Data.Repositories
{
    public class ExchangeRateRepository : Repository<ExchangeRate>
    {
        private readonly ApplicationDbContext _context;
        public ExchangeRateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
