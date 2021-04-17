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
    public class CardStatusConfiguration : IEntityTypeConfiguration<CardStatus>
    {
        public void Configure(EntityTypeBuilder<CardStatus> builder)
        {
            builder.ToTable(nameof(CardStatus));

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

            builder
                .HasMany(c => c.Cards)
                .WithOne(c => c.Status)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
