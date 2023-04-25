using DB.Entities;
using Repositories.InternetBankingRepositories;

namespace Services.InternetBankingServices
{
    public interface IRegisterCardService
    {
        Task<(bool register, int result)> RegisterCard(int bankAccountId);
    }
    public class RegisterCardService : IRegisterCardService
    {
        private readonly IRegisterCardRepository _registerCardRepository;

        public RegisterCardService(IRegisterCardRepository registerCardRepository)
        {
            _registerCardRepository = registerCardRepository;
        }

        public async Task<(bool register, int result)> RegisterCard(
             int bankAccountId)
        {
            try
            {              
              var bankAccountCheckResult = await _registerCardRepository
                    .CheckBankAccountInDb(bankAccountId);

                if (bankAccountCheckResult == null)
                {
                    return (false,0);
                }
                
                Random rand = new();

                var fullName = await _registerCardRepository.GetUserFullName(bankAccountId);

                var createCard = new CardEntity()
                {
                    CardNumber = Convert.ToString((long)Math.Floor(rand.NextDouble() * 9_000_000_000_000_000L + 1_000_000_000_000_000L)),
                    FullName = fullName,
                    ExpDate = DateTime.Today.AddDays(rand.Next(365 * 6)),
                    CVV = Convert.ToString((long)Math.Floor(rand.NextDouble() * 9_00L + 1_00L)),
                    PIN = Convert.ToString((long)Math.Floor(rand.NextDouble() * 9_000L + 1_000L)),
                    BankAccountId = bankAccountId
                };

                var registerCard = await _registerCardRepository
                    .RegisterCardInDb(createCard);

                return (registerCard, createCard.CardId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }

   
}
