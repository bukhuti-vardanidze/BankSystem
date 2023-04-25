using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class TransactionDto
    {
        [Required, MaxLength(34)]
        public string SenderIBAN { get; set; }

        [Required, MaxLength(34)]
        public string RecipientIBAN { get; set; }

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be a positive number.")]
        public double Amount { get; set; }
    }
}