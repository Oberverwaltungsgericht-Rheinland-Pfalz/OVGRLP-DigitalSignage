// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.Text;

namespace DigitalSignage.ImportCLI.Logging;

public class LogEntryParsingService
{
    public LogEntryParsingService()
    {
    }

    public string ParseToStringMessage(LogEntry logEntry, Int32 indent = 0)
    {
        StringBuilder sb = new StringBuilder();
        string ln = logEntry.Level.ToString();
        if (ln.Length > 4)
            ln = ln.Substring(0, 4);

        sb.AppendLine(String.Format("{0:G} | {1} | {2}{3}", logEntry.StartTime, ln.ToUpper().PadRight(6), new String(' ', indent), logEntry.Message));

        foreach (LogEntry subEntry in logEntry.SubEntries)
            sb.Append(ParseToStringMessage(subEntry, indent + 2));

        return sb.ToString();
    }
}