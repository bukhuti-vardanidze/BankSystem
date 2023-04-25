using DB;
using DB.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;
using Repositories.InternetBankingRepositories;

namespace Tests.InternetBankingTests
{
    [TestFixture]
    public class ExternalTransactionsRepositoryTests
    {
        private AppDbContext _context;
        private ExternalTransactionsRepository _externalTransactionsRepo;

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
            _externalTransactionsRepo = new ExternalTransactionsRepository(_context);

            // Add some test data to the BankAccounts table
            _context.BankAccounts.Add(new BankAccountEntity
            {
                IBAN = "GE29CDK60161331926819",
                Currency = Currency.GEL,
                Amount = 1000.00
            });

            _context.BankAccounts.Add(new BankAccountEntity
            {
                IBAN = "FR1420041010050500013M02606",
                Currency = Currency.USD,
                Amount = 1000.00
            });

            _context.BankAccounts.Add(new BankAccountEntity
            {
                IBAN = "GB29NWBK60161331926819",
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


        [TestCase("GE29CDK60161331926819", "FR1420041010050500013M02606", 100, 898.5, 1053)]
        [TestCase("GE29CDK60161331926819", "GB29NWBK60161331926819", 100, 898.5, 1036)]

        [TestCase("FR1420041010050500013M02606", "GE29CDK60161331926819", 200, 797.5, 1236)]
        [TestCase("FR1420041010050500013M02606", "GB29NWBK60161331926819", 200, 797.5, 1280)]

        [TestCase("GB29NWBK60161331926819", "GE29CDK60161331926819", 300, 696.5, 1570)]
        [TestCase("GB29NWBK60161331926819", "FR1420041010050500013M02606", 300, 696.5, 1480)]

        public async Task ExternalTransaction_ValidTransaction_Success(
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
            var result = await _externalTransactionsRepo.ExternalTransaction(transaction);

            var senderAccount = _context.BankAccounts.Single(a => a.IBAN == senderIban);
            var recipientAccount = _context.BankAccounts.Single(a => a.IBAN == recipientIban);

            senderAccount.Amount.Should().BeApproximately(expectedSenderAccountAmount, 0.01);
            recipientAccount.Amount.Should().BeApproximately(expectedRecipientAccountAmount, 0.01);
        }
    }
}