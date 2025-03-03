namespace ExchangeRates.Models.DTOs
{
	public partial record ExchangeRateDTO
	{
		public string Id {get;set;}
		public string FromCurrency {get;set;}
		public string ToCurrency {get;set;}
		public double Rate {get;set;}
		public double Ask {get;set;}
		public double Bid {get;set;}
		public System.DateTime CreatedAt {get;set;}
	}
}