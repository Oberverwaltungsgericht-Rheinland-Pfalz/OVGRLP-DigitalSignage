// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using OvgRlp.Libs.Logging;
using OvgRlp.Libs.Logging.LogTypes;
using System;
using System.IO;

namespace DigitalSignage.ImportCLI.Service
{
  public class LoggingHelper
  {
    private static bool m_FileLogging = false;
    private static LogEntry m_LogEntry = null;

    //! Logging (hinsichtlich Filelogging)  initialisieren
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
        m_FileLogging = true;
      }
    }

    //! Ausnahmefehler protokollieren
    public static void Trace(Exception ex, bool LogStack = false, LogEventLevel logEventLevel = LogEventLevel.Error)
    {
      Exception exx = ex;
      do
      {
        Trace(exx.Message, logEventLevel);
        if (LogStack)
          Trace(exx.StackTrace, logEventLevel);
        exx = exx.InnerException;
      } while (null != exx);
    }

    //! Programmausgaben protokollieren
    public static void Trace(params String[] args)
    {
      foreach (var text in args)
      {
        Trace(text, LogEventLevel.Information);
      }
    }

    //! Programmausgabe protokollieren
    public static void Trace(String text, LogEventLevel logEventLevel)
    {
      System.Diagnostics.Trace.WriteLine(text);
      if (m_FileLogging)
      {
        if (null == m_LogEntry)
        { m_LogEntry = new LogEntry(text, logEventLevel); }
        else { m_LogEntry.AddSubEntry(text, logEventLevel); }
      }
    }

    //! Logischer Logging Block (hinsichtlich Filelogging) finalisieren
    public static void EndLoggingBlock(string context = "")
    {
      if (null != m_LogEntry)
      {
        Logger.Log(m_LogEntry, context);
      }
    }
  }
}