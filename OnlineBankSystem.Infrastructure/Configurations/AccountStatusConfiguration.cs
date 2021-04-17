using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class AccountStatusConfiguration : IEntityTypeConfiguration<AccountStatus>
    {
        public void Configure(EntityTypeBuilder<AccountStatus> builder)
        {
            builder.ToTable(nameof(AccountStatus));

            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name).IsRequired().HasMaxLength(128);

            builder
                .HasMany(c => c.Accounts)
                .WithOne(c => c.Status)
                .HasForeignKey(c => c.StatusId)
                .OnDelete(DeleteBehavior.ClientNoAction);
        }
    }
}
