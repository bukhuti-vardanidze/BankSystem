using Repositories.DTOs;
using Repositories.InternetBankingRepositories;

namespace Services.InternetBankingServices
{
    public interface IInternalTransactionsService
    {
        Task<(bool register, string message)> InternalTransaction(string userId, TransactionDto Transaction);
    }

    public class InternalTransactionsService : IInternalTransactionsService
    {
        private readonly IInternalTransactionsRepository _internalTransactionsRepository;
        
        public InternalTransactionsService(IInternalTransactionsRepository
            internalTransactionsRepository)
        {
            _internalTransactionsRepository = internalTransactionsRepository;
        }

        public async Task<(bool register, string message)> InternalTransaction(
            string userId, TransactionDto Transaction)
        {
            try
            {
                var checkSenderIBANResult = await _internalTransactionsRepository
                .CheckIBANInDb(Transaction.SenderIBAN);

                if (checkSenderIBANResult == null)
                {
                    return (false, 
                        $"{Transaction.SenderIBAN} Doesn't Exist!");
                }

                var checkRecepientIBANResult = await _internalTransactionsRepository
                    .CheckIBANInDb(Transaction.RecipientIBAN);

                if (checkRecepientIBANResult == null)
                {
                    return (false,
                        $"{Transaction.RecipientIBAN} Doesn't Exist!");
                }

                if (Transaction.Amount > checkSenderIBANResult.Amount)
                {
                    return (false,
                        "Not Enough Money on Account for Transaction!");
                }

                if (checkSenderIBANResult.UserId != checkRecepientIBANResult.UserId)
                {
                    return (false,
                        "This Transaction Is Internal And Can Only Be Made Between Same User's Accounts.");
                }

                if(checkSenderIBANResult.IBAN == checkRecepientIBANResult.IBAN)
                {
                    return (false,
                        "Choose Correct Recepient Account!");
                }

                var transactionResult = await _internalTransactionsRepository
                    .InternalTransaction(Transaction);

                return (transactionResult, "Transaction Completed Successfully!");
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing the request.", ex);
            }
        }
    }

    
}