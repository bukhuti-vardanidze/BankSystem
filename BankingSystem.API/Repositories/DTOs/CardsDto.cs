using System.ComponentModel.DataAnnotations;

namespace Repositories.DTOs
{
    public class CardsDto
    {
        public string CardNumber { get; set; }
        public string FullName { get; set; }
        public DateTime ExpDate { get; set; }
    }
}
