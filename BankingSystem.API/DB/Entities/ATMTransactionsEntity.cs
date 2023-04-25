using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Entities
{
    public class ATMTransactionsEntity
    {
        [Key]
        public int ATMTransactionId { get; set; }
       
        [Required]
        public double Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }

        [Required]
        public double TransactionFee { get; set; }

        [Required]
        public DateTime TransactionTime { get; set; }

        [Required]
        public int CardId { get; set; }
        
        [ForeignKey("CardId")]
        public virtual CardEntity CardEntity { get; set; }

    }
}
