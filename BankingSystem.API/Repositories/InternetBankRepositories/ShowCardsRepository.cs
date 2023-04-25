using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.InternetBankingRepositories
{
    public interface IShowCardsRepository
    {
        Task<List<CardEntity>> ShowCards(string userId);
    }

    public class ShowCardsRepository : IShowCardsRepository
    {
        private readonly AppDbContext _context;

        public ShowCardsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CardEntity>> ShowCards(string userId)
        {
            var user = await _context.BankUsers.FirstOrDefaultAsync(u => u.Id == userId);

            var bankAccounts = await _context.BankAccounts
                            .Where(u => u.UserId == user.UserId)
                            .ToListAsync();

            var cards = await _context.Cards
                .Where(c => bankAccounts.Select(b => b.BankAccountId)
                .Contains(c.BankAccountId))
                .ToListAsync();

            return cards;

        }
    }
}
