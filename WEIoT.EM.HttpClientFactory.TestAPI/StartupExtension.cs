using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Prometheus;
using Serilog;
using Walmart.EventManager.Common.Packages.Tenancy;
using WEIoT.EM.HttpClientFactory.Extensions;
using WEIoT.EM.HttpClientFactory.Validators;
using WEIoT.EM.TenancyHelper.Extensions;

namespace WEIoT.EM.HttpClientFactory.TestAPI
{
    public static class StartupExtension
    {

        public static IConfiguration Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public static IServiceCollection AddServicesExtension(this IServiceCollection services, IConfiguration configuration)
        {
            Configuration = configuration;
            services.AddTenancyHelper();
            services.Configure<TenantPipelineSettings>(Configuration.GetSection("TenantPipelineSettings"));
            //services.Configure<TenantPipelineSettings>(Configuration.GetSection("TenantPipelineSettings"));
            //services.AddTransient<IEMHttpClientHandlerHelper, EMHttpClientHandlerHelper>();
            services.ConfigureHTTPClientFactory(Configuration);
            services.AddControllers();
            return services;
        }
        public static LoggerConfiguration SetLogLevel(this LoggerConfiguration config, string MinimumLevelType)
        {
            if (MinimumLevelType == "Information")
            {
                config.MinimumLevel.Information();
            }
            else if (MinimumLevelType == "Warning")
            {
                config.MinimumLevel.Warning();
            }
            else
            {
                config.MinimumLevel.Warning();
            }

            return config;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static WebApplication ExtendedAppBuilder(this WebApplication app, IWebHostEnvironment env)
        {
            if (env.EnvironmentName.Equals("Development"))
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();
            app.UseMetricServer();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }
    }
}

