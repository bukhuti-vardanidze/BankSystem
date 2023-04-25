using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface ILastOneYearRegisteredUsersRepository
    {
        Task<Dictionary<string, int>> LastOneYearRegisteredUsers();
    }
    public class LastOneYearRegisteredUsersRepository : ILastOneYearRegisteredUsersRepository
    {
        private readonly AppDbContext _context;

        public LastOneYearRegisteredUsersRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<Dictionary<string, int>> LastOneYearRegisteredUsers()
        {
            Dictionary<string, int> userQuantity = new Dictionary<string, int>();
          
            var lastYear = DateTime.Now.AddYears(-1);
 
            var RegisteredUsersResult = await _context.BankUsers.CountAsync(x => x.RegistrationDate >= lastYear);

            userQuantity.Add(nameof(RegisteredUsersResult),RegisteredUsersResult);
            
            return userQuantity;
        }
    }
}
