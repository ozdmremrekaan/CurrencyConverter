using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace CurrencyConverter.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CurrencyController : ControllerBase
    {
        [HttpGet("import-exchange-rates")]
        public async Task<IActionResult> ImportExchangeRates()
        {
            try
            {
                string apiKey = "8103a2cb923d6a89f1b57b10";
                string baseCurrency = "TRY";
                string apiUrl = $"https://v6.exchangerate-api.com/v6/{apiKey}/latest/{baseCurrency}";

                using (var httpClient = new HttpClient())
                {
                    var json = await httpClient.GetStringAsync(apiUrl);
                    API_Obj exchangeRates = JsonConvert.DeserializeObject<API_Obj>(json);

                    return Ok(exchangeRates.conversion_rates);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return BadRequest(new { Error = $"API Error: {ex.Message}" });
            }
        }
    }

    public class API_Obj
    {
        public string result { get; set; }
        public string documentation { get; set; }
        public string terms_of_use { get; set; }
        public string time_last_update_unix { get; set; }
        public string time_last_update_utc { get; set; }
        public string time_next_update_unix { get; set; }
        public string time_next_update_utc { get; set; }
        public string base_code { get; set; }
        public ConversionRate conversion_rates { get; set; }
    }

    public class ConversionRate
    {
        public double EUR { get; set; }
        public double USD { get; set; }
        public double ZAR { get; set; }
        public double GBP { get; set; }
        public double JPY { get; set; }
    }

}

