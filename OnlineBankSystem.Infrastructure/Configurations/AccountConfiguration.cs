using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable(nameof(Account));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Number).IsRequired().HasMaxLength(30);
            builder.Property(x => x.CreatedOn).IsRequired();
            builder.Property(x => x.Balance).IsRequired().HasPrecision(18, 2).HasDefaultValue(0);

            builder.HasOne(p => p.Currency).WithOne().HasForeignKey<CountryCurrencyCode>(p => p.Number);

            builder
                .HasOne(x => x.User)
                .WithMany(u => u.Accounts)
                .HasForeignKey(x => x.UserId);

            builder
                .HasOne(x => x.Currency)
                .WithMany(x => x.Accounts)
                .HasForeignKey(a => a.CurrencyId);

            builder
                .HasOne(x => x.Status)
                .WithMany(x => x.Accounts)
                .HasForeignKey(a => a.StatusId);

            builder
                .HasMany(a => a.Cards)
                .WithOne(c => c.Account)
                .HasForeignKey(c => c.AccountId)
                .OnDelete(DeleteBehavior.ClientNoAction);

            builder
                .HasMany(x => x.FromTransactions)
                .WithOne(t => t.FromAccount)
                .HasForeignKey(x => x.FromAccountId);

            builder
                .HasMany(x => x.ToTransactions)
                .WithOne(t => t.ToAccount)
                .HasForeignKey(x => x.ToAccountId);
        }
    }
}
