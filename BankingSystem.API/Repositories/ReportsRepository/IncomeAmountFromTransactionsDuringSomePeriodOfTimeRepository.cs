using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface IIncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository
    {
        Task<Dictionary<string, double>> IncomeAmountFromTransactionsDuringSomePeriodOfTime();
    }

    public class IncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository :
        IIncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository
    {
        private readonly AppDbContext _context;

        public IncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, double>> IncomeAmountFromTransactionsDuringSomePeriodOfTime()
        {
            Dictionary<string, double> incomeAmount = new Dictionary<string, double>();

            var currentDate = DateTime.Now;
            var lastOneMonth = currentDate.AddMonths(-1);
            var lastSixMonth = currentDate.AddMonths(-6);
            var lastOneYear = currentDate.AddYears(-1);

            //Internal GEL
            var lastOneMonthInternalTransactionsQuantityInGel = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneMonth && 
                t.TransactionTime <= currentDate && 
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthInternalTransactionsQuantityInGel = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastSixMonth && 
                t.TransactionTime <= currentDate && 
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee); 

            var lastOneYearInternalTransactionsQuantityInGel = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneYear && 
                t.TransactionTime <= currentDate && 
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);

            //InternalUSD
            var lastOneMonthInternalTransactionsQuantityInUsd = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthInternalTransactionsQuantityInUsd = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastSixMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearInternalTransactionsQuantityInUsd = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneYear &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            //Internal Euro
            var lastOneMonthInternalTransactionsQuantityInEur = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthInternalTransactionsQuantityInEur = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastSixMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearInternalTransactionsQuantityInEur = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneYear &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.Internal &&
                t.SenderCurrency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);


            //External GEL
            var lastOneMonthExternalTransactionsQuantityInGel = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneMonth && 
                t.TransactionTime <= currentDate && 
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee); 
            
            var lastSixMonthExternalTransactionsQuantityInGel = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastSixMonth && 
                t.TransactionTime <= currentDate && 
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearExternalTransactionsQuantityInGel = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneYear && 
                t.TransactionTime <= currentDate && 
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);
            
            //External USD
            var lastOneMonthExternalTransactionsQuantityInUsd = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthExternalTransactionsQuantityInUsd = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastSixMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearExternalTransactionsQuantityInUsd = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneYear &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            //external Euro
            var lastOneMonthExternalTransactionsQuantityInEur = await _context.UserTransactions
               .Where(t => t.TransactionTime >= lastOneMonth &&
               t.TransactionTime <= currentDate &&
               t.TransactionType == DB.Entities.TransactionType.External &&
               t.SenderCurrency == DB.Entities.Currency.EUR)
               .SumAsync(t => t.TransactionFee);

            var lastSixMonthExternalTransactionsQuantityInEur = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastSixMonth &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearExternalTransactionsQuantityInEur = await _context.UserTransactions
                .Where(t => t.TransactionTime >= lastOneYear &&
                t.TransactionTime <= currentDate &&
                t.TransactionType == DB.Entities.TransactionType.External &&
                t.SenderCurrency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);


            //ATM in GEL
            var lastOneMonthTransactionsQuantityATMInGel = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastOneMonth && 
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthTransactionsQuantityATMInGel = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastSixMonth && 
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearTransactionsQuantityATMInGel = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastOneYear && 
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.GEL)
                .SumAsync(t => t.TransactionFee);

            //ATM in USD
            var lastOneMonthTransactionsQuantityATMInUsd = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastOneMonth &&
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthTransactionsQuantityATMInUsd = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastSixMonth &&
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearTransactionsQuantityATMInUsd = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastOneYear &&
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.USD)
                .SumAsync(t => t.TransactionFee);

            //ATM in Euro
            var lastOneMonthTransactionsQuantityATMInEur = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastOneMonth &&
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);

            var lastSixMonthTransactionsQuantityATMInEur = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastSixMonth &&
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);

            var lastOneYearTransactionsQuantityATMInEur = await _context.ATMTransactions
                .Where(t => t.TransactionTime >= lastOneYear &&
                t.TransactionTime <= currentDate &&
                t.Currency == DB.Entities.Currency.EUR)
                .SumAsync(t => t.TransactionFee);


            //Total in GEL
            var totalLastOneMonthTransactionsQuantityInGel =
                lastOneMonthInternalTransactionsQuantityInGel +
                lastOneMonthExternalTransactionsQuantityInGel +
                lastOneMonthTransactionsQuantityATMInGel;

            var totalLastSixMonthTransactionsQuantityInGel =
                lastSixMonthInternalTransactionsQuantityInGel +
                lastSixMonthExternalTransactionsQuantityInGel +
                lastSixMonthTransactionsQuantityATMInGel;

            var totalLastOneYearTransactionsQuantityInGel =
                lastOneYearInternalTransactionsQuantityInGel +
                lastOneYearExternalTransactionsQuantityInGel +
                lastOneYearTransactionsQuantityATMInGel;

            //Total in USD
            var totalLastOneMonthTransactionsQuantityInUsd =
                lastOneMonthInternalTransactionsQuantityInUsd +
                lastOneMonthExternalTransactionsQuantityInUsd +
                lastOneMonthTransactionsQuantityATMInUsd;

            var totalLastSixMonthTransactionsQuantityInUsd =
                lastSixMonthInternalTransactionsQuantityInUsd +
                lastSixMonthExternalTransactionsQuantityInUsd +
                lastSixMonthTransactionsQuantityATMInUsd;

            var totalLastOneYearTransactionsQuantityInUsd =
                lastOneYearInternalTransactionsQuantityInUsd +
                lastOneYearExternalTransactionsQuantityInUsd +
                lastOneYearTransactionsQuantityATMInUsd;

            //Total in EURO
            var totalLastOneMonthTransactionsQuantityInEur =
                lastOneMonthInternalTransactionsQuantityInEur +
                lastOneMonthExternalTransactionsQuantityInEur +
                lastOneMonthTransactionsQuantityATMInEur;

            var totalLastSixMonthTransactionsQuantityInEur =
                lastSixMonthInternalTransactionsQuantityInEur +
                lastSixMonthExternalTransactionsQuantityInEur +
                lastSixMonthTransactionsQuantityATMInEur;

            var totalLastOneYearTransactionsQuantityInEur =
                lastOneYearInternalTransactionsQuantityInEur +
                lastOneYearExternalTransactionsQuantityInEur +
                lastOneYearTransactionsQuantityATMInEur;

            //Internal
            incomeAmount.Add(nameof(lastOneMonthInternalTransactionsQuantityInGel), lastOneMonthInternalTransactionsQuantityInGel);
            incomeAmount.Add(nameof(lastSixMonthInternalTransactionsQuantityInGel), lastSixMonthInternalTransactionsQuantityInGel);
            incomeAmount.Add(nameof(lastOneYearInternalTransactionsQuantityInGel), lastOneYearInternalTransactionsQuantityInGel);

            incomeAmount.Add(nameof(lastOneMonthInternalTransactionsQuantityInUsd), lastOneMonthInternalTransactionsQuantityInUsd);
            incomeAmount.Add(nameof(lastSixMonthInternalTransactionsQuantityInUsd), lastSixMonthInternalTransactionsQuantityInUsd);
            incomeAmount.Add(nameof(lastOneYearInternalTransactionsQuantityInUsd), lastOneYearInternalTransactionsQuantityInUsd);

            incomeAmount.Add(nameof(lastOneMonthInternalTransactionsQuantityInEur), lastOneMonthInternalTransactionsQuantityInEur);
            incomeAmount.Add(nameof(lastSixMonthInternalTransactionsQuantityInEur), lastSixMonthInternalTransactionsQuantityInEur);
            incomeAmount.Add(nameof(lastOneYearInternalTransactionsQuantityInEur), lastOneYearInternalTransactionsQuantityInEur);

            //External
            incomeAmount.Add(nameof(lastOneMonthExternalTransactionsQuantityInGel), lastOneMonthExternalTransactionsQuantityInGel);
            incomeAmount.Add(nameof(lastSixMonthExternalTransactionsQuantityInGel), lastSixMonthExternalTransactionsQuantityInGel);
            incomeAmount.Add(nameof(lastOneYearExternalTransactionsQuantityInGel), lastOneYearExternalTransactionsQuantityInGel);

            incomeAmount.Add(nameof(lastOneMonthExternalTransactionsQuantityInUsd), lastOneMonthExternalTransactionsQuantityInUsd);
            incomeAmount.Add(nameof(lastSixMonthExternalTransactionsQuantityInUsd), lastSixMonthExternalTransactionsQuantityInUsd);
            incomeAmount.Add(nameof(lastOneYearExternalTransactionsQuantityInUsd), lastOneYearExternalTransactionsQuantityInUsd);

            incomeAmount.Add(nameof(lastOneMonthExternalTransactionsQuantityInEur), lastOneMonthExternalTransactionsQuantityInEur);
            incomeAmount.Add(nameof(lastSixMonthExternalTransactionsQuantityInEur), lastSixMonthExternalTransactionsQuantityInEur);
            incomeAmount.Add(nameof(lastOneYearExternalTransactionsQuantityInEur), lastOneYearExternalTransactionsQuantityInEur);

            //ATM
            incomeAmount.Add(nameof(lastOneMonthTransactionsQuantityATMInGel), lastOneMonthTransactionsQuantityATMInGel);
            incomeAmount.Add(nameof(lastSixMonthTransactionsQuantityATMInGel), lastSixMonthTransactionsQuantityATMInGel);
            incomeAmount.Add(nameof(lastOneYearTransactionsQuantityATMInGel), lastOneYearTransactionsQuantityATMInGel);

            incomeAmount.Add(nameof(lastOneMonthTransactionsQuantityATMInUsd), lastOneMonthTransactionsQuantityATMInUsd);
            incomeAmount.Add(nameof(lastSixMonthTransactionsQuantityATMInUsd), lastSixMonthTransactionsQuantityATMInUsd);
            incomeAmount.Add(nameof(lastOneYearTransactionsQuantityATMInUsd), lastOneYearTransactionsQuantityATMInUsd);

            incomeAmount.Add(nameof(lastOneMonthTransactionsQuantityATMInEur), lastOneMonthTransactionsQuantityATMInEur);
            incomeAmount.Add(nameof(lastSixMonthTransactionsQuantityATMInEur), lastSixMonthTransactionsQuantityATMInEur);
            incomeAmount.Add(nameof(lastOneYearTransactionsQuantityATMInEur), lastOneYearTransactionsQuantityATMInEur);

            //Total
            incomeAmount.Add(nameof(totalLastOneMonthTransactionsQuantityInGel), totalLastOneMonthTransactionsQuantityInGel);
            incomeAmount.Add(nameof(totalLastSixMonthTransactionsQuantityInGel), totalLastSixMonthTransactionsQuantityInGel);
            incomeAmount.Add(nameof(totalLastOneYearTransactionsQuantityInGel), totalLastOneYearTransactionsQuantityInGel);

            incomeAmount.Add(nameof(totalLastOneMonthTransactionsQuantityInUsd), totalLastOneMonthTransactionsQuantityInUsd);
            incomeAmount.Add(nameof(totalLastSixMonthTransactionsQuantityInUsd), totalLastSixMonthTransactionsQuantityInUsd);
            incomeAmount.Add(nameof(totalLastOneYearTransactionsQuantityInUsd), totalLastOneYearTransactionsQuantityInUsd);

            incomeAmount.Add(nameof(totalLastOneMonthTransactionsQuantityInEur), totalLastOneMonthTransactionsQuantityInEur);
            incomeAmount.Add(nameof(totalLastSixMonthTransactionsQuantityInEur), totalLastSixMonthTransactionsQuantityInEur);
            incomeAmount.Add(nameof(totalLastOneYearTransactionsQuantityInEur), totalLastOneYearTransactionsQuantityInEur);

            return incomeAmount;
        }
    }
}



