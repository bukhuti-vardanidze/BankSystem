using System.ComponentModel.DataAnnotations;

namespace DB.Entities
{
    public class ExchangeRateEntity
    {
        [Key]
        public int ExchangeCurrencyId { get; set; }

        [Required]
        public Currency FromCurrency { get; set; }

        [Required]
        public Currency ToCurrency { get; set; }

        [Required]
        public double CurrencyRate { get; set; }
    }
}
