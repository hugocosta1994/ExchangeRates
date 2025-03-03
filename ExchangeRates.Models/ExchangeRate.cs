using ExchangeRates.Models.Generics;
using System.ComponentModel.DataAnnotations;

namespace ExchangeRates.Models
{
    public class ExchangeRate : BaseModel
    {
        [Required]
        [MaxLength(8)]
        public required string FromCurrency { get; set; }

        [Required]
        [MaxLength(8)]
        public required string ToCurrency { get; set; }

        public double Rate { get; set; }
        public double Ask { get; set; }
        public double Bid { get; set; }
    }
}