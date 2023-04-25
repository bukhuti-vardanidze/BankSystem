using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface ILastThirtyDaysRegisteredUsersService
    {
        Task<Dictionary<string, int>> LastThirtyDaysRegisteredUsersQuantity();
    }
    public class LastThirtyDaysRegisteredUsersService : ILastThirtyDaysRegisteredUsersService
    {
        private readonly ILastThirtyDaysRegisteredUsersRepository _lastThirtyDaysRegisteredUsers;

        public LastThirtyDaysRegisteredUsersService(ILastThirtyDaysRegisteredUsersRepository lastThirtyDaysRegisteredUsers) 
        {
            _lastThirtyDaysRegisteredUsers = lastThirtyDaysRegisteredUsers;
        }
        public async Task<Dictionary<string, int>> LastThirtyDaysRegisteredUsersQuantity()
        {
            try
            {
                var lastThirtyDayRegisteredUsersQuantityResult = await _lastThirtyDaysRegisteredUsers.LastThirtyDaysRegisteredUsers();

                if (lastThirtyDayRegisteredUsersQuantityResult.Count == 0)
                {
                    return new Dictionary<string, int>();
                }

                return lastThirtyDayRegisteredUsersQuantityResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
           
        }
    }
}
