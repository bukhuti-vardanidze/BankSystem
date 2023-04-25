using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.DTOs;
using Services.InternetBankingServices;
using Services.InternetBankServices;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BankingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Operator, User")]
    public class InternetBankController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IRegisterUserService _registerUserService;
        private readonly IRegisterBankAccountService _registerBankAccountService;
        private readonly IRegisterCardService _registerCardService;
        private readonly IShowBankAccountsService _showBankAccountsService;
        private readonly IShowCardsService _showCardsService;
        private readonly IInternalTransactionsService _internalTransactionsService;
        private readonly IExternalTransactionsService _externalTransactionsService;

        public InternetBankController(ILoginService loginService,
            IRegisterUserService registerUserService, 
            IRegisterBankAccountService registerBankAccountService,
            IRegisterCardService registerCardService,
            IShowBankAccountsService showBankAccountsService,
            IShowCardsService showCardsService,
            IInternalTransactionsService internalTransactionsService,
            IExternalTransactionsService externalTransactionsService)
        {
            _loginService = loginService;
            _registerUserService = registerUserService;
            _registerBankAccountService = registerBankAccountService;
            _registerCardService = registerCardService;
            _showBankAccountsService = showBankAccountsService;
            _showCardsService = showCardsService;
            _internalTransactionsService = internalTransactionsService;
            _externalTransactionsService = externalTransactionsService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromQuery]LoginDto login)
        {
            var httpResult = new HttpResult();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var loginResult = await _loginService.LogIn(login);

            if (!loginResult.Succeeded)
            {
                httpResult.Message = "Invalid Email or Password!";
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            var tokenJWT = _loginService.JWTToken(login);

            httpResult.Message = "You have successfully signed in";
            httpResult.Payload.Add("Token", tokenJWT.Result);

            return Ok(httpResult);
        }

        [HttpPost("register_user")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> RegisterUser([FromQuery] RegisterUserDto userRegistration)
        {
            var httpResult = new HttpResult();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerUserResult = await _registerUserService.RegisterUser(userRegistration);

            if (!registerUserResult.register.Succeeded)
            {
                httpResult.Message = "Registration Failed! User already exists!";
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = "Successful Registration!";
            httpResult.Payload.Add("UserId", registerUserResult.userId);

            return Ok(httpResult);
        }

        [HttpPost("register_bankAccount")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> RegisterBankAccount(
            [Required]int userId, [FromQuery] RegisterBankAccountDto bankAccountRegistration)
        {
            var httpResult = new HttpResult();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var registerBankAccountResult = await _registerBankAccountService.RegisterBankAccount(userId, bankAccountRegistration);

            if (!registerBankAccountResult.register)
            {
                httpResult.Message = registerBankAccountResult.result.ToString();
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = "Bank account successfully registered!";
            httpResult.Payload.Add("BankAccountId", registerBankAccountResult.result);

            return Ok(httpResult);
        }

        [HttpPost("register_card")]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> RegisterCard(        
            [Required] int bankAccountId)
        {
            var httpResult = new HttpResult();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cardRegisterResult = await _registerCardService
                .RegisterCard(bankAccountId);

            if (!cardRegisterResult.register)
            {
                httpResult.Message = "Bank Account Not Found!";
                httpResult.Status= HttpResultStatus.BadRequest; 

                return BadRequest(httpResult);
            }

            httpResult.Message = "Card successfully registered!";
            httpResult.Payload.Add("CardId", cardRegisterResult.result);

            return Ok(httpResult);
        }

        [HttpGet("show_all_bankAccounts")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ShowBankAccounts()
        {
            var httpResult = new HttpResult();

            var authorizedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var bankAccountsResult = await _showBankAccountsService.ShowBankAccounts(authorizedUserId);

            if (bankAccountsResult.Count == 0)
            {
                httpResult.Message = "You currently do not have any bank accounts registered!";
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            return Ok(bankAccountsResult);
        }

        [HttpGet("show_all_cards")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ShowCardsList()
        {
            var httpResult = new HttpResult();

            var authorizedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _showCardsService.ShowCardsList(authorizedUserId);

            if (result.Count == 0)
            {
                httpResult.Message = "You currently do not have any cards registered!";
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            return Ok(result);
        }

        [HttpPost("internal_transaction")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> InternalTransaction(
            [FromQuery] TransactionDto transaction)
        {
            var httpResult = new HttpResult();

            var authorizedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var internalTransactionResult = await _internalTransactionsService
                .InternalTransaction(authorizedUserId, transaction);

            if (!internalTransactionResult.register)
            {
                httpResult.Message = internalTransactionResult.message;
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = internalTransactionResult.message;

            return Ok(httpResult);
        }

        [HttpPost("external_transaction")]
        [Authorize(Roles = "User")]
        public async Task<IActionResult> ExternalTransaction(
            [FromQuery] TransactionDto transaction)
        {
            var httpResult = new HttpResult();

            var authorizedUserId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var externalTransactionResult = await _externalTransactionsService
                .ExternalTransaction(authorizedUserId, transaction);

            if (!externalTransactionResult.register)
            {
                httpResult.Message = externalTransactionResult.message;
                httpResult.Status = HttpResultStatus.BadRequest;

                return BadRequest(httpResult);
            }

            httpResult.Message = externalTransactionResult.message;

            return Ok(httpResult);
        }
    }
}
