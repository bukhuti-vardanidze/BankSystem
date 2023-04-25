using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ATMRepositories
{
    public interface IWithdrawMoneyRepository
    {
        Task<CardEntity> CheckCardPINInDb(string PIN);
        Task<CardEntity> CheckCardNumberInDb(string cardNumber);
        Task<double> ExchangeRate(double amount, string cardNumber);
        Task<bool> WithdrawMoney(double amount, string cardNumber);
        Task<double> CardLimitForOneDay(double amount, string cardNumber);
    } 
    public class WithdrawMoneyRepository : IWithdrawMoneyRepository
    {
        private readonly AppDbContext _context;

        public WithdrawMoneyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CardEntity> CheckCardNumberInDb(string cardNumber)
        {
            var checkCardNumber = await _context.Cards
                .Include(c => c.BankAccountEntity)
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

            return checkCardNumber;
        }

        public async Task<CardEntity> CheckCardPINInDb(string PIN)
        {
            var checkCardPIN = await _context.Cards
                .FirstOrDefaultAsync(x => x.PIN == PIN);

            return checkCardPIN;
        }

        public async Task<double> ExchangeRate(double amount, string cardNumber)
        {
            var card = await _context.Cards
                .FirstOrDefaultAsync(x=>x.CardNumber == cardNumber);

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.BankAccountId == card.BankAccountId);

            var exhangeRate = await _context.ExchangeRates
                .FirstOrDefaultAsync(x => (x.FromCurrency == bankAccount.Currency) &&
                (x.ToCurrency == Currency.GEL));

            return exhangeRate.CurrencyRate;
        }

        public async Task<bool> WithdrawMoney(double amount, string cardNumber)
        {
            var card = await _context.Cards
               .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.BankAccountId == card.BankAccountId);

            bankAccount.Amount -= (amount + (amount * 0.02));

            var atmTransaction = new ATMTransactionsEntity
            {
                Amount = amount,
                TransactionFee = amount * 0.02,
                Currency = bankAccount.Currency,
                TransactionTime= DateTime.Now,
                CardId = card.CardId
            };

            await _context.ATMTransactions.AddAsync(atmTransaction);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<double> CardLimitForOneDay(double amount,string cardNumber)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
            var atmTransaction = await _context.ATMTransactions.Where(x => x.TransactionTime >= DateTime.UtcNow.AddHours(-24) && x.CardId == card.CardId).SumAsync(x => x.Amount + amount);
            
            return atmTransaction;
        }
    }   
}
