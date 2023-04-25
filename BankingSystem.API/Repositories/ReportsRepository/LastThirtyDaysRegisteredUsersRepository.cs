using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface ILastThirtyDaysRegisteredUsersRepository
    {
        Task<Dictionary<string, int>> LastThirtyDaysRegisteredUsers();
    }
    public class LastThirtyDaysRegisteredUsersRepository : ILastThirtyDaysRegisteredUsersRepository
    {
        private readonly AppDbContext _context;

        public LastThirtyDaysRegisteredUsersRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, int>> LastThirtyDaysRegisteredUsers()
        {
            Dictionary<string, int> userQuantity = new Dictionary<string, int>();

            var lastThirtyDay = DateTime.Now.AddMonths(-1);
          
            var RegisteredUsersResult = await _context.BankUsers.CountAsync(x => x.RegistrationDate >= lastThirtyDay);
             
            userQuantity.Add(nameof(RegisteredUsersResult),RegisteredUsersResult);

            return userQuantity;
        }
    }
}
