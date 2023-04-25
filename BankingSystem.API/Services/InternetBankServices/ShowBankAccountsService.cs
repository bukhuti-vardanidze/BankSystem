using Repositories.DTOs;
using Repositories.InternetBankingRepositories;

namespace Services.InternetBankingServices
{
    public interface IShowBankAccountsService
    {
        Task<ICollection<BankAccountsDto>> ShowBankAccounts(string userId);
    }

    public class ShowBankAccountsService : IShowBankAccountsService
    {
        private readonly IShowBankAccountsRepository _showBankAccountsRepository;

        public ShowBankAccountsService(
            IShowBankAccountsRepository showBankAccountsRepository)
        {
            _showBankAccountsRepository = showBankAccountsRepository;
        }

        public async Task<ICollection<BankAccountsDto>> ShowBankAccounts(string userId)
        {
            try
            {
                var bankAccountsResult = await _showBankAccountsRepository.BankAccounts(userId);

                var resultList = new List<BankAccountsDto>();

                for (int i = 0; i < bankAccountsResult.Count; i++)
                {
                    var entityToDto = new BankAccountsDto()
                    {
                        IBAN = bankAccountsResult[i].IBAN,
                        Amount = bankAccountsResult[i].Amount,
                        Currency = bankAccountsResult[i].Currency,
                    };

                    resultList.Add(entityToDto);
                }

                return resultList;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
