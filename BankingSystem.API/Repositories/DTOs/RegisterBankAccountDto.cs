using DB.Entities;
using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class RegisterBankAccountDto
    {
            [Required,MinLength(15), MaxLength(34)]
            public string IBAN { get; set; }

            [Required]
            public double Amount { get; set; }

            [Required]
            public Currency Currency { get; set; }
    }
}
