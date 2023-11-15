// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Serilog;
using System.Collections.ObjectModel;
using System.Data;

namespace DigitalSignage.ImportCLI.Logging.LogTypes;

public class LogTypeMSSqlServer : ILogType
{
    public string DBServer { get; set; }
    public string Database { get; set; }
    public string DataTable { get; set; }

    private string _dBCredentials;

    public string DBCredentials
    {
        get
        {
            if (string.IsNullOrEmpty(_dBCredentials))
                _dBCredentials = "Trusted_Connection=True;";
            return _dBCredentials;
        }
        set => _dBCredentials = value;
    }

    public LogTypeMSSqlServer(string dataTable,
                       string dbServer = @"SERVER\Database",
                       string database = "APPDATA",
                       string dbUser = "",
                       string dbPassword = "")
    {
        DataTable = dataTable;
        DBServer = dbServer;
        Database = database;
        if (!string.IsNullOrEmpty(dbUser) || !string.IsNullOrEmpty(dbPassword))
            DBCredentials = string.Format("uid={0};password={1};", dbUser, dbPassword);
    }

    public List<Action<LoggerConfiguration>> GetConfiguration()
    {
        Action<LoggerConfiguration> logAction;

        var colOptions = new Serilog.Sinks.MSSqlServer.ColumnOptions();
        colOptions.AdditionalDataColumns = new Collection<DataColumn> {
    new DataColumn() { AllowDBNull = true, ColumnName = "Metadaten", DataType = typeof(string) },
  };

        logAction = lc => lc.WriteTo.MSSqlServer(string.Format("Server={0};Database={1};{2}", DBServer, Database, DBCredentials),
                                                 DataTable,
                                                 columnOptions: colOptions,
                                                 autoCreateSqlTable: true,
                                                 period: TimeSpan.Zero);

        return new List<Action<LoggerConfiguration>> { logAction };
    }
}