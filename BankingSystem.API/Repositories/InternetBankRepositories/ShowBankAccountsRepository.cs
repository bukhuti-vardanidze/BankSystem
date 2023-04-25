using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.InternetBankingRepositories
{
    public interface IShowBankAccountsRepository
    {
        Task<List<BankAccountEntity>> BankAccounts(string userId);
    }

    public class ShowBankAccountsRepository : IShowBankAccountsRepository
    {
        private readonly AppDbContext _context;

        public ShowBankAccountsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BankAccountEntity>> BankAccounts(string userId)
        {
            var user = await _context.BankUsers.FirstOrDefaultAsync(u => u.Id == userId);

            var bankAccounts = await _context.BankAccounts
                .Where(u => u.UserId == user.UserId)
                .ToListAsync();

            return bankAccounts;

        }
    }
}
