using Repositories.DTOs;
using Repositories.InternetBankingRepositories;

namespace Services.InternetBankingServices
{
    public interface IExternalTransactionsService
    {
        Task<(bool register,string message)> ExternalTransaction(string userId, TransactionDto transaction);
    }
    public class ExternalTransactionsService : IExternalTransactionsService
    {
        private readonly IExternalTransactionsRepository _externalTransactionsRepository;

        public ExternalTransactionsService(
            IExternalTransactionsRepository externalTransactionsRepository)
        {
            _externalTransactionsRepository = externalTransactionsRepository;
        }

        public async Task<(bool register, string message)> ExternalTransaction(
            string userId, TransactionDto transaction)
        {
            try
            {
                var checkSenderIBANResult = await _externalTransactionsRepository
                .CheckIBanForUser(userId,transaction.SenderIBAN);

                if (checkSenderIBANResult == null)
                {
                    return (false,
                        "Choose Your Account!");
                }

                var checkRecepientIBANResult = await _externalTransactionsRepository
                        .CheckIBANInDb(transaction.RecipientIBAN);

                if (checkRecepientIBANResult == null)
                {
                    return (false,
                        $"{transaction.RecipientIBAN} Doesn't Exist!");
                }

                if (transaction.Amount + (transaction.Amount * 0.01 + 0.5) > checkSenderIBANResult.Amount)
                {
                    return (false,
                        "Not Enough Money on Account for Transaction!");
                }

                if (checkSenderIBANResult.UserId == checkRecepientIBANResult.UserId)
                {
                    return (false,
                        "This Transaction Is External And Can Only Be Made Between Different User's Accounts.");
                }

                if (checkSenderIBANResult.IBAN == checkRecepientIBANResult.IBAN)
                {
                    return (false,
                        "Choose Correct Recepient Account!"); ;
                }

                var transactionResult = await _externalTransactionsRepository
                        .ExternalTransaction(transaction);

                return (transactionResult, "Transaction Completed Successfully!");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }
}
