// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Serilog;

namespace DigitalSignage.ImportCLI.Logging.LogTypes;

public class LogTypeFile : ILogType
{
    private bool oneFileForAll = false;
    public string LogDirectory { get; set; }
    public string LogFileIformation { get; set; }
    public string LogFileWarning { get; set; }
    public string LogFileError { get; set; }
    public string LogFileFatal { get; set; }

    public LogTypeFile(string logDir,
                       string logFileIformation = "Info.log",
                       string logFileWarning = "Warn.log",
                       string logFileError = "Error.log",
                       string logFileFatal = "Fatal.log")
    {
        this.LogDirectory = logDir;
        this.LogFileIformation = logFileIformation;
        this.LogFileWarning = logFileWarning;
        this.LogFileError = logFileError;
        this.LogFileFatal = logFileFatal;
    }

    public LogTypeFile(string logDir, string logFileName) : this(logDir, logFileName, logFileName, logFileName, logFileName)
    { this.oneFileForAll = true; }

    public List<Action<LoggerConfiguration>> GetConfiguration()
    {
        string outTempl = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level}] {Message}{NewLine}{Exception}";
        List<Action<LoggerConfiguration>> logActions = new List<Action<LoggerConfiguration>>();

        if (this.oneFileForAll)
        {
            logActions.Add(lc => lc.WriteTo.File(Path.Combine(this.LogDirectory, this.LogFileIformation), outputTemplate: outTempl));
        }
        else
        {
            logActions.Add(lc => lc.Filter.ByIncludingOnly(
                                evt => evt.Level == Serilog.Events.LogEventLevel.Information)
                                .WriteTo.File(Path.Combine(this.LogDirectory, this.LogFileIformation), outputTemplate: outTempl));

            logActions.Add(lc => lc.Filter.ByIncludingOnly(
                                  evt => evt.Level == Serilog.Events.LogEventLevel.Warning)
                                  .WriteTo.File(Path.Combine(this.LogDirectory, this.LogFileWarning), outputTemplate: outTempl));

            logActions.Add(lc => lc.Filter.ByIncludingOnly(
                                  evt => evt.Level == Serilog.Events.LogEventLevel.Error)
                                  .WriteTo.File(Path.Combine(this.LogDirectory, this.LogFileError), outputTemplate: outTempl));

            logActions.Add(lc => lc.Filter.ByIncludingOnly(
                                  evt => evt.Level == Serilog.Events.LogEventLevel.Fatal)
                                  .WriteTo.File(Path.Combine(this.LogDirectory, this.LogFileFatal), outputTemplate: outTempl));
        }

        return logActions;
    }
}