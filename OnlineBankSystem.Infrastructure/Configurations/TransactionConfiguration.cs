using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ToTable(nameof(Transaction));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Number).IsRequired().HasMaxLength(16);
            builder.Property(x => x.Amount).IsRequired().HasPrecision(18, 2);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.Destination);
            builder.Property(x => x.FromCurrency).IsRequired().HasMaxLength(3);
            builder.Property(x => x.ToCurrency).IsRequired().HasMaxLength(3);
            builder.Property(x => x.ExchangeRate).IsRequired().HasPrecision(18, 2);

            builder
                .HasOne(x => x.ToAccount)
                .WithMany(a => a.ToTransactions)
                .HasForeignKey(x => x.ToAccountId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(x => x.FromAccount)
                .WithMany(a => a.FromTransactions)
                .HasForeignKey(x => x.FromAccountId)
                .OnDelete(DeleteBehavior.NoAction); ;

            builder
                .HasOne(t => t.Card)
                .WithMany(c => c.Transactions)
                .HasForeignKey(t => t.CardId);
        }
    }
}
