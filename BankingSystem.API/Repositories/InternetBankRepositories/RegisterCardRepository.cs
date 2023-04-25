using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories.InternetBankingRepositories
{
    public interface IRegisterCardRepository
    {
        Task<BankAccountEntity> CheckBankAccountInDb(int bankAccountId);
        Task<bool> RegisterCardInDb(CardEntity card);
        Task<string> GetUserFullName(int bankAccountId);
    }
    public class RegisterCardRepository : IRegisterCardRepository
    {
        private readonly AppDbContext _context;

        public RegisterCardRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<BankAccountEntity> CheckBankAccountInDb(int bankAccountId)
        {
            var checkBankAccount = await _context.BankAccounts
                .FirstOrDefaultAsync(b => b.BankAccountId == bankAccountId);

            return checkBankAccount;
        }

        public async Task<string> GetUserFullName(int bankAccountId)
        {
            var result = await _context.BankAccounts
                .Where(b => b.BankAccountId == bankAccountId)
                .Select(b => new
                {
                    b.UserEntity.FirstName,
                    b.UserEntity.LastName,
                })
                .SingleOrDefaultAsync();

            return ($"{result.FirstName} {result.LastName}");
        }

        public async Task<bool> RegisterCardInDb(CardEntity card)
        {
            await _context.AddAsync(card);
            await _context.SaveChangesAsync();

            return true;
        }
    }

    
}
