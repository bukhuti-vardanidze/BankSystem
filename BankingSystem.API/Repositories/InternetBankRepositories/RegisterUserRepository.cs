using DB;
using DB.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Repositories.InternetBankRepositories
{
    public interface IRegisterUserRepository
    {
        Task<IdentityUser> CheckIdentityUserInDb(RegisterUserDto userRegistration);
        Task<IdentityResult> RegisterIdentityUser(IdentityUser user);
        Task<bool> RegisterUserEntity(UserEntity user);
        Task<bool> AddUserRole(IdentityUserRole<string> userRole);
        Task<bool> CheckIdNumberInDb(RegisterUserDto userRegistration);

    }

    public class RegisterUserRepository : IRegisterUserRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RegisterUserRepository(AppDbContext context, UserManager<IdentityUser> userManager) 
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IdentityUser> CheckIdentityUserInDb(RegisterUserDto userRegistration)
        {
            var checkIdentityUser = await _userManager.FindByEmailAsync(userRegistration.EmailAddress);

            return checkIdentityUser;
        }

        public async Task<bool> CheckIdNumberInDb(RegisterUserDto userRegistration)
        {
            var checkIdNumber = await _context.BankUsers.AnyAsync(u => u.IDNumber == userRegistration.IDNumber);

            return checkIdNumber;
        }

        public async Task<IdentityResult> RegisterIdentityUser(IdentityUser user)
        {
            var saveIdentityUserToDb = await _userManager.CreateAsync(user);

            return saveIdentityUserToDb;
        }

        public async Task<bool> RegisterUserEntity(UserEntity user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> AddUserRole(IdentityUserRole<string> userRole)
        {
            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();

            return true;
        }

    }

    
}
