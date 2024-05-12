using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Mountebank.Data.Configurations;

public class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.ToTable("currencies");
        builder.Property(_ => _.Id).IsRequired();
        builder.Property(_ => _.Name).IsRequired();
        builder.Property(_ => _.Rate).IsRequired();
        builder.HasData(new List<Currency>()
        {
            new ()
            {
                Id = 1,
                Name = "RUB",
                Rate = 1.0,
            },
            new ()
            {
                Id = 2,
                Name = "USD",
                Rate = 73.4419
            },
            new ()
            {
                Id = 3,
                Name = "EUR",
                Rate = 79.7264,
            }
        });
    }
}