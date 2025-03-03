using ExchangeRates.Models;
using ExchangeRates.Models.DTOs;
using ExchangeRates.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRates.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRateController : ControllerBase
    {
        private readonly IExchangeRateService _exchangeRateService;
        private readonly ILogger<ExchangeRateController> _logger;

        public ExchangeRateController(IExchangeRateService exchangeRateService, ILogger<ExchangeRateController> logger)
        {
            _exchangeRateService = exchangeRateService;
            _logger = logger;
        }

        [HttpGet("All", Name = "GetExchangeRates")]
        [ProducesResponseType(typeof(ExchangeRateDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var exchangeRates = await _exchangeRateService.GetAllAsync();
                var lst = exchangeRates.Select(l => l.ToExchangeRateDTO());
                return Ok(lst);
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting all exchange rates.\n" + ex.Message);
                throw;
            }
        }

        [HttpGet(Name = "GetExchangeRate")]
        [ProducesResponseType(typeof(ExchangeRateDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetExchangeRate([FromQuery] string baseCurrency, [FromQuery] string targetCurrency)
        {
            try
            {

                if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(targetCurrency))
                {
                    return BadRequest("Base currency and target currency must be provided.");
                }

                var exchangeRate = await _exchangeRateService.GetAsync(baseCurrency, targetCurrency, createIfNotExist: true);
                if (exchangeRate == null)
                {
                    return NotFound("Exchange rate not found.");
                }

                return Ok(exchangeRate.ToExchangeRateDTO());
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while getting exchange rate.\n" + ex.Message);
                throw;
            }
        }

        [HttpDelete(Name = "DeleteExchangeRate")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteExchangeRate([FromQuery] string baseCurrency, [FromQuery] string targetCurrency)
        {
            try
            {
                if (string.IsNullOrEmpty(baseCurrency) || string.IsNullOrEmpty(targetCurrency))
                {
                    return BadRequest("Base currency and target currency must be provided.");
                }
                var exchangeRate = await _exchangeRateService.GetAsync(baseCurrency, targetCurrency);
                if (exchangeRate == null)
                {
                    return NotFound("Exchange rate not found.");
                }
                await _exchangeRateService.RemoveAsync(exchangeRate.Id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting exchange rate.\n" + ex.Message);
                throw;
            }
        }
    }
}
