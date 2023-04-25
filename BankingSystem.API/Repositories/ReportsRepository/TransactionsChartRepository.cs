using DB;
using Microsoft.EntityFrameworkCore;
using Repositories.DTOs;

namespace Repositories.ReportsRepository
{
    public interface ITransactionsChartRepository
    {
        Task<List<TransactionChartDto>> TransactionsChart();
    }

    public class TransactionsChartRepository : ITransactionsChartRepository
    {
        private readonly AppDbContext _context;

        public TransactionsChartRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<TransactionChartDto>> TransactionsChart()
        {
            var oneMonthAgo = DateTime.Now.AddMonths(-1);

            var userTransactionChart = await _context.UserTransactions
                .Where(t => t.TransactionTime >= oneMonthAgo)
                .GroupBy(t => t.TransactionTime.Date)
                .Select(t => new TransactionChartDto
                {
                    Date = t.Key,
                    Count = t.Count()
                })
                .ToListAsync();

            var atmTransactionChart = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= oneMonthAgo)
                .GroupBy(t => t.TransactionTime.Date)
                .Select(t => new TransactionChartDto
                {
                    Date = t.Key,
                    Count = t.Count()
                })
                .ToListAsync();

            var totalTransactionChart = userTransactionChart
                .Concat(atmTransactionChart)
                .GroupBy(t => t.Date)
                .Select(g => new TransactionChartDto
                {
                    Date = g.Key,
                    Count = g.Sum(t => t.Count)
                })
                .ToList();

            return totalTransactionChart;
        }
    }
}
