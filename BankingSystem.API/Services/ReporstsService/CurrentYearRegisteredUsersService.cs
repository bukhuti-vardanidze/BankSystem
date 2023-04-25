using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface ICurrentYearRegisteredUsersService
    {
        Task<Dictionary<string, int>> CurrentYearRegisteredUsersQuantity();
    }
    public class CurrentYearRegisteredUsersService : ICurrentYearRegisteredUsersService
    {
        private readonly ICurrentYearRegisteredUsersRepository _currentYearRegisteredUsers;

        public CurrentYearRegisteredUsersService(ICurrentYearRegisteredUsersRepository currentYearRegisteredUsers) 
        {
            _currentYearRegisteredUsers = currentYearRegisteredUsers;
        }
        public async Task<Dictionary<string, int>> CurrentYearRegisteredUsersQuantity()
        {
            try
            {
                var currentYearRegisteredUsersQuantityResult = await _currentYearRegisteredUsers.RegisteredUsers();

                if (currentYearRegisteredUsersQuantityResult.Count == 0)
                {
                    return new Dictionary<string, int>();
                }
                
                return currentYearRegisteredUsersQuantityResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }   
}
