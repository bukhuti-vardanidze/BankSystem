using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;


namespace DB.Entities
{
    public class BankAccountEntity
    {
        [Key]
        public int BankAccountId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required, MaxLength(34)]
        [RegularExpression(@"^[A-Z]{2}\d{2}[A-Z\d]{1,30}$", ErrorMessage = "Invalid IBAN format.")]
        public string IBAN { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [ForeignKey("UserId")]
        public virtual UserEntity UserEntity { get; set; }
        public virtual ICollection<CardEntity> CardEntities { get; set; }

        [InverseProperty("SenderAccount")]
        public virtual ICollection<UserTransactionsEntity> SenderTransactions { get; set; }

        [InverseProperty("RecipientAccount")]
        public virtual ICollection<UserTransactionsEntity> RecipientTransactions { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Currency
    {
        GEL = 1,
        USD = 2,
        EUR = 3
    }
}
