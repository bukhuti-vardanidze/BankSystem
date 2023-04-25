using DB;
using DB.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repositories.ATMRepositories;

namespace ATMTest
{
    [TestFixture]
    public class WithdrawMoneyRepositoryTest
    {
        private AppDbContext _context;
        private IWithdrawMoneyRepository _withdrawMoneyRepository;

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

            _withdrawMoneyRepository = new WithdrawMoneyRepository(_context);

            // Add some test data to the BankAccounts table
            _context.BankAccounts.Add(new BankAccountEntity
            {
                BankAccountId = 1,
                IBAN = "GE29CDK60161331926819",
                Currency = Currency.GEL,
                Amount = 1000.00
            });

            _context.BankAccounts.Add(new BankAccountEntity
            {
                BankAccountId = 2,
                IBAN = "GE29CDK60161331926527",
                Currency = Currency.USD,
                Amount = 500.00
            });

            _context.BankAccounts.Add(new BankAccountEntity
            {
                BankAccountId = 3,
                IBAN = "GE29CDK60161331924423",
                Currency = Currency.EUR,
                Amount = 250.00
            });

            // Add some test data to the Cards table
            _context.Cards.Add(new CardEntity
            {
                FullName = "Harry Poter",
                CardNumber = "1640893023366711",
                PIN = "1111",
                CVV = "123",
                BankAccountId = 1
            });

            _context.Cards.Add(new CardEntity
            {
                FullName = "Draco Malefoy",
                CardNumber = "2222893023362222",
                PIN = "2222",
                CVV = "123",
                BankAccountId = 2
            });
            _context.Cards.Add(new CardEntity
            {
                FullName = "Hermione Granger",
                CardNumber = "3333893023363333",
                PIN = "3333",
                CVV = "123",
                BankAccountId = 3
            });

            _context.SaveChanges();
        }

        [TestCase("1640893023366711", 100, 898)]
        [TestCase("2222893023362222", 300, 194)]
        [TestCase("3333893023363333", 150, 97)]

        public async Task WithrawMoney_Test(  
           string cardNumber,
           double amount,
           double expectedAmount)
        {
            var result = await _withdrawMoneyRepository.WithdrawMoney(amount, cardNumber);

            var card =_context.Cards
              .Single(x => x.CardNumber == cardNumber);

            var bankAccount = _context.BankAccounts
              .Single(x => x.BankAccountId == card.BankAccountId);

            bankAccount.Amount.Should().BeApproximately(expectedAmount, 0.01);
        }
    }
}
