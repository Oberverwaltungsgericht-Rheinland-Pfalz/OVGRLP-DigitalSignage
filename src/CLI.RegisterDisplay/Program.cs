using CLI.Register.Services;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace CLI.Register;

// TODO: appsettings.json must be delivered too...
internal class Program
{
    static void Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
            .Build();

        // Configure Serilog
        var logger = new LoggerConfiguration()
            .ReadFrom.Configuration(config)
            .CreateLogger();

        if (logger == null)
        {
            Console.WriteLine("Could not create Logger instance");
            return;
        }

        if (!Environment.IsPrivilegedProcess)
        {
            logger.Error("This CLI needs privileged Rights. Please run as administrator");
            return;
        }

        // Start the Register-Process...
        LocalService.RegisterDevice(config, logger);

#if DEBUG
        Console.WriteLine("\n\nBitte Taste drücken...");
        Console.ReadKey();
#endif
    }
}
