using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using OnlineBankSystem.Core.Entities;
using OnlineBankSystem.Infrastructure.Configurations;

namespace OnlineBankSystem.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<CountryCurrencyCode> Currencies { get; set; }
        public DbSet<AccountStatus> AccountStatuses { get; set; }
        public DbSet<CardStatus> CardStatuses { get; set; }
        public DbSet<TransactionStatus> TransactionStatuses { get; set; }
        public DbSet<Departament> Departament { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserConfiguration());
            builder.ApplyConfiguration(new AccountConfiguration());
            builder.ApplyConfiguration(new CardConfiguration());
            builder.ApplyConfiguration(new TransactionConfiguration());
            builder.ApplyConfiguration(new CountryCurrencyCodeConfiguration());
            builder.ApplyConfiguration(new AccountStatusConfiguration());
            builder.ApplyConfiguration(new CardStatusConfiguration());
            builder.ApplyConfiguration(new TransactionStatusConfiguration());
            builder.ApplyConfiguration(new DepartamentConfiguration());
        }
    }
}
