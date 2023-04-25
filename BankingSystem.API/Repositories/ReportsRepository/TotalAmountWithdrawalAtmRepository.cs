using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface ITotalAmountWithdrawalAtmRepository
    {
        Task<Dictionary<string, double>> TotalWithdrawalAmount();
    }

    public class TotalAmountWithdrawalAtmRepository : ITotalAmountWithdrawalAtmRepository
    {
        private readonly AppDbContext _context;

        public TotalAmountWithdrawalAtmRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, double>> TotalWithdrawalAmount()
        {
            var totalAmount = new Dictionary<string, double>();

            var atmWithdrawalGEL = await _context.ATMTransactions
                .Where(x => x.Currency == DB.Entities.Currency.GEL)
                .SumAsync(x => x.Amount);

            var atmWithdrawalUSD = await _context.ATMTransactions
                .Where(x => x.Currency == DB.Entities.Currency.USD)
                .SumAsync(x => x.Amount);

            var atmWithdrawalEUR = await _context.ATMTransactions
                .Where(x => x.Currency == DB.Entities.Currency.EUR)
                .SumAsync(x => x.Amount);

            totalAmount.Add(nameof(atmWithdrawalGEL), atmWithdrawalGEL);
            totalAmount.Add(nameof(atmWithdrawalUSD), atmWithdrawalUSD);
            totalAmount.Add(nameof(atmWithdrawalEUR), atmWithdrawalEUR);

            return totalAmount;
        }
    }


}
