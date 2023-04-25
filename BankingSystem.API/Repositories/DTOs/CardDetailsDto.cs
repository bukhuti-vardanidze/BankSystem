using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class CardDetailsDto
    {
        [Required, MinLength(16), MaxLength(16)]
        public string CardNumber { get; set; }

        [Required, MinLength(4), MaxLength(4)]
        public string PIN { get; set; }
    }
}
