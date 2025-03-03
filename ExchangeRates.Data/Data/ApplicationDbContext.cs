using ExchangeRates.Models;
using Microsoft.EntityFrameworkCore;

namespace ExchangeRates.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
     
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }


        public DbSet<ExchangeRate> ExchangeRates { get; set; }

    }
}
