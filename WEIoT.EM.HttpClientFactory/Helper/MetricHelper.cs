using Prometheus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEIoT.EM.HttpClientFactory.Helper
{
    public class MetricHelper : IMetricHelper
    {
        Gauge _httpMetric;
        public MetricHelper() {
            _httpMetric = Metrics.CreateGauge("http_request_duration_seconds_count", "Http request response duration");
        }
        public void SetHttpMetric(double _responseTime)
        {
            _httpMetric.Set(_responseTime);
        }

    }
}
