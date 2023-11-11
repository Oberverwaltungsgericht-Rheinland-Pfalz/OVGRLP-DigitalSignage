// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.ImportCLI.Logging;

public class LogEntry
{
    #region fields

    private DateTime startTime;
    private List<LogEntry> subEntries;
    private LogEventLevel level;
    private String message;

    #endregion fields

    #region constructor

    public LogEntry(string message, LogEventLevel level)
    {
        this.message = message;
        this.level = level;
        startTime = DateTime.Now;
    }

    #endregion constructor

    #region properties

    public List<LogEntry> SubEntries
    {
        get
        {
            if (subEntries == null)
                subEntries = new List<LogEntry>();

            return subEntries;
        }
    }

    public LogEventLevel Level
    {
        get { return level; }
        set { level = value; }
    }

    public String Message
    {
        get { return message; }
        set { message = value; }
    }

    public DateTime StartTime
    {
        get { return startTime; }
        set { startTime = value; }
    }

    #endregion properties

    #region methods

    public void AddSubEntry(string message, LogEventLevel level)
    {
        SubEntries.Add(new LogEntry(message, level));

        if (Level < level)
            Level = level;
    }

    #endregion methods
}