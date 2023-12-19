// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2

global using System.Collections.Generic;
global using System.IO;
using DigitalSignage.Data;
using DigitalSignage.ImportCLI.Logging;
using DigitalSignage.ImportCLI.Service;
using System.Linq;

namespace DigitalSignage.ImportCLI;

public class CLIActions
{
    public List<string> InputFiles;
    public List<string> UpdateFiles;
    public bool ClearDatabase;
    public bool WritingInformationToUser;
    public string NameOrConnectionString;
    public string LogFile;

    public CLIActions()
    {
        //Defaults
        this.InputFiles = new List<string>();
        this.UpdateFiles = new List<string>();
        this.ClearDatabase = false;
        this.WritingInformationToUser = false;
    }

    public void ValidateActions()
    {
        if (WritingInformationToUser)
            return;
        if (string.IsNullOrEmpty(this.NameOrConnectionString))
        {
            throw new ArgumentException("Es wurde kein ConnectionString angegeben! (-con)");
        }

        if (!this.ClearDatabase)
        {
            if ((this.InputFiles == null || this.InputFiles.Count == 0) && (this.UpdateFiles == null || this.UpdateFiles.Count == 0))
                throw new ArgumentException("Es wurden keine XML-Einlesedateien angegeben!");
        }
    }

    public void ExecuteActions(DigitalSignageDbContext context = null)
    {
        if (WritingInformationToUser)
            return;
        if (LogFile != "")
            Service.LoggingHelper.InitLogging(LogFile);

        ValidateActions();

        Service.LoggingHelper.Trace("ausgewahlte Datenbank: " + this.NameOrConnectionString);

        DBService db;
        if (context != null)
            db = new DBService(context);
        else
            db = new DBService(this.NameOrConnectionString);

        //ggf. zuerst die Datenbank löschen
        if (this.ClearDatabase)
        {
            Service.LoggingHelper.Trace("Daten werden aus der Datebank gelöscht");
            db.DeleteAll();
            Service.LoggingHelper.Trace("=> erfolgreich");
        }

        //XML Dateien neu einlesen
        if (this.InputFiles != null || this.InputFiles.Count > 0)
        {
            foreach (string inputFile in this.InputFiles)
            {
                if (!string.IsNullOrEmpty(inputFile))
                {
                    Service.LoggingHelper.Trace("Daten werden hinzugefügt: " + inputFile);
                    try
                    {
                        var fp = Path.GetFullPath(inputFile);
                        if (!File.Exists(inputFile))
                            throw new IOException("=> Die Datei konnte nicht geöffnet werden.");

                        Terminsaushang data = XMLHelper.DeserializeFromXml<Terminsaushang>(inputFile);
                        if (null != data)
                        {
                            db.AddData(data);
                            AddDbWarningsToLogging(db);
                            Service.LoggingHelper.Trace("=> erfolgreich");
                        }
                    }
                    catch (IOException ex)
                    { Service.LoggingHelper.Trace(ex, false, LogEventLevel.Warning); }
                    catch (System.Data.Entity.Validation.DbEntityValidationException ex)
                    {
                        foreach (var eve in ex.EntityValidationErrors)
                        {
                            Service.LoggingHelper.Trace(
                              string.Format("Fehler bei Validierung der Tabelle \"{0}\" (state \"{1}\"):", eve.Entry.Entity.GetType().Name, eve.Entry.State),
                              LogEventLevel.Error);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Service.LoggingHelper.Trace(
                                string.Format("- Feld \"{0}\", Error: \"{1}\"", ve.PropertyName, ve.ErrorMessage),
                                LogEventLevel.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    { Service.LoggingHelper.Trace(ex); }
                }
            }
        }

        //XML Dateien zum Update
        foreach (string updateFile in (this.UpdateFiles ?? Enumerable.Empty<string>()))
        {
            if (string.IsNullOrEmpty(updateFile)) 
                continue;
                
            Service.LoggingHelper.Trace("Datenupdate: " + updateFile);
            try
            {
                if (!File.Exists(updateFile))
                    throw new IOException("=> Die Datei konnte nicht geöffnet werden.");

                Terminsaushang data = XMLHelper.DeserializeFromXml<Terminsaushang>(updateFile);

                if (data == null) 
                    continue;

                db.UpdateData(data);
                AddDbWarningsToLogging(db);
                Service.LoggingHelper.Trace("=> erfolgreich");
            }
            catch (IOException ex)
            { Service.LoggingHelper.Trace(ex, false, LogEventLevel.Warning); }
            catch (Exception ex)
            { Service.LoggingHelper.Trace(ex); }
    }

        Service.LoggingHelper.Trace("Programmende... ");
        if (LogFile != "")
            Service.LoggingHelper.EndLoggingBlock();
    }

    public void AddDbWarningsToLogging(Service.DBService dBService)
    {
        if (null != dBService.Warnings && dBService.Warnings.Count > 0)
        {
            foreach (string warning in dBService.Warnings)
            {
                Service.LoggingHelper.Trace(warning, LogEventLevel.Warning);
            }
            dBService.Warnings.Clear();
        }
    }
}