using Microsoft.Extensions.DependencyInjection;
using Repositories.ATMRepositories;
using Repositories.InternetBankingRepositories;
using Repositories.InternetBankRepositories;
using Repositories.ReportsRepository;

namespace Repositories
{
    public static class DependencyForRepositories
    {
        public static IServiceCollection InjectRepositories(
            this IServiceCollection services)
        {
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<IRegisterUserRepository, RegisterUserRepository>();
            services.AddScoped<IRegisterBankAccountRepository, RegisterBankAccountRepository>();
            services.AddScoped<IRegisterCardRepository, RegisterCardRepository>();
            services.AddScoped<IShowBankAccountsRepository, ShowBankAccountsRepository>();
            services.AddScoped<IShowCardsRepository, ShowCardsRepository>();
            services.AddScoped<IInternalTransactionsRepository, InternalTransactionsRepository>();
            services.AddScoped<IExternalTransactionsRepository, ExternalTransactionsRepository>();
            services.AddScoped<IShowBalanceRepository, ShowBalanceRepository>();
            services.AddScoped<IChangePINRepository, ChangePINRepository>();
            services.AddScoped<IWithdrawMoneyRepository, WithdrawMoneyRepository>();
            services.AddScoped<ICurrentYearRegisteredUsersRepository, CurrentYearRegisteredUsersRepository>();
            services.AddScoped<ILastOneYearRegisteredUsersRepository, LastOneYearRegisteredUsersRepository>();
            services.AddScoped<ILastThirtyDaysRegisteredUsersRepository, LastThirtyDaysRegisteredUsersRepository>();
            services.AddScoped<ITransactionsDuringSomePeriodOfTimeRepository, TransactionsDuringSomePeriodOfTimeRepository>();
            services.AddScoped<IIncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository, IncomeAmountFromTransactionsDuringSomePeriodOfTimeRepository>();
            services.AddScoped<IAverageIncomeRepository, AverageIncomeRepository>();
            services.AddScoped<ITransactionsChartRepository, TransactionsChartRepository>();
            services.AddScoped<ITotalAmountWithdrawalAtmRepository, TotalAmountWithdrawalAtmRepository>();

            return services;
        }
    }
}
