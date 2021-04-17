using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class CountryCurrencyCodeConfiguration : IEntityTypeConfiguration<CountryCurrencyCode>
    {
        public void Configure(EntityTypeBuilder<CountryCurrencyCode> builder)
        {
            builder.ToTable(nameof(CountryCurrencyCode));

            builder.HasKey(x => x.Number);

            builder.Property(e => e.Number);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(3);
            builder.Property(x => x.Currency).IsRequired();
            builder.Property(x => x.Country).IsRequired();

            builder
                .HasMany(c => c.Accounts)
                .WithOne(a => a.Currency)
                .HasForeignKey(a => a.CurrencyId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
