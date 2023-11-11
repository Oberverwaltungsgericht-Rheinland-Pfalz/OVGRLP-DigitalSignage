// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Serilog;

namespace DigitalSignage.ImportCLI.Logging;

public static class Logger
{
    public static List<ILogType> LoggingTypes = new List<ILogType>();

    [Obsolete]
    public static void InitLogger()
    {
        // Logger kann wohl nicht zentral initialisiert werden, da Serilog sofort die
        // Ressourcen sperrt und diese nicht mehr frei gibt.
        // Aus diesem Grund muss der Logger bei jedem Log initialisiert werden
        // (Aufgefallen ist dies, weil bei SQL-Logging die Datenbank nicht schließt und der Prozess den Webserver auslastet)
        /*
        var Configuration = GetLoggerConfiguration();
        Serilog.Log.Logger = Configuration.CreateLogger();
        */
    }

    /// <summary>
    /// Serilog Instanz anfordern  <para/>
    /// !!!Achtung!!!
    ///    Bei SQL-Logging die Datenbank muss die Logging Instanz selbst wieder terminiert werden,
    ///    Serilog selbst sperrt sofort die Ressourcen und gibt diese selbstständig nicht wieder frei gibt
    /// </summary>
    public static Serilog.Core.Logger CreateLogger()
    {
        var Configuration = GetLoggerConfiguration();
        return Configuration.CreateLogger();
    }

    private static LoggerConfiguration GetLoggerConfiguration()
    {
        var Configuration = new Serilog.LoggerConfiguration();
        List<Action<LoggerConfiguration>> logActions;
        int i;

        foreach (ILogType lType in LoggingTypes)
        {
            logActions = lType.GetConfiguration();
            for (i = 0; i < logActions.Count; i++)
            {
                Configuration.WriteTo.Logger(logActions[i]);
            }
        }

        return Configuration;
    }

    public static void Log<T>(LogEntry entry, string context, T Metadaten)
    {
        var ssv = new SerializationService();
        Log(entry, context, ssv.SerializeTypeAsJsonString<T>(Metadaten));
    }

    public static void Log(LogEntry entry, string context = "", string Metadaten = "")
    {
        var logEntryParsingService = new LogEntryParsingService();
        Log(logEntryParsingService.ParseToStringMessage(entry, 2), context, entry.Level, Metadaten);
    }

    public static void Log<T>(string message, string context, LogEventLevel level, T Metadaten)
    {
        var ssv = new SerializationService();
        Log(message, context, level, ssv.SerializeTypeAsJsonString<T>(Metadaten));
    }

    public static void Log(string message, string context, LogEventLevel level, string Metadaten = "")
    {
        try
        {
            string Template = "{Message}";
            if (!string.IsNullOrEmpty(Metadaten))
                Template = Template + ", {Metadaten}";

            // Logger kann wohl nicht zentral initialisiert werden, da Serilog sofort die
            // Ressourcen sperrt und diese nicht mehr frei gibt.
            // Aus diesem Grund muss der Logger bei jedem Log initialisiert werden
            // (Aufgefallen ist dies, weil bei SQL-Logging die Datenbank nicht schließt und der Prozess den Webserver auslastet)
            //Serilog.Log.Write(level.toSerilogEventLevel(), Template, string.Concat(context, Environment.NewLine, message), Metadaten);
            using (var log = GetLoggerConfiguration()
                    .CreateLogger())
            {
                string l = context;
                if (!string.IsNullOrEmpty(message))
                {
                    if (!string.IsNullOrEmpty(l))
                        l = l + Environment.NewLine;
                    l = l + message;
                }
                log.Write(level.toSerilogEventLevel(), Template, l, Metadaten);
            }
        }
        catch (Exception ex)
        {
#if DEBUG
            System.Diagnostics.Debug.Write(ex.Message);
#endif
        }
    }
}