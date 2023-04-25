using DB;
using Microsoft.AspNetCore.Identity;

namespace Repositories.InternetBankRepositories
{
    public interface ILoginRepository
    {
        Task<IdentityUser> CheckUsernameInDb(string email);
        Task<SignInResult> SignIn(IdentityUser user, string password);
        Task<IList<string>> GetUserRoles(IdentityUser user);
    }

    public class LoginRepository : ILoginRepository
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDbContext _context;

        public LoginRepository(UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager,AppDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        public async Task<IdentityUser> CheckUsernameInDb(string email)
        {
            var checkIdentityUser = await _userManager.FindByEmailAsync(email);

            return checkIdentityUser;
        }

        public async Task<SignInResult> SignIn(IdentityUser user, string password)
        {
            var signInUser = await _signInManager.PasswordSignInAsync(user, password, false, false);
            
            return signInUser;
        }

        public async Task<IList<string>> GetUserRoles(IdentityUser user)
        {
            var userRolesList = await _userManager.GetRolesAsync(user);

            return userRolesList;
        }
    }
}
