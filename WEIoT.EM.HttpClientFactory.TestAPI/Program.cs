using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog.Formatting.Compact;
using Serilog;
using Microsoft.AspNetCore.Builder;
using WEIoT.EM.HttpClientFactory.TestAPI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServicesExtension(builder.Configuration);
builder.Host.UseSerilog((context, config) =>
{
    var logLevel = context.Configuration.GetSection("Logging:LogLevel:Default").Value;
    config.SetLogLevel(logLevel)
    .Enrich.FromLogContext().Filter.ByExcluding((le) => le.RenderMessage().Contains("/health") || le.RenderMessage().Contains("/favicon.ico"))
    .WriteTo.Console(new RenderedCompactJsonFormatter());
});
var app=builder.Build();
app.ExtendedAppBuilder(builder.Environment);
app.Run();


