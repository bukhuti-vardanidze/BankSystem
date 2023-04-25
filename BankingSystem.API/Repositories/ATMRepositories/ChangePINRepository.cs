using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ATMRepositories
{
    public interface IChangePINRepository
    {
        Task<CardEntity> CheckCardPINInDb(string PIN);
        Task<CardEntity> CheckCardNumberInDb(string cardNumber);
        Task<string> ChangePIN(string cardNumber, string newPIN);
    }

    public class ChangePINRepository : IChangePINRepository
    {
        private readonly AppDbContext _context;

        public ChangePINRepository(AppDbContext context)
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

        public async Task<string> ChangePIN(string cardNumber, string newPIN)
        {
            var changePIN = await _context.Cards
                .FirstOrDefaultAsync(x => x.CardNumber == cardNumber);

            changePIN.PIN = newPIN;

            await _context.SaveChangesAsync();

            return $"Card with Number: {changePIN.CardNumber} PIN has been changed!";
        }
    }
}
