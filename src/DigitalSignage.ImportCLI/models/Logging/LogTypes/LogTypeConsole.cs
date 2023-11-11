// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Serilog;

namespace DigitalSignage.ImportCLI.Logging.LogTypes;

public class LogTypeConsole : ILogType
{
    public List<Action<LoggerConfiguration>> GetConfiguration()
    {
        Action<LoggerConfiguration> logAction;

        logAction = lc => lc.WriteTo.Console();

        return new List<Action<LoggerConfiguration>> { logAction };
    }
}