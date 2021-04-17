using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class TransactionStatusConfiguration : IEntityTypeConfiguration<TransactionStatus>
    {
        public void Configure(EntityTypeBuilder<TransactionStatus> builder)
        {
            builder.ToTable(nameof(TransactionStatus));

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

            builder
                .HasMany(c => c.Transactions)
                .WithOne(c => c.Status)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
