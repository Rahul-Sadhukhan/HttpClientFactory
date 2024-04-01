using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using WEIoT.EM.ServiceRegistry.Helper.Options;
using WEIoT.EM.ServiceRegistry.Helper.Services;

namespace WEIoT.EM.HttpClientFactory.Handler
{
    public class EMHttpMessageHandler : DelegatingHandler
    {
        private readonly IHeaderGenerator _headerGenerator;
        private readonly ServiceRegistryOptions _serviceRegistryOptions;
        private readonly ILogger<EMHttpMessageHandler> _log;
        public EMHttpMessageHandler(ILoggerFactory loggerFactory, IHeaderGenerator headergenerator, IOptions<ServiceRegistryOptions> serviceRegistryOptions)
        {
            _log = loggerFactory.CreateLogger<EMHttpMessageHandler>();
            _headerGenerator = headergenerator;
            _serviceRegistryOptions = serviceRegistryOptions.Value;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {

            var serviceRegistryCredential = _serviceRegistryOptions?.ServiceRegistryCredentials?
                                            .FirstOrDefault(x => request.RequestUri.ToString().Contains(x.BaseUrl.ToString(), StringComparison.OrdinalIgnoreCase));
            if(serviceRegistryCredential==null)
            {
                _log.LogError($"Error => No mandatory headers (ConsumerId, ServiceName, Environment, Version) provided for the current request");
                throw new ArgumentNullException();
            }
            var headers = _headerGenerator.GenerateHeaders(serviceRegistryCredential?.ServiceName);
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
            return await base.SendAsync(request, cancellationToken);
        }

    }
}
