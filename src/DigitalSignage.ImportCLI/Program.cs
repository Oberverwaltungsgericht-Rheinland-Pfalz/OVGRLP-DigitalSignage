using System;

namespace DigitalSignage.ImportCLI
{
  internal class Program
  {
    private static void Main(string[] args)
    {
      var cliService = new Service.CLIService();

      try
      {
        CLIActions cliActions = cliService.ParseCommandLineArguments(args);
        if (null != cliActions)
          cliActions.ExecuteActions();
      }
      catch (Exception ex)
      {
        Service.LoggingHelper.Trace(ex);
      }

#if DEBUG
      Console.WriteLine("\n\n\nBitte Taste drücken...");
      Console.ReadKey();
#endif
    }
  }
}