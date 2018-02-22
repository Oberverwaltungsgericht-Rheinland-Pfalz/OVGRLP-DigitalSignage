using System;
using System.Collections.Generic;

namespace DigitalSignage.ImportCLI
{
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
      if (String.IsNullOrEmpty(this.NameOrConnectionString))
      {
        throw new ArgumentException("Es wurde kein ConnectionString angegeben! (-con)");
      }

      if (!this.ClearDatabase)
      {
        if ((this.InputFiles == null || this.InputFiles.Count == 0) && (this.UpdateFiles == null || this.UpdateFiles.Count == 0))
          throw new ArgumentException("Es wurden keine XML-Einlesedateien angegeben!");
      }
    }

    public void ExecuteActions()
    {
      if (WritingInformationToUser)
        return;
      if (LogFile != "")
        Service.LoggingHelper.InitLogging(LogFile);

      ValidateActions();

      Service.LoggingHelper.Trace("ausgewahlte Datenbank: " + this.NameOrConnectionString);
      var db = new Service.DBService(this.NameOrConnectionString);

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
          if (!string.IsNullOrEmpty(inputFile) && System.IO.File.Exists(inputFile))
          {
            Service.LoggingHelper.Trace("Daten werden hinzugefügt: " + inputFile);
            Terminsaushang data = Service.XMLHelper.DeserializeFromXml<Terminsaushang>(inputFile);
            if (null != data)
            {
              db.AddData(data);
              Service.LoggingHelper.Trace("=> erfolgreich");
            }
          }
        }
      }

      //XML Dateien zum Update
      if (this.UpdateFiles != null || this.UpdateFiles.Count > 0)
      {
        foreach (string updateFile in this.UpdateFiles)
        {
          if (!string.IsNullOrEmpty(updateFile) && System.IO.File.Exists(updateFile))
          {
            Service.LoggingHelper.Trace("Datenupdate: " + updateFile);
            Terminsaushang data = Service.XMLHelper.DeserializeFromXml<Terminsaushang>(updateFile);
            if (null != data)
            {
              db.UpdateData(data);
              Service.LoggingHelper.Trace("=> erfolgreich");
            }
          }
        }
      }

      Service.LoggingHelper.Trace("Programmende... ");
    }
  }
}