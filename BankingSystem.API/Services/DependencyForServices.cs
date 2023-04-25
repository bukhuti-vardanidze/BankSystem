using Microsoft.Extensions.DependencyInjection;
using Repositories.ReportsRepository;
using Services.ATMServices;
using Services.InternetBankingServices;
using Services.InternetBankServices;
using Services.ReporstsService;

namespace Services
{
    public static class DependencyForServices
    {
        public static IServiceCollection InjectServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IRegisterBankAccountService, RegisterBankAccountService>();
            services.AddScoped<IRegisterCardService, RegisterCardService>();
            services.AddScoped<IShowBankAccountsService, ShowBankAccountsService>();
            services.AddScoped<IShowCardsService, ShowCardsService>();
            services.AddScoped<IInternalTransactionsService, InternalTransactionsService>();
            services.AddScoped<IExternalTransactionsService, ExternalTransactionsService>();         
            services.AddScoped<IShowBalanceService, ShowBalanceService>();
            services.AddScoped<IChangePINService, ChangePINService>();
            services.AddScoped<IWithdrawMoneyService, WithdrawMoneyService>();
            services.AddScoped<ICurrentYearRegisteredUsersService, CurrentYearRegisteredUsersService>();
            services.AddScoped<ILastOneYearRegisteredUsersService, LastOneYearRegisteredUsersService>();
            services.AddScoped<ILastThirtyDaysRegisteredUsersService, LastThirtyDaysRegisteredUsersService>();
            services.AddScoped<ITransactionsDuringSomePeriodOfTimeService, TransactionsDuringSomePeriodOfTimeService>();
            services.AddScoped<IIncomeAmountFromTransactionsDuringSomePeriodOfTimeService, IncomeAmountFromTransactionsDuringSomePeriodOfTimeService>();
            services.AddScoped<IAverageIncomeService, AverageIncomeService>();
            services.AddScoped<ITransactionsChartService, TransactionsChartService>();
            services.AddScoped<ITotalAmountWithdrawalAtmService, TotalAmountWithdrawalAtmService>();

            return services;
        }
    }
}
