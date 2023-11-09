// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using NDesk.Options;
using OvgRlp.Core.Common;
using System;

namespace DigitalSignage.ImportCLI.Service
{
  public class CLIService
  {
    public CLIService()
    {
      System.Diagnostics.Trace.Listeners.Add(new System.Diagnostics.ConsoleTraceListener());
    }

    //! Kommandozeilenargumente auswerten
    public CLIActions ParseCommandLineArguments(String[] args, bool validate = true)
    {
      var cliActions = new CLIActions();

      var p = new OptionSet();
      p.Add("a|add=", "{XML-Datei} für die Verarbeitung vormerken.", v => cliActions.InputFiles.Add(v.Replace("\"", "")));
      p.Add("c|con=", HelpForConnectionString(), v => cliActions.NameOrConnectionString = v);
      p.Add("d|delete", "Datenbank (vor dem hinzufügen von Daten) löschen.", v => { if (v != null) cliActions.ClearDatabase = true; });
      p.Add("h|?|help", "Hilfe anzeigen.", v => { if (v != null) cliActions.WritingInformationToUser = true; ShowHelp(p); });
      p.Add("l|log=", "Angabe eines {Dateiname}ns oder Verzeichnisses um Ausgaben zu loggen.", v => cliActions.LogFile = v.Replace("\"", ""));
      p.Add("u|update=", "{XML-Datei} für Datenupdate vormerken.", v => cliActions.UpdateFiles.Add(v.Replace("\"", "")));
      p.Add("v|version", "Versionsinformationen anzeigen.", v => { if (v != null) cliActions.WritingInformationToUser = true; ShowVersionInformation(); });

      try
      {
        p.Parse(args);
        if (validate)
          cliActions.ValidateActions();
      }
      catch (Exception e)
      {
        Console.Write("DSImportCLI: ");
        Console.WriteLine(e.Message);
        Console.WriteLine("Bitte verwenden Sie `DSImportCLI --help' für mehr Informationen.");
        return null;
      }

      return cliActions;
    }

    private static void ShowVersionInformation()
    {
      Console.WriteLine("DSImportCLI Version " + AssemblyHelper.AssemblyVersion(System.Reflection.Assembly.GetExecutingAssembly()));
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private static void ShowHelp(OptionSet p)
    {
      Console.WriteLine("Verwendung: DSImportCLI [OPTIONEN]+ [OPTIONSWERTE]");
      Console.WriteLine();
      Console.WriteLine("OPTIONEN:");
      p.WriteOptionDescriptions(Console.Out);
    }

    //! Übersicht der möglichen Kommandozeilenargumente auf der Konsole ausgeben
    private string HelpForConnectionString()
    {
      string help = string.Concat(
        "ConnectionName oder {ConnectionString}", "\n",
        "Hier kann ein ConnectionString in folgendem Format angegeben werden:", "\n",
        "\"Server=[DBServername]\\[DBInstanz]; Database=[DBName]; Integrated Security=True\"", "\n",
        "In der Konfigurationsdatei sind momentan folgende hinterlegt:");
      foreach (System.Configuration.ConnectionStringSettings connection in System.Configuration.ConfigurationManager.ConnectionStrings)
      {
        if (connection.Name != "LocalSqlServer")
        {
          help = help + String.Format("\n  {0}: {1}", connection.Name, connection.ConnectionString);
        }
      }
      return help;
    }
  }
}