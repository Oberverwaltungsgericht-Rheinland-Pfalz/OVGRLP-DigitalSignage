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
      List<string> inputFiles;
      bool clearDB;
      var cliService = new Services.CLIService();

      try
      {
        cliService.ParseCommandLineArguments(args, out inputFiles, out clearDB);

        if (inputFiles == null || inputFiles.Count == 0)
          throw new ArgumentException("Es wurden keine XML-Einlesedateien angegeben!");
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