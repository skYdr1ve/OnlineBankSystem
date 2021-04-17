using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class CardConfiguration : IEntityTypeConfiguration<Card>
    {
        public void Configure(EntityTypeBuilder<Card> builder)
        {
            builder.ToTable(nameof(Card));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Number).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Last4Digits).IsRequired().HasMaxLength(4);
            builder.Property(x => x.PinCode).IsRequired().HasMaxLength(128);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.CardHolderName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ExpireTime).IsRequired();
            builder.Property(x => x.SecurityCode).IsRequired().HasMaxLength(128);

            builder
                .HasOne(x => x.Status)
                .WithMany(x => x.Cards)
                .HasForeignKey(a => a.StatusId);

            builder
                .HasMany(c => c.Transactions)
                .WithOne(t => t.Card)
                .HasForeignKey(t => t.CardId);
        }
    }
}
