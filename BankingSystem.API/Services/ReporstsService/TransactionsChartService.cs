using Repositories.DTOs;
using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface ITransactionsChartService
    {
        Task<List<TransactionChartDto>> TransactionsChart();
    }

    public class TransactionsChartService : ITransactionsChartService
    {
        private readonly ITransactionsChartRepository _transactionsChartRepository;

        public TransactionsChartService(ITransactionsChartRepository transactionsChartRepository)
        {
            _transactionsChartRepository = transactionsChartRepository;
        }

        public async Task<List<TransactionChartDto>> TransactionsChart()
        {
            try
            {
                var transactionsChartResult = await _transactionsChartRepository
                .TransactionsChart();

                if (transactionsChartResult.Count == 0)
                {
                    return new List<TransactionChartDto>();
                }

                 return transactionsChartResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
