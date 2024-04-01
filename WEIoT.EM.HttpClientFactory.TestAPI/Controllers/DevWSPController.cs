using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using WEIoT.EM.HttpClientFactory.Validators;
using System.Collections.Generic;

namespace WEIoT.EM.HttpClientFactory.TestAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DevWSPController : ControllerBase
    {
        private HttpClient _client;
        private readonly ILogger<DevWSPController> _logger;
        public string BaseAddress = "https://weiot-em-storeinfowebapi.qa.walmart.com/";
        private readonly string clientName;
        public Dictionary<string, string> _headers = new Dictionary<string, string>();
        public DevWSPController(ILogger<DevWSPController> logger, IEMHttpClientHandlerHelper clientHelper)
        {
            clientName = "DevWSPClient";
            _client = clientHelper.CreateClient(clientName, BaseAddress);
            foreach(var header in _headers)
            {
                _client.DefaultRequestHeaders.Add(header.Key, header.Value);
            }
            
            _logger = logger;
        }

        [HttpGet]
        public async Task Get()
        {
            var response = _client.GetAsync(string.Format("{0}/{1}/{2}", RouteCollection.GetStoreProfile, "US", 121));
            var stream = await response.ConfigureAwait(false);
            //var result = JsonSerializer.Deserialize<StoreProfile>(stream.Content.ReadAsStringAsync().Result);
            //var result = stream.Content.ReadAsStringAsync();
            Console.WriteLine(stream.Content.ReadAsStringAsync().Result.ToString());
        }
    }
}
