// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.AspNetCore.Authentication.Negotiate;
using Serilog;

namespace DigitalSignage.dn.WebApiCore;
public partial class Program
{
    public static bool NoTest { get; set; } = true;
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Services.AddSystemWebAdapters();
        // builder.Services.AddHttpForwarder();

        builder.Host
            .UseSerilog((context, services, configuration) =>
              configuration.WriteTo.File("./logs/log-.txt", rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true));
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console().WriteTo.File("./logs/bootstrap.txt")
            .CreateBootstrapLogger();

        var startup = new Startup(builder);
        
        startup.AddDependencyInjection();
        if (NoTest)
        {
            builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
                .AddNegotiate();

            builder.Services.AddAuthorization(options =>
            {
                options.FallbackPolicy = options.DefaultPolicy;
            });
        }
        var app = builder.Build();

        startup.ConfigureServices(app);
        //app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

        app.Run();
    }
}
