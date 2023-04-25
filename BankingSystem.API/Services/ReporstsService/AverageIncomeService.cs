using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface IAverageIncomeService
    {
        Task<Dictionary<string, double>> AverageIncome();
    }

    public class AverageIncomeService : IAverageIncomeService
    {
        private readonly IAverageIncomeRepository _averageIncomeRepository;

        public AverageIncomeService(IAverageIncomeRepository averageIncomeRepository)
        {
            _averageIncomeRepository = averageIncomeRepository;
        }

        public async Task<Dictionary<string, double>> AverageIncome()
        {
            try
            {
                var averageIncomeResult = await _averageIncomeRepository.AverageIncome();

                if (averageIncomeResult.Count == 0)
                {
                    return new Dictionary<string, double>();
                }
               
                return averageIncomeResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
