using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        cliService.Trace(ex);
      }

#if DEBUG
      Console.WriteLine("\n\n\nBitte Taste drücken...");
      Console.ReadKey();
#endif
    }
  }
}