using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WEIoT.EM.HttpClientFactory.Helper;

namespace WEIoT.EM.HttpClientFactory.Handler
{
    public class MetricHandler : DelegatingHandler
    {
        IMetricHelper _metricHelper;
        public MetricHandler(IMetricHelper metricHelper)
        {
            _metricHelper = metricHelper;
        }
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var watch = Stopwatch.StartNew();
            var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            watch.Stop();
            _metricHelper.SetHttpMetric(watch.Elapsed.TotalMinutes);
            Console.WriteLine($"Response Time: {watch.Elapsed.TotalMinutes}");
            return response;
        }
    }
}
