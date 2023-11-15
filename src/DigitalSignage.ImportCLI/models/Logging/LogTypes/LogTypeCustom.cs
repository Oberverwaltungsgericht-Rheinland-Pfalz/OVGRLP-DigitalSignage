// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Serilog;

namespace DigitalSignage.ImportCLI.Logging.LogTypes;

public class LogTypeCustom : ILogType
{
    private List<Action<LoggerConfiguration>> LogActions = new List<Action<LoggerConfiguration>>();

    public void AddLoggerAction(Action<LoggerConfiguration> logAction)
    {
        LogActions.Add(logAction);
    }

    public List<Action<LoggerConfiguration>> GetConfiguration()
    {
        return LogActions;
    }
}