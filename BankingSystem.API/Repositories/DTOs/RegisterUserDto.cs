using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class RegisterUserDto
    {
        [Required, MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "First name can only contain alphabetic characters")]
        public string FirstName { get; set; }

        [Required, MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Last name can only contain alphabetic characters")]
        public string LastName { get; set; }

        [Required, MinLength(9), MaxLength(20)]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "ID Number can only contain digits")]
        public string IDNumber { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        [Required, EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }

        [Required, Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
