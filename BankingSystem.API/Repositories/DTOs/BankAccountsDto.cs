using DB.Entities;
using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class BankAccountsDto
    {
        [Required]
        [RegularExpression(@"^[A-Z]{2}\d{2}[A-Z\d]{1,30}$", ErrorMessage = "Invalid IBAN format.")]
        public string IBAN { get; set; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public Currency Currency { get; set; }
    }
}
