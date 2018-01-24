using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DigitalSignage.ImportCLI.Services
{
  public class CLIService
  {
    public CLIService()
    {
      System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
    }

    //! Kommandozeilenargumente auswerten
    public void ParseCommandLineArguments(String[] args, out List<string> inputFiles, out bool ClearDatabase)
    {
      Match match = null;
      MatchCollection matches = null;
      String cmdline = "\"" + String.Join("\"", args) + "\"";

      //Defaults
      inputFiles = new List<string>();
      ClearDatabase = false;

      // -h Hilfe anzeigen
      match = Regex.Match(cmdline, "-h");
      if (match.Success)
      {
        WriteHelp();
      }

      // -fi Eingabedatei festlegen
      matches = Regex.Matches(cmdline, "-add *(\".+?\"|[^ ]+?)");
      if (matches.Count > 0)
      {
        foreach (Match mat in matches)
        {
          inputFiles.Add(mat.Groups[1].Value.Replace("\"", ""));
        }
      }

      // -c ClearDatabase
      match = Regex.Match(cmdline, "-c");
      if (match.Success)
      {
        ClearDatabase = true;
      }
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private void WriteHelp()
    {
      Trace("  -add XML-Datendatei für die Verarbeitung vormerken");
      Trace("  -c   Datenbank vor dem hinzufügen von Daten löschen");
      Trace("  -h   Hilfe anzeigen");
    }

    //! Ausnahmefehler protokollieren
    public void Trace(Exception ex)
    {
      Exception exx = ex;
      do
      {
        Trace(exx.Message, exx.StackTrace);
        exx = exx.InnerException;
      } while (null != exx);
    }

    //! Programmausgaben protokollieren
    public void Trace(params String[] args)
    {
      foreach (var i in args)
      {
        System.Diagnostics.Trace.WriteLine(i);
      }
    }
  }
}