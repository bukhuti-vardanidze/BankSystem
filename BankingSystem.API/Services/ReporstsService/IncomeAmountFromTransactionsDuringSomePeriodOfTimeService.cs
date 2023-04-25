using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface IIncomeAmountFromTransactionsDuringSomePeriodOfTimeService
    {
        Task<Dictionary<string, double>> IncomeAmountFromTransactionsDuringSomePeriodOfTime();
    }

    public class IncomeAmountFromTransactionsDuringSomePeriodOfTimeService :
        IIncomeAmountFromTransactionsDuringSomePeriodOfTimeService
    {
        private readonly IIncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository _incomeAmount;

        public IncomeAmountFromTransactionsDuringSomePeriodOfTimeService(
            IIncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository incomeAmount)
        {
            _incomeAmount = incomeAmount;
        }

        public async Task<Dictionary<string, double>> IncomeAmountFromTransactionsDuringSomePeriodOfTime()
        {
            try
            {
                var IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult = await _incomeAmount
                    .IncomeAmountFromTransactionsDuringSomePeriodOfTime();

                if (IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult.Count == 0)
                {
                    return new Dictionary<string, double>();
                }

                return IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
