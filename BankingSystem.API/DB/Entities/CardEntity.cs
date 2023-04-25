using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Entities
{
    public class CardEntity
    {
        [Key]
        public int CardId { get; set; }

        [Required, MinLength(16), MaxLength(16)]
        public string CardNumber { get; set; }

        [Required, MaxLength(30)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Full name can only contain alphabetic characters")]
        public string FullName { get; set; }

        [Required]
        public DateTime ExpDate { get; set; }

        [Required, MinLength(3), MaxLength(3)]
        public string CVV { get; set; }

        [Required, MinLength(4), MaxLength(4)]
        public string PIN { get; set; }

        [Required]
        public int BankAccountId { get; set; }

        [ForeignKey("BankAccountId")]
        public virtual BankAccountEntity BankAccountEntity { get; set; }
    }
}
