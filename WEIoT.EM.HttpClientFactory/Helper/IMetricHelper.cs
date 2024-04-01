using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WEIoT.EM.HttpClientFactory.Helper
{
    public interface IMetricHelper
    {
        public void SetHttpMetric(double _responseTime);
    }
}
