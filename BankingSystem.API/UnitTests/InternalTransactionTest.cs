using DB;
using DB.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;
using Repositories.InternetBankingRepositories;

namespace Tests.InternetBankingTests
{
    [TestFixture]
    public class InternalTransactionsRepositoryTests
    {
        private AppDbContext _context;
        private IInternalTransactionsRepository _internalTransactionsRepo;

        [SetUp]
        public void Setup()
        {
            // Create a new in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CredoBank")
                .Options;
            _context = new AppDbContext(options);
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            // Initialize the InternalTransactionsRepository with the test database context
            _internalTransactionsRepo = new InternalTransactionsRepository(_context);

            // Add some test data to the BankAccounts table
            _context.BankAccounts.Add(new BankAccountEntity
            {
                IBAN = "GE29CDK60161331926819",
                Currency = Currency.GEL,
                Amount = 1000.00
            });

            _context.BankAccounts.Add(new BankAccountEntity
            {
                IBAN = "GE29CDK60161331926527",
                Currency = Currency.USD,
                Amount = 1000.00
            });

            _context.BankAccounts.Add(new BankAccountEntity
            {
                IBAN = "GE29CDK60161331924423",
                Currency = Currency.EUR,
                Amount = 1000.00
            });

            // Add exchange rates to the in-memory database for testing
            var exchangeRates = new List<ExchangeRateEntity>
            {
                new ExchangeRateEntity
                {
                    FromCurrency = Currency.GEL,
                    ToCurrency = Currency.USD,
                    CurrencyRate = 0.53
                },
                new ExchangeRateEntity
                {
                    FromCurrency = Currency.USD,
                    ToCurrency = Currency.GEL,
                    CurrencyRate = 1.18
                },
                new ExchangeRateEntity
                {
                    FromCurrency = Currency.GEL,
                    ToCurrency = Currency.EUR,
                    CurrencyRate = 0.36
                },
                new ExchangeRateEntity
                {
                    FromCurrency = Currency.EUR,
                    ToCurrency = Currency.GEL,
                    CurrencyRate = 1.9
                },
                new ExchangeRateEntity
                {
                    FromCurrency = Currency.USD,
                    ToCurrency = Currency.EUR,
                    CurrencyRate = 1.4
                },
                 new ExchangeRateEntity
                {
                    FromCurrency = Currency.EUR,
                    ToCurrency = Currency.USD,
                    CurrencyRate = 1.6
                }
            };

            var allRates = _context.ExchangeRates.ToList();
            _context.ExchangeRates.RemoveRange(allRates);

            _context.ExchangeRates.AddRange(exchangeRates);
            _context.SaveChanges();
        }

       
        [TestCase("GE29CDK60161331926819", "GE29CDK60161331926527", 250, 750, 1132.5)]
        [TestCase("GE29CDK60161331926819", "GE29CDK60161331924423", 250, 750, 1090)]

        [TestCase("GE29CDK60161331926527", "GE29CDK60161331926819", 150, 850, 1177)]
        [TestCase("GE29CDK60161331926527", "GE29CDK60161331924423", 150, 850, 1210)]

        [TestCase("GE29CDK60161331924423", "GE29CDK60161331926819", 150, 850, 1285)]
        [TestCase("GE29CDK60161331924423", "GE29CDK60161331926527", 150, 850, 1240)]

        public async Task InternalTransaction_ValidTransaction_Success(
            string senderIban,
            string recipientIban,
            double amount,
            double expectedSenderAccountAmount,
            double expectedRecipientAccountAmount)
        {
            var transaction = new TransactionDto
            {
                SenderIBAN = senderIban,
                RecipientIBAN = recipientIban,
                Amount = amount
            };
            var result = await _internalTransactionsRepo.InternalTransaction(transaction);

            var senderAccount = _context.BankAccounts.Single(a => a.IBAN == senderIban);
            var recipientAccount = _context.BankAccounts.Single(a => a.IBAN == recipientIban);

            senderAccount.Amount.Should().BeApproximately(expectedSenderAccountAmount, 0.01);
            recipientAccount.Amount.Should().BeApproximately(expectedRecipientAccountAmount, 0.01);
        }
    }
}

