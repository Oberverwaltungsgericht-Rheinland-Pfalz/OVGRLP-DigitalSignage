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
        if (this.InputFiles == null || this.InputFiles.Count == 0)
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

      //ggf. zuerst die Datenbank löschen
      if (this.ClearDatabase)
      {
        Service.LoggingHelper.Trace("Daten werden aus der Datebank gelöscht");
        var db = new Service.DBService(this.NameOrConnectionString);
        db.DeleteAll();
        Service.LoggingHelper.Trace("=> erfolgreich");
      }

      //XML Dateien eilesen
      if (this.InputFiles != null || this.InputFiles.Count > 0)
      {
        foreach (string inputFile in this.InputFiles)
        {
          if (!string.IsNullOrEmpty(inputFile) && System.IO.File.Exists(inputFile))
          {
            Service.LoggingHelper.Trace("Verarbeitung: " + inputFile);
            Terminsaushang data = Service.XMLHelper.DeserializeFromXml<Terminsaushang>(inputFile);
            if (null != data)
            {
              var db = new Service.DBService(this.NameOrConnectionString);
              db.AddData(data);
              Service.LoggingHelper.Trace("=> erfolgreich");
            }
          }
        }
      }
      Service.LoggingHelper.Trace("Programmende... ");
    }
  }
}