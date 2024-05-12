using Microsoft.EntityFrameworkCore;
using Mountebank.Data.Configurations;

namespace Mountebank.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        
    }
    
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        new CurrencyConfiguration().Configure(modelBuilder.Entity<Currency>());
    }
}