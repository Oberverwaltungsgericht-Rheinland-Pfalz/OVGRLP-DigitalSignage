using Breeze.ContextProvider.EF6;
using DigitalSignage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalSignage.ImportCLI
{
  public class CLIActions
  {
    public List<string> InputFiles;
    public bool ClearDatabase;
    public string NameOrConnectionString;

    public CLIActions()
    {
      //Defaults
      this.InputFiles = new List<string>();
      this.ClearDatabase = false;
    }

    private void ValidateActions()
    {
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
      ValidateActions();

      //ggf. zuerst die Datenbank löschen
      if (this.ClearDatabase)
      {
        var db = new Service.DBService(this.NameOrConnectionString);
        db.DeleteAll();
      }

      //XML Dateien eilesen
      if (this.InputFiles != null || this.InputFiles.Count > 0)
      {
        foreach (string inputFile in this.InputFiles)
        {
          if (!string.IsNullOrEmpty(inputFile) && System.IO.File.Exists(inputFile))
          {
            //TerminsaushangTerminiertVerfahren[] verfahren = null;
            //TerminsaushangStammdaten header = null;

            Terminsaushang data = Service.XMLHelper.DeserializeFromXml<Terminsaushang>(inputFile);

            if (null != data)
            {
              var db = new Service.DBService(this.NameOrConnectionString);
              db.AddData(data);
            }
          }
        }
      }

      //
    }
  }
}