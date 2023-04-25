using DB;
using Microsoft.EntityFrameworkCore;

namespace Repositories.ReportsRepository
{
    public interface IAverageIncomeRepository
    {
        Task<Dictionary<string, double>> AverageIncome();
    }

    public class AverageIncomeRepository : IAverageIncomeRepository
    {
        private readonly AppDbContext _context;

        public AverageIncomeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Dictionary<string, double>> AverageIncome()
        {
            Dictionary<string, double> averageIncome = new Dictionary<string, double>();

            double averageIncomeFromUserTransactionsInGel;
            double averageIncomeFromAtmTransactionsInGel;

            var hasUserTransactionInGel = await _context.UserTransactions
                .Where(t => t.SenderCurrency == DB.Entities.Currency.GEL)
                .AnyAsync();

            if (!hasUserTransactionInGel)
            {
                averageIncomeFromUserTransactionsInGel = 0;
            }
            else
            {
                averageIncomeFromUserTransactionsInGel = await _context.UserTransactions
                    .Where(t => t.SenderCurrency == DB.Entities.Currency.GEL)
                    .AverageAsync(t => t.TransactionFee);
            }

            var hasAtmTransactionInGel = await _context.ATMTransactions
                .Where(t => t.Currency == DB.Entities.Currency.GEL)
                .AnyAsync();

            if (!hasAtmTransactionInGel)
            {
                averageIncomeFromAtmTransactionsInGel = 0;
            }
            else
            {
                averageIncomeFromAtmTransactionsInGel = await _context.ATMTransactions
                    .Where(t => t.Currency == DB.Entities.Currency.GEL)
                    .AverageAsync(t => t.TransactionFee);
            }

            var totalAverageIncomeInGel = (averageIncomeFromUserTransactionsInGel + averageIncomeFromAtmTransactionsInGel)/2;


            double averageIncomeFromUserTransactionsInUsd;
            double averageIncomeFromAtmTransactionsInUsd;

            var hasUserTransactionInUsd = await _context.UserTransactions
                .Where(t => t.SenderCurrency == DB.Entities.Currency.USD)
                .AnyAsync();

            if (!hasUserTransactionInUsd)
            {
                averageIncomeFromUserTransactionsInUsd = 0;
            }
            else
            {
                averageIncomeFromUserTransactionsInUsd = await _context.UserTransactions
                    .Where(t => t.SenderCurrency == DB.Entities.Currency.USD)
                    .AverageAsync(t => t.TransactionFee);
            }

            var hasAtmTransactionInUsd = await _context.ATMTransactions
                .Where(t => t.Currency == DB.Entities.Currency.USD)
                .AnyAsync();

            if (!hasAtmTransactionInUsd)
            {
                averageIncomeFromAtmTransactionsInUsd = 0;
            }
            else
            {
                averageIncomeFromAtmTransactionsInUsd = await _context.ATMTransactions
                    .Where(t => t.Currency == DB.Entities.Currency.USD)
                    .AverageAsync(t => t.TransactionFee);
            }

            var totalAverageIncomeInUsd = (averageIncomeFromUserTransactionsInUsd + averageIncomeFromAtmTransactionsInUsd) / 2;


            double averageIncomeFromUserTransactionsInEur;
            double averageIncomeFromAtmTransactionsInEur;

            var hasUserTransactionInEur = await _context.UserTransactions
                .Where(t => t.SenderCurrency == DB.Entities.Currency.EUR)
                .AnyAsync();

            if (!hasUserTransactionInEur)
            {
                averageIncomeFromUserTransactionsInEur = 0;
            }
            else
            {
                averageIncomeFromUserTransactionsInEur = await _context.UserTransactions
                    .Where(t => t.SenderCurrency == DB.Entities.Currency.EUR)
                    .AverageAsync(t => t.TransactionFee);
            }

            var hasAtmTransactionInEur = await _context.ATMTransactions
                .Where(t => t.Currency == DB.Entities.Currency.USD)
                .AnyAsync();

            if (!hasAtmTransactionInEur)
            {
                averageIncomeFromAtmTransactionsInEur = 0;
            }
            else
            {
                averageIncomeFromAtmTransactionsInEur = await _context.ATMTransactions
                    .Where(t => t.Currency == DB.Entities.Currency.USD)
                    .AverageAsync(t => t.TransactionFee);
            }

            var totalAverageIncomeInEur = (averageIncomeFromUserTransactionsInEur + averageIncomeFromAtmTransactionsInEur) / 2;


            averageIncome.Add(nameof(totalAverageIncomeInGel), totalAverageIncomeInGel);
            averageIncome.Add(nameof(totalAverageIncomeInUsd), totalAverageIncomeInUsd);
            averageIncome.Add(nameof(totalAverageIncomeInEur), totalAverageIncomeInEur);

            return averageIncome;
        }
    }
}
