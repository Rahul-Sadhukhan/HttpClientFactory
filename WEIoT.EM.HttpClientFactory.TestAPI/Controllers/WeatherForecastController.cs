using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WEIoT.EM.HttpClientFactory.Validators;

namespace WEIoT.EM.HttpClientFactory.TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly ILogger<WeatherForecastController> _logger;
        public string BaseAddress = "http://weiot-em-wsp.dev.walmart.net";
        private readonly string clientName;
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IEMHttpClientHandlerHelper clientHelper)
        {
            clientName = "WeatherForecastClient";
            _client = clientHelper.CreateClient(clientName, BaseAddress);
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {
            using (var response = _client.GetAsync("/iot/api/stores"))
            {
                var stream = await response.ConfigureAwait(false);
                var result=stream.Content.ReadAsStringAsync();
                Console.WriteLine(result.Result.ToString());
            }

        }
    }
}

