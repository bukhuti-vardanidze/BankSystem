using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Repositories.InternetBankingRepositories
{
    public interface IExternalTransactionsRepository
    {
        Task<BankAccountEntity> CheckIBANInDb(string IBAN);
        Task<bool> ExternalTransaction(TransactionDto Transaction);
        Task<BankAccountEntity> CheckIBanForUser(string userId, string IBAN);
    }

    public class ExternalTransactionsRepository : IExternalTransactionsRepository
    {
        private readonly AppDbContext _context;

        public ExternalTransactionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccountEntity> CheckIBANInDb(string IBAN)
        {
            var checkIban = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.IBAN == IBAN);

            return checkIban;
        }

        public async Task<BankAccountEntity> CheckIBanForUser(string userId, string IBAN)
        {
            var checkUser = await _context.BankUsers
                .FirstOrDefaultAsync(x => x.Id == userId);

            var checkIbanForUser = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.UserId == checkUser.UserId &&
                x.IBAN == IBAN);

            return checkIbanForUser;
        }

        public async Task<bool> ExternalTransaction(TransactionDto transaction)
        {
            var SenderBankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.IBAN == transaction.SenderIBAN);

            SenderBankAccount.Amount -= (transaction.Amount +
                (transaction.Amount * 0.01 + 0.5));

            var recepientBankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.IBAN == transaction.RecipientIBAN);

            var exchangeRate = await _context.ExchangeRates
                .FirstOrDefaultAsync(x =>
                (x.FromCurrency == SenderBankAccount.Currency) &&
                (x.ToCurrency == recepientBankAccount.Currency));

            recepientBankAccount.Amount += transaction.Amount * exchangeRate.CurrencyRate;


            var changesInTransactionDb = new UserTransactionsEntity()
            {
                SenderAccountId = SenderBankAccount.BankAccountId,
                SenderCurrency = SenderBankAccount.Currency,
                SenderAmount = transaction.Amount,
                RecipientAccountId = recepientBankAccount.BankAccountId,
                RecipientCurrency = recepientBankAccount.Currency,
                RecipientAmount = transaction.Amount * exchangeRate.CurrencyRate,
                TransactionType = TransactionType.External,
                TransactionFee = transaction.Amount * 0.01 + 0.5,
                TransactionTime = DateTime.Now,
                ExchangeRate = exchangeRate.CurrencyRate
            };

            await _context.AddAsync(changesInTransactionDb);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
