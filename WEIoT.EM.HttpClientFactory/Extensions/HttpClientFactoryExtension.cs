using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WEIoT.EM.HttpClientFactory.Handler;
using WEIoT.EM.HttpClientFactory.Helper;
using WEIoT.EM.HttpClientFactory.Validators;
using WEIoT.EM.ServiceRegistry.Helper.Extensions;

namespace WEIoT.EM.HttpClientFactory.Extensions
{
    public static class HttpClientFactoryExtension
    {
        public static IServiceCollection ConfigureHTTPClientFactory(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHandlers(configuration);
            services.AddScoped<IEMHttpClientHandlerHelper, EMHttpClientHandlerHelper>();
            return services;
        }
        public static IServiceCollection AddHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceRegistryHelper(configuration);
            services.AddScoped<IMetricHelper, MetricHelper>();
            services.AddScoped<EMHttpMessageHandler>();
            services.AddScoped<TraceLoggingHandler>();
            services.AddScoped<MetricHandler>();
            return services;
        }
    }
}

