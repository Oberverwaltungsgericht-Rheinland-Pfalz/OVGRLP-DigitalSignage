using System.Diagnostics;
using NDesk.Options;

namespace CLI.Import.XML.Services;

public class CLIService
{
    public OptionSet Options { get; set; }

    public CLIService()
    {
        // Output Trace to StdOut
        Trace.Listeners.Add(new ConsoleTraceListener());
        Options = [];
    }

    public CLIActions? ParseCommandLineArguments(string[] args, string host, bool validate = true)
    {
        var cliActions = new CLIActions(host);

        // Configure all available CLI-Commands
        var p = new OptionSet
        {
            { "f|file=",    "{XML-Datei} welche importiert werden soll(en)",                        v => cliActions.InputFiles.Add(v.Replace("\"", "")) },
            { "verbose",    "Show Verbose informations in CLI",                                     v => { if (v != null) cliActions.Verbose = true; } },
            { "d|delete",   "Datenbank vor dem Importieren löschen",                                v => { if (v != null) cliActions.ClearDatabase = true; } },
            { "t|test",     "Import nicht in Datenbank ubertragen und nur Konsolenausgabe tätigen", v => { if (v != null) cliActions.TestRun = true; } },
            { "v|version",  "Versionsinformationen anzeigen",                                       v => { if (v != null) cliActions.ShowInformation = true; } },
            { "h|?|help",   "Hilfe anzeigen",                                                       v => { if (v != null) cliActions.ShowHelp = true; } }
        };
        Options = p;

        try
        {
            p.Parse(args);
            if (validate) cliActions.ValidateActions();
        }
        catch (Exception ex)
        {
            Console.Write("CLI.Import.XML: ");
            Console.WriteLine(ex.Message);
            Console.WriteLine("Bitte verwenden Sie `CLI.Import.XML --help` für mehr Informationen.");
            return null;
        }

        return cliActions;
    }
}
