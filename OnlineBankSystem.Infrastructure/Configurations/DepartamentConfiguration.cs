using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineBankSystem.Core.Entities;

namespace OnlineBankSystem.Infrastructure.Configurations
{
    public class DepartamentConfiguration : IEntityTypeConfiguration<Departament>
    {
        public void Configure(EntityTypeBuilder<Departament> builder)
        {
            builder.ToTable(nameof(Departament));

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id);

            builder.Property(x => x.Address).IsRequired();
        }
    }
}
