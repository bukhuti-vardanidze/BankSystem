using DB.Entities;
using IbanNet;
using Repositories.DTOs;
using Repositories.InternetBankingRepositories;
using Repositories.InternetBankRepositories;

namespace Services.InternetBankingServices
{
    public interface IRegisterBankAccountService
    {
        Task<(bool register, object result)> RegisterBankAccount(int userId, RegisterBankAccountDto bankAccountRegistration);
    }
    public class RegisterBankAccountService : IRegisterBankAccountService
    {
        private readonly IRegisterBankAccountRepository _registerBankAccountRepository;

        public RegisterBankAccountService(IRegisterBankAccountRepository registerBankAccountRepository)
        {
            _registerBankAccountRepository = registerBankAccountRepository;
        }

        public async Task<(bool register, object result)> RegisterBankAccount(
            int userId, RegisterBankAccountDto bankAccountRegistration)
        {
            try
            {
                var userEntityCheckResult = await _registerBankAccountRepository
                    .CheckUserEntityInDb(userId);

                if (userEntityCheckResult == null)
                {
                    return (false,"User Not Found!");
                }

                var checkIbanResult = await _registerBankAccountRepository
                    .CheckIbanInDb(bankAccountRegistration);

                if (checkIbanResult)
                {
                    return (false, "Iban Already Exist!");
                }

                var Iban = ValidateIban(bankAccountRegistration.IBAN);

                string IBAN;

                if (Iban)
                {
                    IBAN = bankAccountRegistration.IBAN;
                }
                else
                {
                    return (false,"Invalid IBan!");
                }

                var createBankAccount = new BankAccountEntity()
                {
                    UserId = userId,
                    IBAN = IBAN,
                    Amount = bankAccountRegistration.Amount,
                    Currency = bankAccountRegistration.Currency
                };

                var registerBankAccount = await _registerBankAccountRepository
                    .RegisterBankAccountInDb(createBankAccount);

                return (registerBankAccount, createBankAccount.BankAccountId);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }

        
        public bool ValidateIban(string Iban)
        {
            var ibanValidator = new IbanValidator();
            var isValid = ibanValidator.Validate(Iban).IsValid;
            if (!isValid) return false;
            return isValid;
        }
    }

   
}