// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
namespace DigitalSignage.ImportCLI.Logging;

public enum LogEventLevel
{
    Information = 2,
    Warning = 3,
    Error = 4,
    Fatal = 5
}

public static class LogEventLevelExtensions
{
    public static Serilog.Events.LogEventLevel toSerilogEventLevel(this LogEventLevel level)
    {
        switch (level)
        {
            case LogEventLevel.Warning:
                return Serilog.Events.LogEventLevel.Warning;

            case LogEventLevel.Error:
                return Serilog.Events.LogEventLevel.Error;

            case LogEventLevel.Fatal:
                return Serilog.Events.LogEventLevel.Fatal;

            default:
                return Serilog.Events.LogEventLevel.Information;
        }
    }
}