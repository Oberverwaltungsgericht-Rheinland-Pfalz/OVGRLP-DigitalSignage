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
      if (Regex.Match(cmdline, "-help").Success || Regex.Match(cmdline, "-h").Success)
      {
        WriteHelp();
        return null;
      }

      // -add Quell-XML aufnehmen
      matches = Regex.Matches(cmdline, "-add *(\".+?\"|[^ ]+?)");
      if (matches.Count > 0)
      {
        foreach (Match mat in matches)
        {
          cliActions.InputFiles.Add(mat.Groups[1].Value.Replace("\"", ""));
        }
      }

      // -clear ClearDatabase
      match = Regex.Match(cmdline, "-clear");
      if (match.Success)
      {
        cliActions.ClearDatabase = true;
      }

      // -con ConnectionString oder -Name festlegen
      matches = Regex.Matches(cmdline, "-con *(\".+?\"|[^ ]+?)");
      if (matches.Count > 0)
      {
        cliActions.NameOrConnectionString = matches[0].Groups[1].Value.Replace("\"", "");
      }

      // -log logFile oder logDir festlegen
      matches = Regex.Matches(cmdline, "-log *(\".+?\"|[^ ]+?)");
      if (matches.Count > 0)
      {
        cliActions.LogFile = matches[0].Groups[1].Value.Replace("\"", "");
      }

      return cliActions;
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private void WriteHelp()
    {
      LoggingHelper.Trace("  -add   XML-Datendatei für die Verarbeitung vormerken");
      LoggingHelper.Trace("  -clear Datenbank (vor dem hinzufügen von Daten) löschen");
      WriteHelpForConnectionString();
      LoggingHelper.Trace("  -log   Angabe eines Dateinames oder Verzeichnisses um Ausgaben zu loggen");
      LoggingHelper.Trace("  -help  Hilfe anzeigen");
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private void WriteHelpForConnectionString()
    {
      LoggingHelper.Trace("  -con   ConnectionName oder ConnectionString");
      LoggingHelper.Trace("         Hier kann ein ConnectionString in folgendem Format angegeben werden:");
      LoggingHelper.Trace("           Server=[DBServername]\\[DBInstanz]; Database=[DBName]; Integrated Security=True");
      LoggingHelper.Trace("         Alternativ kann ein ConnectionName aus der Konfigurationsdatei angegeben werden.");
      LoggingHelper.Trace("         In der Konfigurationsdatei sind momentan folgende hinterlegt:");
      foreach (System.Configuration.ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
      {
        if (connection.Name != "LocalSqlServer")
        {
          LoggingHelper.Trace(String.Format("           {0}: {1}", connection.Name, connection.ConnectionString));
        }
      }
    }
  }
}