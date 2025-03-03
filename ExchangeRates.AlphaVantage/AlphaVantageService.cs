using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ExchangeRates.AlphaVantage
{
    public class AlphaVantageService
    {
        private readonly string _apiKey;
        private readonly ILogger<AlphaVantageService> _logger;

        public AlphaVantageService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<AlphaVantageResponse?> GetAsync(string fromCurrency, string toCurrency)
        {
            var url = $"https://www.alphavantage.co/query?function=CURRENCY_EXCHANGE_RATE&from_currency={fromCurrency}&to_currency={toCurrency}&apikey={_apiKey}";
            try
            {
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(url);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<AlphaVantageResponse>(content);
                        return result;
                    }
                    else
                    {
                        throw new Exception("Failed to get exchange rate from Alpha Vantage.");
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError($"An error occurred while sending the request: {ex.Message}");
                throw new Exception("Failed to get exchange rate from Alpha Vantage.");
            }
        }
    }
}
