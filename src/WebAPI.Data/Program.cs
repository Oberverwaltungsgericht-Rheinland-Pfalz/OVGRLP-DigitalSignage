// using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Services.Database.Services;
using System.Diagnostics;
using System.Reflection;

namespace Services.Database;

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

        // Hint: Changing the common Id-Type not only here. The Type does not get passed to all the necessary classes
        // The Type also needs to be changed in the Controller Classes, Asp.Net does not allow generic Controllers 
        // without any hacks
        if (builder.Environment.IsDevelopment())
        {
            // logger.Debug("Using In-Memory-Database...");
            // Use In-memory-DB for Debugging purposes...
            builder.Services.AddDbContextPool<DSContext>(options => 
                options.UseInMemoryDatabase("DigitalSignage"));
        }
        else
        {
            // logger.Debug("Using SQL-Server...");
            // Use a production DB for regular use...
            builder.Services.AddDbContextPool<DSContext>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DigitalSignage")));
        }

        var collection = builder.Services.AddSingleton<IWorkService, WorkService>();

        var app = builder.Build();

        app.UseSerilogRequestLogging();

        app.UseCors(options =>
        {
            options.AllowAnyOrigin();
            options.AllowAnyMethod();
            options.AllowAnyHeader();
        });

        var logger = app.Services.GetRequiredService<Serilog.ILogger>();
        var assembly = Assembly.GetExecutingAssembly();
        var info = FileVersionInfo.GetVersionInfo(assembly.Location);
        logger.Information($"{info.ProductName} - {info.CompanyName} - v{info.FileVersion}");

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            // Testdata only for the In-memory database...
            var workService = app.Services.GetRequiredService<IWorkService>();
            if (workService != null) TestData.InsertTestData(workService);

            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();

    }
}
