// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Serilog;

namespace DigitalSignage.ImportCLI.Logging
{
    public interface ILogType
    {
        List<Action<LoggerConfiguration>> GetConfiguration();
    }
}