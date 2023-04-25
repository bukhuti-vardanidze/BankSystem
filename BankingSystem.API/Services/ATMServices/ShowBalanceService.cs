using Repositories.ATMRepositories;
using Repositories.DTOs;

namespace Services.ATMServices
{
    public interface IShowBalanceService
    {
        Task<(bool success, string message)> ShowBalance(CardDetailsDto cardDetails);
    }
    public class ShowBalanceService : IShowBalanceService
    {
        private readonly IShowBalanceRepository _showBalanceRepository;

        public ShowBalanceService(IShowBalanceRepository showBalanceRepository)
        {
            _showBalanceRepository = showBalanceRepository;
        }

        public async Task<(bool success,string message)> ShowBalance(CardDetailsDto cardDetails)
        {
            try
            {
                var checkCardNumberResult = await _showBalanceRepository
               .CheckCardNumberInDb(cardDetails.CardNumber);

                if (checkCardNumberResult == null)
                {
                    return (false, "Invalid Card Number!"); ;
                }

                var checkCardPINResult = await _showBalanceRepository
                    .CheckCardPINInDb(cardDetails.PIN);

                if (checkCardPINResult == null)
                {
                    return (false, "Invalid PIN!");
                }

                var showBalanceResult = await _showBalanceRepository
                    .ShowBalance(cardDetails.CardNumber);

                return (true,showBalanceResult);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
