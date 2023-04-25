using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.ATMServices;
using System.ComponentModel.DataAnnotations;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ATMController : ControllerBase
    {
        private readonly IShowBalanceService _showBalanceService;
        private readonly IChangePINService _changePINService;
        private readonly IWithdrawMoneyService _withdrawMoneyService;

        public ATMController(IShowBalanceService showBalanceService,
            IChangePINService changePINService,
            IWithdrawMoneyService withdrawMoneyService)
        {
            _showBalanceService = showBalanceService;
            _changePINService = changePINService;
            _withdrawMoneyService = withdrawMoneyService;
        }

        [HttpPost("show_balance")]
        public async Task<IActionResult> ShowBalance([FromQuery]CardDetailsDto cardDetails)
        {
            var httpResult = new HttpResult();

            var showBalanceResult = await _showBalanceService.ShowBalance(cardDetails);

            if (!showBalanceResult.success)
            {
                httpResult.Message = showBalanceResult.message;
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = showBalanceResult.message;

            return Ok(httpResult);
        }

        [HttpPost("withdraw_money")]
        public async Task<IActionResult> WithdrawMoney(
            [FromQuery] CardDetailsDto cardDetails,
            [Required] double amount)
        {
            var httpResult = new HttpResult();

            var withdrawMoneyResult = await _withdrawMoneyService.WithdrawMoney(cardDetails,amount);

            if (!withdrawMoneyResult.success)
            {
                httpResult.Message = withdrawMoneyResult.message;
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = withdrawMoneyResult.message;

            return Ok(httpResult);
        }

        [HttpPatch("change_pin")]
        public async Task<IActionResult> ChangePIN(
            [FromQuery]ChangePinDto changePin)
        {
            var httpResult = new HttpResult();

            var changeCardPINResult = await _changePINService.ChangePIN(changePin);

            if (!changeCardPINResult.success)
            {
                httpResult.Message = changeCardPINResult.message;
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = changeCardPINResult.message;

            return Ok(httpResult);
        }
    }
}
