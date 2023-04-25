using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Repositories.InternetBankingRepositories
{
    public interface IInternalTransactionsRepository
    {
        Task<BankAccountEntity> CheckIBANInDb(string IBAN);
        Task<bool> InternalTransaction(TransactionDto transaction);
    }
    public class InternalTransactionsRepository : IInternalTransactionsRepository
    {
        private readonly AppDbContext _context;
       
        public InternalTransactionsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<BankAccountEntity> CheckIBANInDb(string IBAN)
        {
            var checkIban = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.IBAN == IBAN);

            return checkIban;
        }

        public async Task<bool> InternalTransaction(TransactionDto transaction)
        {
            var SenderBankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.IBAN == transaction.SenderIBAN);

            SenderBankAccount.Amount -= transaction.Amount;

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
                TransactionType = TransactionType.Internal,
                TransactionFee = 0,
                TransactionTime = DateTime.Now,
                ExchangeRate = exchangeRate.CurrencyRate
            };

            await _context.AddAsync(changesInTransactionDb);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}