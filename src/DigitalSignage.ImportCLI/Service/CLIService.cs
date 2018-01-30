using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace DigitalSignage.ImportCLI.Service
{
  public class CLIService
  {
    public CLIService()
    {
      System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
    }

    //! Kommandozeilenargumente auswerten
    public CLIActions ParseCommandLineArguments(String[] args)
    {
      var cliActions = new CLIActions();
      Match match = null;
      MatchCollection matches = null;
      String cmdline = "\"" + String.Join("\"", args) + "\"";

      // -h Hilfe anzeigen
      match = Regex.Match(cmdline, "-h");
      if (match.Success)
      {
        WriteHelp();
        return null;
      }

      // -fi Eingabedatei festlegen
      matches = Regex.Matches(cmdline, "-add *(\".+?\"|[^ ]+?)");
      if (matches.Count > 0)
      {
        foreach (Match mat in matches)
        {
          cliActions.InputFiles.Add(mat.Groups[1].Value.Replace("\"", ""));
        }
      }

      // -c ClearDatabase
      match = Regex.Match(cmdline, "-c");
      if (match.Success)
      {
        cliActions.ClearDatabase = true;
      }

      return cliActions;
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private void WriteHelp()
    {
      Trace("  -add XML-Datendatei für die Verarbeitung vormerken");
      Trace("  -c   Datenbank (vor dem hinzufügen von Daten) löschen");
      WriteHelpForConnectionString();
      Trace("  -h   Hilfe anzeigen");
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private void WriteHelpForConnectionString()
    {
      Trace("  -con ConnectionName oder ConnectionString");
      Trace("       Hier kann entweder ein ConnectionString in folgendem Format angegeben werden:");
      Trace("         Server=[DBServername]\\[DBInstanz]; Database=[DBName]; Integrated Security=True");
      Trace("       Alternativ kann ein ConnectionName aus der Konfigurationsdatei angegeben werden.");
      Trace("       In der Konfigurationsdatei sind momentan folgende hinterlegt:");
      foreach (System.Configuration.ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
      {
        if (connection.Name != "LocalSqlServer")
        {
          Trace(String.Format("         {0}: {1}", connection.Name, connection.ConnectionString));
        }
      }
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