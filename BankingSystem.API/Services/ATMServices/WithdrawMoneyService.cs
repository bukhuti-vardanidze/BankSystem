using Repositories.ATMRepositories;
using Repositories.DTOs;

namespace Services.ATMServices
{
    public interface IWithdrawMoneyService
    {
        Task<(bool success, string message)> WithdrawMoney(CardDetailsDto cardDetails, double amount);
    }
    public class WithdrawMoneyService : IWithdrawMoneyService
    {
        private readonly IWithdrawMoneyRepository _withdrawMoneyRepository;

        public WithdrawMoneyService(IWithdrawMoneyRepository withdrawMoneyRepository)
        {
            _withdrawMoneyRepository = withdrawMoneyRepository;
        }

        public async Task<(bool success,string message)> WithdrawMoney(CardDetailsDto cardDetails, double amount)
        {
            try
            {
                var checkCardNumberResult = await _withdrawMoneyRepository
               .CheckCardNumberInDb(cardDetails.CardNumber);

                if (checkCardNumberResult == null)
                {
                    return (false,"Invalid Card Number!");
                }

                var checkCardPINResult = await _withdrawMoneyRepository
                    .CheckCardPINInDb(cardDetails.PIN);

                if (checkCardPINResult == null)
                {
                    return (false,"Invalid PIN!");
                }

                var fee = amount * 0.02;
                var amountOnBankAccount = checkCardNumberResult.BankAccountEntity.Amount;

                if (amount + fee > amountOnBankAccount)
                {
                    return (false, "Not enough money on bank account!");
                }

                if (amount <= 0)
                {
                    return (false, "Enter Amount More than 0!");
                }

                if (amount % 5 != 0)
                { 
                    return (false, "Amount must be multiple of 5"); 
                }

                var checkLimitResult = await CheckAmountLimit(amount,cardDetails.CardNumber);

                if(!checkLimitResult) 
                {
                    return (false, "Amount is over the limit");
                }         

                var withdrawMoneyResult = await _withdrawMoneyRepository
                    .WithdrawMoney(amount,cardDetails.CardNumber);

                return (true,
                    $"You withdrew {amount} {checkCardNumberResult.BankAccountEntity.Currency}");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        public async Task<bool> CheckAmountLimit(double amount, string cardNumber)
        {
            var amountLimit = 10000;

            var exchangeRateResult = await _withdrawMoneyRepository
                .ExchangeRate(amount, cardNumber);

            var oneDayWithdrawStatistic = await _withdrawMoneyRepository.CardLimitForOneDay(amount, cardNumber);
          
            if (amount * exchangeRateResult > amountLimit)
            {
                return false;
            }

            if(oneDayWithdrawStatistic * exchangeRateResult > amountLimit)
            {
                return false;
            }

            return true;
        }
    }
}
