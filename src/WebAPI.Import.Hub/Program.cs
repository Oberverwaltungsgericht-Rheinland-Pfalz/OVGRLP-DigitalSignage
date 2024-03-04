using Microsoft.AspNetCore.Authentication.Negotiate;
using Serilog;
using System.Diagnostics;
using System.Reflection;

namespace Services.Import.Hub;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Use Serilog...
        builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Logging.ClearProviders();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
            .AddNegotiate();

        var app = builder.Build();

        app.UseSerilogRequestLogging();

        var logger = app.Services.GetRequiredService<Serilog.ILogger>();
        var assembly = Assembly.GetExecutingAssembly();
        var info = FileVersionInfo.GetVersionInfo(assembly.Location);
        logger.Information($"{info.ProductName} - {info.CompanyName} - v{info.FileVersion}");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
