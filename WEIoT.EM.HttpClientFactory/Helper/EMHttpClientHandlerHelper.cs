using System;
using System.Net.Http;
using WEIoT.EM.HttpClientFactory.Handler;

namespace WEIoT.EM.HttpClientFactory.Validators
{
    public class EMHttpClientHandlerHelper : IEMHttpClientHandlerHelper
    {
        private readonly IHttpClientFactory _clientFactory;
        public EMHttpClientHandlerHelper(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public HttpClient CreateClient(string clientName, string BaseAddress)
        {
            var _client = _clientFactory.CreateClient(clientName);
            _client.BaseAddress = new Uri(BaseAddress);
            return _client;
        }
    }
}
