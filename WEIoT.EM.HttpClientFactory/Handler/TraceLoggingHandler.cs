using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WEIoT.EM.HttpClientFactory.Handler
{
    public class TraceLoggingHandler: DelegatingHandler
    {
        private readonly ILogger<TraceLoggingHandler> _log;
        public TraceLoggingHandler(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _log = loggerFactory.CreateLogger<TraceLoggingHandler>();
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            _log.LogWarning($"HttpRequest=> Date&Time: {DateTime.Now} Uri:{request.RequestUri}-Type:{request.Method}-Body:{request.Content?.ReadAsStringAsync()}");
            var response = await base.SendAsync(request, cancellationToken);
            var responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (!response.IsSuccessStatusCode)
            {
                _log.LogWarning($"Respose:Error => {responseBody}");
            }
            else
            {
                _log.LogWarning($"Response=> ResponseBody: {responseBody}");
            }
            return response;
        }
    }
}
