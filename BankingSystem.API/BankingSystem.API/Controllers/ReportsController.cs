using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.ReporstsService;
using System.Data;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Operator")]
    public class ReportsController : ControllerBase
    {
        private readonly ICurrentYearRegisteredUsersService _currentYearRegisteredUsers;
        private readonly ILastOneYearRegisteredUsersService _lastOneYearRegisteredUsers;
        private readonly ILastThirtyDaysRegisteredUsersService _lastThirtyDaysRegisteredUsers;
        private readonly ITransactionsDuringSomePeriodOfTimeService _transactionsDuringSomePeriodOfTime;
        private readonly IIncomeAmountFromTransactionsDuringSomePeriodOfTimeService _incomeAmountFromTransactions;
        private readonly IAverageIncomeService _averageIncomeService;
        private readonly ITransactionsChartService _transactionsChartService;
        private readonly ITotalAmountWithdrawalAtmService _totalAmountWithdrawalAtmService;

        public ReportsController(
            ICurrentYearRegisteredUsersService currentYearRegisteredUsers,
            ILastOneYearRegisteredUsersService lastOneYearRegisteredUsers,
            ILastThirtyDaysRegisteredUsersService lastThirtyDaysRegisteredUsers,
            ITransactionsDuringSomePeriodOfTimeService transactionsDuringSomePeriodOfTime,
            IIncomeAmountFromTransactionsDuringSomePeriodOfTimeService incomeAmountFromTransactions,
            IAverageIncomeService averageIncomeService,
            ITransactionsChartService transactionsChartService,
            ITotalAmountWithdrawalAtmService totalAmountWithdrawalAtmService) 
        {
            _currentYearRegisteredUsers = currentYearRegisteredUsers;
            _lastOneYearRegisteredUsers = lastOneYearRegisteredUsers;
            _lastThirtyDaysRegisteredUsers = lastThirtyDaysRegisteredUsers;
            _transactionsDuringSomePeriodOfTime = transactionsDuringSomePeriodOfTime;
            _incomeAmountFromTransactions = incomeAmountFromTransactions;
            _averageIncomeService = averageIncomeService;
            _transactionsChartService = transactionsChartService;
            _totalAmountWithdrawalAtmService = totalAmountWithdrawalAtmService;
        }

        [HttpGet("current_year_registered_users")]

        public async Task<IActionResult> CurrentYearRegisteredUsers()
        {
            var currentYearRegisteredUsersResult = await _currentYearRegisteredUsers.CurrentYearRegisteredUsersQuantity();
            
            if(currentYearRegisteredUsersResult.Count == 0)
            {
                return BadRequest(currentYearRegisteredUsersResult);
            }
           
            return Ok(currentYearRegisteredUsersResult);
        }

        [HttpGet("last_one_year_registered_users")]
        public async Task<IActionResult> LastOneYearRegisteredUsers()
        {
            var lastOneYearRegisteredUsersResult = await _lastOneYearRegisteredUsers.LastOneYearRegisteredUsersQuantity();
           
            if (lastOneYearRegisteredUsersResult.Count == 0)
            {
                return BadRequest(lastOneYearRegisteredUsersResult);
            }
            
            return Ok(lastOneYearRegisteredUsersResult);
        }

        [HttpGet("last_thirty_days_registered_users")]
        public async Task<IActionResult> LastThirtyDaysRegisteredUsers()
        {
            var lastThirtyDaysRegisteredUsersResult = await _lastThirtyDaysRegisteredUsers.LastThirtyDaysRegisteredUsersQuantity();
           
            if (lastThirtyDaysRegisteredUsersResult.Count == 0)
            {
                return BadRequest(lastThirtyDaysRegisteredUsersResult);
            }
            
            return Ok(lastThirtyDaysRegisteredUsersResult);
        }

        [HttpGet("count_transactions")]
        public async Task<IActionResult> TransactionsDuringSomePeriodsOfTime()
        {
            var transactionsDuringSomePeriodOfTimeResult = await _transactionsDuringSomePeriodOfTime.TransactionsDuringSomePeriodOfTime();
            
            if (transactionsDuringSomePeriodOfTimeResult.Count == 0)
            {
                return BadRequest(transactionsDuringSomePeriodOfTimeResult);
            }
            return Ok(transactionsDuringSomePeriodOfTimeResult);
        }

        [HttpGet("income_amount_from_transactions")]
        public async Task<IActionResult> IncomeAmountFromTransactionsDuringSomePeriodOfTime()
        {
            var IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult = 
                await _incomeAmountFromTransactions
                .IncomeAmountFromTransactionsDuringSomePeriodOfTime();

            if (IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult == null)
            {
                return BadRequest(IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult);
            }

            return Ok(IncomeAmountFromTransactionsDuringSomePeriodOfTimeResult);
        }

        [HttpGet("average_income")]
        public async Task<IActionResult> AverageIncome()
        {
            var averageIncomeResult = await _averageIncomeService.AverageIncome();

            if (averageIncomeResult.Count == 0)
            {
                return BadRequest(averageIncomeResult);
            }

            return Ok(averageIncomeResult);
        }

        [HttpGet("transactions_chart")]
        public async Task<IActionResult> TransactionsChart()
        {
            var transactionChartResult = await _transactionsChartService
                .TransactionsChart();
            
            if (transactionChartResult.Count == 0)
            {
                return BadRequest(transactionChartResult);
            }

            return Ok(transactionChartResult);
        }

        [HttpGet("total_withdrawal_amount_from_atm")]
        public async Task<IActionResult> TotalWithdrawalAmount()
        {
            var totalWithdrawalResult = await _totalAmountWithdrawalAtmService
                .TotalWithdrawalAmount();

            if (totalWithdrawalResult.Count == 0)
            {
                return BadRequest(totalWithdrawalResult);
            }

            return Ok(totalWithdrawalResult);
        }
    }
}
