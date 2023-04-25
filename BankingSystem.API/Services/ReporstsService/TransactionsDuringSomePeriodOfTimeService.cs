using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface ITransactionsDuringSomePeriodOfTimeService
    {
        Task<Dictionary<string, int>> TransactionsDuringSomePeriodOfTime();
    }
    public class TransactionsDuringSomePeriodOfTimeService : ITransactionsDuringSomePeriodOfTimeService
    {
        private readonly ITransactionsDuringSomePeriodOfTimeRepository _transactionsDuringSomePeriodOfTime;

        public TransactionsDuringSomePeriodOfTimeService(ITransactionsDuringSomePeriodOfTimeRepository transactionsDuringSomePeriodOfTime) 
        {
            _transactionsDuringSomePeriodOfTime = transactionsDuringSomePeriodOfTime;
        }

        public async Task<Dictionary<string, int>> TransactionsDuringSomePeriodOfTime()
        {
            try
            {
                var transactionsDuringSomePeriodOfTimeQuantityResult = await _transactionsDuringSomePeriodOfTime.TransactionsDuringSomePeriodOfTime();

                if (transactionsDuringSomePeriodOfTimeQuantityResult.Count == 0)
                {
                    return new Dictionary<string, int>();
                }

                return transactionsDuringSomePeriodOfTimeQuantityResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
