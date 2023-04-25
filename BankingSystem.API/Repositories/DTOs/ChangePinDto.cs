using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class ChangePinDto
    {
        [Required, MinLength(16), MaxLength(16)]
        public string CardNumber { get; set; }

        [Required, MinLength(4), MaxLength(4)]
        public string PIN { get; set; }

        [Required, MinLength(4), MaxLength(4)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "New PIN must contain only numbers")]
        public string NewPIN { get; set; }
    }
}
