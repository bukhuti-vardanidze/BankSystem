using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DB.Entities;

namespace DB
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public DbSet<UserEntity> BankUsers { get; set; }
        public DbSet<BankAccountEntity> BankAccounts { get; set; }
        public DbSet<CardEntity> Cards { get; set; }
        public DbSet<UserTransactionsEntity> UserTransactions { get; set; }
        public DbSet<ExchangeRateEntity> ExchangeRates { get; set; }
        public DbSet<ATMTransactionsEntity> ATMTransactions { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityUserLogin<string>>()
                .HasKey(l => new { l.LoginProvider, l.ProviderKey });

            builder.Entity<IdentityUserRole<string>>()
                .HasKey(r => new { r.UserId, r.RoleId });

            builder.Entity<BankAccountEntity>()
                .HasMany(e => e.SenderTransactions)
                .WithOne(e => e.SenderAccount)
                .HasForeignKey(e => e.SenderAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BankAccountEntity>()
                .HasMany(e => e.RecipientTransactions)
                .WithOne(e => e.RecipientAccount)
                .HasForeignKey(e => e.RecipientAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "1", Name = "Operator", NormalizedName = "OPERATOR" },
                new IdentityRole { Id = "2", Name = "User", NormalizedName = "USER" });

            var operatorUserId = new Guid().ToString();

            builder.Entity<IdentityUser>().HasData(
                new IdentityUser
                {
                    Id = operatorUserId,
                    UserName = "operator@admin.com",
                    NormalizedUserName = "OPERATOR@ADMIN.COM",
                    Email = "operator@admin.com",
                    NormalizedEmail = "OPERATOR@ADMIN.COM",
                    EmailConfirmed = true,
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, "Operator123!")
                }); 

            builder.Entity<IdentityUserRole<string>>()
                .HasData(new IdentityUserRole<string> { UserId = operatorUserId, RoleId = "1" });

            builder.Entity<ExchangeRateEntity>()
                .HasData(
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 1,
                    FromCurrency = Currency.GEL,
                    ToCurrency = Currency.GEL,
                    CurrencyRate = 1,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 2,
                    FromCurrency = Currency.GEL,
                    ToCurrency = Currency.USD,
                    CurrencyRate = 0.361,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 3,
                    FromCurrency = Currency.GEL,
                    ToCurrency = Currency.EUR,
                    CurrencyRate = 0.3636,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 4,
                    FromCurrency = Currency.USD,
                    ToCurrency = Currency.USD,
                    CurrencyRate = 1,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 5,
                    FromCurrency = Currency.USD,
                    ToCurrency = Currency.GEL,
                    CurrencyRate = 2.77,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 6,
                    FromCurrency = Currency.USD,
                    ToCurrency = Currency.EUR,
                    CurrencyRate = 1.007172,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 7,
                    FromCurrency = Currency.EUR,
                    ToCurrency = Currency.EUR,
                    CurrencyRate = 1,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 8,
                    FromCurrency = Currency.EUR,
                    ToCurrency = Currency.GEL,
                    CurrencyRate = 2.75,
                },
                new ExchangeRateEntity()
                {
                    ExchangeCurrencyId = 9,
                    FromCurrency = Currency.EUR,
                    ToCurrency = Currency.USD,
                    CurrencyRate = 0.99275,
                });
        }
    }
}
