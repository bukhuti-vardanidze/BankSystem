using Repositories.ATMRepositories;
using Repositories.DTOs;

namespace Services.ATMServices
{
    public interface IChangePINService
    {
        Task<(bool success,string message)> ChangePIN(ChangePinDto changePin);
    }

    public class ChangePINService : IChangePINService
    {
        private readonly IChangePINRepository _changePINRepository;

        public ChangePINService(IChangePINRepository changePINRepository)
        {
            _changePINRepository = changePINRepository;
        }

        public async Task<(bool success, string message)> ChangePIN(ChangePinDto changePin)
        {
            try
            {
                var checkCardNumberResult = await _changePINRepository
               .CheckCardNumberInDb(changePin.CardNumber);

                if (checkCardNumberResult == null)
                {
                    return (false,"Invalid Card Number!");
                }

                var checkCardPINResult = await _changePINRepository
                    .CheckCardPINInDb(changePin.PIN);

                if (checkCardPINResult == null)
                {
                    return (false, "Invalid PIN!");
                }

                if (changePin.NewPIN == changePin.PIN)
                {
                    return (false, "PIN Can't be same!");
                }

                var showChangePINResult = await _changePINRepository
                    .ChangePIN(changePin.CardNumber, changePin.NewPIN);

                return (true, showChangePINResult);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
