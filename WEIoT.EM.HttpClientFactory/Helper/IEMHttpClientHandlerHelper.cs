using System.Net.Http;


namespace WEIoT.EM.HttpClientFactory.Validators
{
    public interface IEMHttpClientHandlerHelper
    { 
        public HttpClient CreateClient(string clientName, string BaseAddress);
    }
}
