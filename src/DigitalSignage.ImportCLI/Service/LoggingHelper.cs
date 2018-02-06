using OvgRlp.Libs.Logging;
using OvgRlp.Libs.Logging.LogTypes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.ImportCLI.Service
{
  public class LoggingHelper
  {
    private static LogEventLevel LogLevel = LogEventLevel.Information;
    private static Serilog.Core.Logger LoggerInstance = null;

    public static void InitLogging(string logDir)
    {
      // Textfile logging
      if (!String.IsNullOrWhiteSpace(logDir))
      {
        LogTypeFile logType;
        if (!Directory.Exists(logDir) || !File.GetAttributes(logDir).HasFlag(FileAttributes.Directory))
        {
          logType = new LogTypeFile(Path.GetDirectoryName(logDir), Path.GetFileName(logDir));
        }
        else
        {
          logType = new LogTypeFile(logDir);
        }
        Logger.LoggingTypes.Add(logType);
        LoggerInstance = Logger.CreateLogger();
      }
    }

    //! Ausnahmefehler protokollieren
    public static void Trace(Exception ex)
    {
      Exception exx = ex;
      LogLevel = LogEventLevel.Error;
      do
      {
        Trace(exx.Message, exx.StackTrace);
        exx = exx.InnerException;
      } while (null != exx);
    }

    //! Programmausgaben protokollieren
    public static void Trace(params String[] args)
    {
      foreach (var text in args)
      {
        System.Diagnostics.Trace.WriteLine(text);
        if (null != LoggerInstance)
        {
          LoggerInstance.Write(LogLevel.toSerilogEventLevel(), text);
        }
      }
    }
  }
}