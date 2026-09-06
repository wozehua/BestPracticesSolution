using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Hybrid;
using System.Xml.Linq;

namespace Web.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly ILogger<WeatherForecastController> _logger;
        private static readonly Action<ILogger, Exception?> _logGetWeatherForecastCalled =
            LoggerMessage.Define(LogLevel.Information, new EventId(0, nameof(Get)), "GetWeatherForecast called");

        private readonly HybridCache _hybridCache;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, HybridCache hybridCache)
        {
            _logger = logger;
            _hybridCache = hybridCache;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logGetWeatherForecastCalled(_logger, null);
            return [.. Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = $"{Summaries[Random.Shared.Next(Summaries.Length)]}{789}"
            })];
        }

        [HttpPost("GetOrCreateAsync")]
        public async Task<string> GetOrCreateAsync(string name,int id,CancellationToken token)
        {
            return await _hybridCache.GetOrCreateAsync(
              $"{name}-{id}", // Unique key to the cache entry
              async cancel => await GetDataFromTheSourceAsync(name, id, cancel),
              new HybridCacheEntryOptions
              {
                  LocalCacheExpiration=TimeSpan.FromMinutes(5),
                  Expiration=TimeSpan.FromMinutes(30)
              },
              tags: ["products"],
              cancellationToken: token
          );
        }

        [NonAction]
        public async Task<string> GetDataFromTheSourceAsync(string name, int id, CancellationToken token)
        {
            // Placeholder for retrieving data from a real backing store (database, HTTP API, etc.).
            // A real implementation would await that call; this stub returns synchronously,
            // so it intentionally has no await (and produces compiler warning CS1998).
            string someInfo = $"someinfo-{name}-{id}";
            //token.Cancel(100);
            return someInfo;
        }
    }
}