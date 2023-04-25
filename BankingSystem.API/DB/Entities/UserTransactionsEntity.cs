using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DB.Entities
{
    public class UserTransactionsEntity
    {
        [Key]
        public int TransactionId { get; set; }

        [Required]
        public int SenderAccountId { get; set; }

        [Required]
        public Currency SenderCurrency { get; set; }
        
        [Required]
        public double SenderAmount { get; set; }

        [ForeignKey("SenderAccountId"), Required]
        [InverseProperty("SenderTransactions")]
        public virtual BankAccountEntity SenderAccount { get; set; }

        [Required]
        public int RecipientAccountId { get; set; }
     
        [Required]
        public Currency RecipientCurrency { get; set; }
        
        [Required]
        public double RecipientAmount { get; set; }

        [ForeignKey("RecipientAccountId"), Required]
        [InverseProperty("RecipientTransactions")]
        public virtual BankAccountEntity RecipientAccount { get; set; }

        [Required]
        public TransactionType TransactionType { get; set; }

        [Required]
        public double TransactionFee { get; set; }

        [Required]
        public double ExchangeRate { get; set; }

        [Required]
        public DateTime TransactionTime { get; set; }      
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum TransactionType
    {
        Internal = 1,
        External = 2,
    }
}
