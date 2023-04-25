using DB;
using DB.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Repositories.InternetBankingRepositories
{
    public interface IRegisterBankAccountRepository
    {
        Task<UserEntity> CheckUserEntityInDb(int userId);
        Task<bool> CheckIbanInDb(RegisterBankAccountDto bankAccount);
        Task<bool> RegisterBankAccountInDb(BankAccountEntity bankAccount);
    }
    public class RegisterBankAccountRepository : IRegisterBankAccountRepository
    {
        private readonly AppDbContext _context;
        
        public RegisterBankAccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> CheckUserEntityInDb(int userId)
        {
            var checkUserEntity = await _context.BankUsers.FirstOrDefaultAsync(
                u => u.UserId == userId);

            return checkUserEntity;
        }

        public async Task<bool> CheckIbanInDb(RegisterBankAccountDto bankAccount)
        {
            var checkIban = await _context.BankAccounts.AnyAsync(b => b.IBAN == bankAccount.IBAN);
            
            return checkIban;
        }

        public async Task<bool> RegisterBankAccountInDb(BankAccountEntity bankAccount)
        {
            await _context.AddAsync(bankAccount);
            await _context.SaveChangesAsync();

            return true;
        }
    }

   
}
