using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface ILastOneYearRegisteredUsersService
    {
        Task<Dictionary<string, int>> LastOneYearRegisteredUsersQuantity();
    }
    public class LastOneYearRegisteredUsersService : ILastOneYearRegisteredUsersService
    {
        private readonly ILastOneYearRegisteredUsersRepository _lastOneYearRegisteredUsersRepository;

        public LastOneYearRegisteredUsersService(ILastOneYearRegisteredUsersRepository lastOneYearRegisteredUsersRepository) 
        {
            _lastOneYearRegisteredUsersRepository = lastOneYearRegisteredUsersRepository;
        }

        public async Task<Dictionary<string, int>> LastOneYearRegisteredUsersQuantity()
        {
            try
            { 
                var lastOneYearRegisteredUsersQuantityResult = await _lastOneYearRegisteredUsersRepository.LastOneYearRegisteredUsers();

                if (lastOneYearRegisteredUsersQuantityResult.Count == 0)
                {
                    return new Dictionary<string, int>();
                }
                
                return lastOneYearRegisteredUsersQuantityResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
