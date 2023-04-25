using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface ITransactionsDuringSomePeriodOfTimeRepository
    {
        Task<Dictionary<string, int>> TransactionsDuringSomePeriodOfTime();
    }
    public class TransactionsDuringSomePeriodOfTimeRepository : ITransactionsDuringSomePeriodOfTimeRepository
    {
        private readonly AppDbContext _context;

        public TransactionsDuringSomePeriodOfTimeRepository(AppDbContext context) 
        {
            _context = context;
        }

        public async Task<Dictionary<string,int>> TransactionsDuringSomePeriodOfTime()
        {
            Dictionary<string, int> transactionQuantity = new Dictionary<string, int>();
           
            var currentDate = DateTime.Now;
            var lastOneMonth = currentDate.AddMonths(-1);
            var lastSixMonth = currentDate.AddMonths(-6);
            var lastOneYear = currentDate.AddYears(-1);

            //Internal
            var lastOneMonthInternalTransactionsQuantity =await _context.UserTransactions.CountAsync(t => t.TransactionTime >= lastOneMonth && t.TransactionTime <= currentDate && t.TransactionType == DB.Entities.TransactionType.Internal);
            var lastSixMonthInternalTransactionsQuantity = await _context.UserTransactions.CountAsync(t => t.TransactionTime >= lastSixMonth && t.TransactionTime <= currentDate && t.TransactionType == DB.Entities.TransactionType.Internal);
            var lastOneYearTInternalransactionsQuantity = await _context.UserTransactions.CountAsync(t => t.TransactionTime >= lastOneYear && t.TransactionTime <= currentDate && t.TransactionType == DB.Entities.TransactionType.Internal);
            
            //External
            var lastOneMonthExternalTransactionsQuantity = await _context.UserTransactions.CountAsync(t => t.TransactionTime >= lastOneMonth && t.TransactionTime <= currentDate && t.TransactionType == DB.Entities.TransactionType.External);
            var lastSixMonthExternalTransactionsQuantity = await _context.UserTransactions.CountAsync(t => t.TransactionTime >= lastSixMonth && t.TransactionTime <= currentDate && t.TransactionType == DB.Entities.TransactionType.External);
            var lastOneYearExternalTransactionsQuantity = await _context.UserTransactions.CountAsync(t => t.TransactionTime >= lastOneYear && t.TransactionTime <= currentDate && t.TransactionType == DB.Entities.TransactionType.External);

            //ATM
            var lastOneMonthTransactionsQuantityATM = await _context.ATMTransactions.CountAsync(t => t.TransactionTime >= lastOneMonth && t.TransactionTime <= currentDate);
            var lastSixMonthTransactionsQuantityATM = await _context.ATMTransactions.CountAsync(t => t.TransactionTime >= lastSixMonth && t.TransactionTime <= currentDate);
            var lastOneYearTransactionsQuantityATM = await _context.ATMTransactions.CountAsync(t => t.TransactionTime >= lastOneYear && t.TransactionTime <= currentDate);

            //Total
            var totallastOneMonthTransactionQuauntity = lastOneMonthInternalTransactionsQuantity + lastOneMonthExternalTransactionsQuantity + lastOneMonthTransactionsQuantityATM;
            var totallastSixMonthTransactionQuauntity = lastSixMonthInternalTransactionsQuantity + lastSixMonthExternalTransactionsQuantity + lastSixMonthTransactionsQuantityATM;
            var totallastOneYearTransactionQuauntity = lastOneYearTInternalransactionsQuantity + lastOneYearExternalTransactionsQuantity + lastOneYearTransactionsQuantityATM;

            transactionQuantity.Add(nameof(lastOneMonthInternalTransactionsQuantity), lastOneMonthInternalTransactionsQuantity);
            transactionQuantity.Add(nameof(lastSixMonthInternalTransactionsQuantity), lastSixMonthInternalTransactionsQuantity);
            transactionQuantity.Add(nameof(lastOneYearTInternalransactionsQuantity), lastOneYearTInternalransactionsQuantity);
            transactionQuantity.Add(nameof(lastOneMonthExternalTransactionsQuantity), lastOneMonthExternalTransactionsQuantity);
            transactionQuantity.Add(nameof(lastSixMonthExternalTransactionsQuantity), lastSixMonthExternalTransactionsQuantity);
            transactionQuantity.Add(nameof(lastOneYearExternalTransactionsQuantity), lastOneYearExternalTransactionsQuantity);
            transactionQuantity.Add(nameof(lastOneMonthTransactionsQuantityATM), lastOneMonthTransactionsQuantityATM);
            transactionQuantity.Add(nameof(lastSixMonthTransactionsQuantityATM), lastSixMonthTransactionsQuantityATM);
            transactionQuantity.Add(nameof(lastOneYearTransactionsQuantityATM), lastOneYearTransactionsQuantityATM);
            transactionQuantity.Add(nameof(totallastOneMonthTransactionQuauntity), totallastOneMonthTransactionQuauntity);
            transactionQuantity.Add(nameof(totallastSixMonthTransactionQuauntity), totallastSixMonthTransactionQuauntity);
            transactionQuantity.Add(nameof(totallastOneYearTransactionQuauntity), totallastOneYearTransactionQuauntity);

            return transactionQuantity;
        }
    }
}
