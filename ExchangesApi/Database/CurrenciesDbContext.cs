using ExchangesApi.Database.Models;
using Microsoft.EntityFrameworkCore;
using User = ExchangesApi.Database.Models.User;

namespace ExchangesApi.Database;
public class CurrenciesDbContext : DbContext
{
    public CurrenciesDbContext(DbContextOptions<CurrenciesDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Currency> Currencies { get; set; }
}