using Repositories.ReportsRepository;

namespace Services.ReporstsService
{
    public interface ITotalAmountWithdrawalAtmService
    {
        Task<Dictionary<string, double>> TotalWithdrawalAmount();
    }

    public class TotalAmountWithdrawalAtmService : ITotalAmountWithdrawalAtmService
    {
        private readonly ITotalAmountWithdrawalAtmRepository _totalAmountWithdrawalAtm;

        public TotalAmountWithdrawalAtmService(ITotalAmountWithdrawalAtmRepository totalAmountWithdrawalAtm)
        {
            _totalAmountWithdrawalAtm = totalAmountWithdrawalAtm;
        }

        public async Task<Dictionary<string, double>> TotalWithdrawalAmount()
        {
            try
            {
                var totalWithdrawalResult = await _totalAmountWithdrawalAtm
                    .TotalWithdrawalAmount();

                if (totalWithdrawalResult.Count == 0)
                {
                    return new Dictionary<string, double>();
                }

                return totalWithdrawalResult;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }

    
}
