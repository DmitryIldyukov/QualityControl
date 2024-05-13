using Microsoft.EntityFrameworkCore;
using Mountebank.Data.Configurations;
using Mountebank.Data.Interfaces;

namespace Mountebank.Data;

public class AppDbContext : DbContext
{
    public AppDbContext() { }
    
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
        
    }
    
    public virtual DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        new CurrencyConfiguration().Configure(modelBuilder.Entity<Currency>());
    }
}