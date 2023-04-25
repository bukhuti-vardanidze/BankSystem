using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface ICurrentYearRegisteredUsersRepository
    {
        Task<Dictionary<string, int>> RegisteredUsers();
    }
    public class CurrentYearRegisteredUsersRepository : ICurrentYearRegisteredUsersRepository
    {
        private readonly AppDbContext _context;

        public CurrentYearRegisteredUsersRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Dictionary<string,int>> RegisteredUsers()
        {
            Dictionary<string, int> userQuantity = new Dictionary<string, int>();

            var currentYear = DateTime.Now.Year;
            var RegisteredUsersResult = await _context.BankUsers.CountAsync(x => x.RegistrationDate.Year == currentYear);

            userQuantity.Add(nameof(RegisteredUsersResult), RegisteredUsersResult);

            return userQuantity;
        }
    }
}
