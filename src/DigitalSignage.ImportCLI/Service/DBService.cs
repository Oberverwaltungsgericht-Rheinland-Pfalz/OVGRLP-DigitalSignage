using Breeze.ContextProvider.EF6;
using DigitalSignage.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DigitalSignage.Infrastructure.Models.EurekaFach;

namespace DigitalSignage.ImportCLI.Service
{
  public class DBService
  {
    private readonly EFContextProvider<DigitalSignageDbContext> contextProvider = new EFContextProvider<DigitalSignageDbContext>();
    public string NameOrConnectionString;

    public DBService(string nameOrConnectionString)
    {
      this.NameOrConnectionString = nameOrConnectionString;
    }

    public void DeleteAll()
    {
      using (var db = new DigitalSignageDbContext(this.NameOrConnectionString))
      {
        foreach (Stammdaten st in db.Stammdaten.ToArray())
        {
          db.Stammdaten.Remove(st);
        }
      }
    }
  }
}