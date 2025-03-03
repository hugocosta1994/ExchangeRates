namespace ExchangeRates.Models
{
    public static partial class ExchangeRateMappingExtension
    {
        public static DTOs.ExchangeRateDTO ToExchangeRateDTO(this ExchangeRate source)
        {
            return new DTOs.ExchangeRateDTO
            {
				Id = source.Id.ToString(),
				FromCurrency = source.FromCurrency,
				ToCurrency = source.ToCurrency,
				Rate = source.Rate,
				Ask = source.Ask,
				Bid = source.Bid,
				CreatedAt = source.CreatedAt,
            };
        }  
    }
}