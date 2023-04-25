using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Repositories.ATMRepositories
{
    public interface IShowBalanceRepository
    {
        Task<CardEntity> CheckCardNumberInDb(string cardNumber);
        Task<CardEntity> CheckCardPINInDb(string PIN);
        Task<string> ShowBalance(string cardNumber);
    }
    public class ShowBalanceRepository : IShowBalanceRepository
    {
        private readonly AppDbContext _context;

        public ShowBalanceRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CardEntity> CheckCardNumberInDb(string cardNumber)
        {
            var checkCardNumber = await _context.Cards
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

            return checkCardNumber;
        }

        public async Task<CardEntity> CheckCardPINInDb(string PIN)
        {
            var checkCardPIN = await _context.Cards
                .FirstOrDefaultAsync(x => x.PIN == PIN);

            return checkCardPIN;
        }

        public async Task<string> ShowBalance(string cardNumber)
        {
            var card = await _context.Cards
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

            var bankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(x => x.BankAccountId == card.BankAccountId);

            return $"Your Balance is {bankAccount.Amount} {bankAccount.Currency}";
        }
    }
}
