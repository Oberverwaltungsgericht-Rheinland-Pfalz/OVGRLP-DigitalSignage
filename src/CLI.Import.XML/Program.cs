using CLI.Import.XML.Services;
using Microsoft.Extensions.Configuration;

namespace CLI.Import.XML;

internal class Program
{
    static void Main(string[] args)
    {
        var cliService = new CLIService();

        try
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appSettings.json", optional: false);

            var config = builder.Build();

            CLIActions? cliActions = cliService.ParseCommandLineArguments(args, config["import_api"] ?? "");
            if (cliActions == null)
                return;

            cliActions.ExecuteActions(cliService);
        }
        catch (Exception ex)
        {
            // TODO: Logging
            Console.WriteLine(ex.Message);
        }

#if DEBUG
        Console.WriteLine("\n\nBitte Taste drücken...");
        Console.ReadKey();
#endif
    }
}
