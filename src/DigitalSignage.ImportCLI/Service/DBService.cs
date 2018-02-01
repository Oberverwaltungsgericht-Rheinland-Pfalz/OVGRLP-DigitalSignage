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
        db.SaveChanges();
      }
    }

    public void AddData(Terminsaushang data)
    {
      //EFContextProvider<DigitalSignageDbContext> contextProvider = new EFContextProvider<DigitalSignageDbContext>();
      var contextProvider = new EfContextProvider(this.NameOrConnectionString);

      //Kopfdaten
      var st = new DigitalSignage.Infrastructure.Models.EurekaFach.Stammdaten();
      st.Datum = data.Stammdaten.Datum;
      st.Gerichtsname = data.Stammdaten.Gerichtsname;
      contextProvider.Context.Stammdaten.Add(st);

      //alle Verfahren
      foreach (TerminsaushangVerfahren verf in data.Terminiert)
      {
        // Verfahrens-Kopfdaten
        var v = new DigitalSignage.Infrastructure.Models.EurekaFach.Verfahren();
        v.Lfdnr = Convert.ToByte(verf.Lfdnr);
        v.Kammer = Convert.ToByte(verf.Kammer);
        v.Sitzungssaal = verf.Sitzungssaal;
        v.UhrzeitAktuell = verf.Uhrzeit;
        v.UhrzeitPlan = verf.Uhrzeit;
        v.Status = verf.Status;
        v.Oeffentlich = verf.Oeffentlich;
        v.Art = verf.Art;
        v.Az = verf.Az;
        v.Gegenstand = verf.Gegenstand;
        v.Bemerkung1 = verf.Bemerkung1;
        v.Bemerkung2 = verf.Bemerkung2;
        v.StammdatenId = st.StammdatenId;  //!\TODO: prüfen ???

        //Besetzung
        v.Besetzung = DetermineBesetzung(verf);

        //Aktivpartei und Prozessbevollmächtigte Aktivpartei
        v.ParteienAktiv = DetermineAktivParteien(verf); ;
        v.ProzBevAktiv = DetermineAktivProzBev(verf); ;

        //Passivpartei und Prozessbevollmächtigte Passivpartei
        v.ParteienPassiv = DeterminePassivParteien(verf);
        v.ProzBevPassiv = DeterminePassivProzBev(verf);

        // Beigeladene
        v.ParteienBeigeladen = DetermineBeigeladeneParteien(verf);
        v.ProzBevBeigeladen = DetermineBeigeladeneProzBev(verf);

        // Sachverständige
        v.ParteienSV = DetermineSachverstaendige(verf);

        // Zeugen
        v.ParteienZeugen = DetermineZeugen(verf);

        // Aufnahme der Verfahrensdaten
        contextProvider.Context.Verfahren.Add(v);
      }

      contextProvider.Context.SaveChanges();
    }

    private List<ParteienAktiv> DetermineAktivParteien(TerminsaushangVerfahren verf)
    {
      var aktivPartei = new List<ParteienAktiv>();
      if (null != verf.AktivPartei)
      {
        if (null != verf.AktivPartei.Parteien)
        {
          foreach (string par in verf.AktivPartei.Parteien)
          {
            aktivPartei.Add(new ParteienAktiv { Partei = par });
          }
        }
      }
      return aktivPartei;
    }

    private List<ProzBevAktiv> DetermineAktivProzBev(TerminsaushangVerfahren verf)
    {
      var aktivProzBev = new List<ProzBevAktiv>();
      if (null != verf.AktivPartei)
      {
        if (null != verf.AktivPartei.ProzBev && null != verf.AktivPartei.ProzBev.PB)
        {
          foreach (string pro in verf.AktivPartei.ProzBev.PB)
          {
            aktivProzBev.Add(new ProzBevAktiv { PB = pro });
          }
        }
      }
      return aktivProzBev;
    }

    private List<ParteienPassiv> DeterminePassivParteien(TerminsaushangVerfahren verf)
    {
      var passivPartei = new List<ParteienPassiv>();
      if (null != verf.PassivPartei)
      {
        if (null != verf.PassivPartei.Parteien)
        {
          foreach (string par in verf.PassivPartei.Parteien)
          {
            passivPartei.Add(new ParteienPassiv { Partei = par });
          }
        }
      }
      return passivPartei;
    }

    private List<ProzBevPassiv> DeterminePassivProzBev(TerminsaushangVerfahren verf)
    {
      var passivProzBev = new List<ProzBevPassiv>();
      if (null != verf.PassivPartei)
      {
        if (null != verf.PassivPartei.ProzBev && null != verf.PassivPartei.ProzBev.PB)
        {
          foreach (string pro in verf.PassivPartei.ProzBev.PB)
          {
            passivProzBev.Add(new ProzBevPassiv { PB = pro });
          }
        }
      }
      return passivProzBev;
    }

    private List<ParteienBeigeladen> DetermineBeigeladeneParteien(TerminsaushangVerfahren verf)
    {
      var beigeladenPartei = new List<ParteienBeigeladen>();
      if (null != verf.Beigeladen)
      {
        if (null != verf.Beigeladen.Parteien)
        {
          foreach (string par in verf.Beigeladen.Parteien)
          {
            beigeladenPartei.Add(new ParteienBeigeladen { Partei = par });
          }
        }
      }
      return beigeladenPartei;
    }

    private List<ProzBevBeigeladen> DetermineBeigeladeneProzBev(TerminsaushangVerfahren verf)
    {
      var beigeladenProzBev = new List<ProzBevBeigeladen>();
      if (null != verf.Beigeladen)
      {
        if (null != verf.Beigeladen.ProzBev)
        {
          foreach (string pro in verf.Beigeladen.ProzBev)
          {
            beigeladenProzBev.Add(new ProzBevBeigeladen { PB = pro });
          }
        }
      }
      return beigeladenProzBev;
    }

    private List<ParteienSV> DetermineSachverstaendige(TerminsaushangVerfahren verf)
    {
      var svPartei = new List<ParteienSV>();
      if (null != verf.SV)
      {
        foreach (string par in verf.SV.Parteien)
        {
          svPartei.Add(new ParteienSV { Partei = par });
        }
      }

      return svPartei;
    }

    private List<ParteienZeugen> DetermineZeugen(TerminsaushangVerfahren verf)
    {
      var zPartei = new List<ParteienZeugen>();
      if (null != verf.Zeugen)
      {
        foreach (var zeu in verf.Zeugen.Parteien)
        {
          zPartei.Add(new ParteienZeugen { Partei = zeu });
        }
      }

      return zPartei;
    }

    private List<Besetzung> DetermineBesetzung(TerminsaushangVerfahren verf)
    {
      var besetzung = new List<Besetzung>();
      if (null != verf.Besetzung)
      {
        foreach (string bes in verf.Besetzung)
        {
          besetzung.Add(new Besetzung { Richter = bes });
        }
      }
      return besetzung;
    }
  }

  public class EfContextProvider : EFContextProvider<DigitalSignageDbContext>
  {
    private string NameOrConnectionString;

    public EfContextProvider(string nameOrConnectionString) : base()
    {
      this.NameOrConnectionString = nameOrConnectionString;
    }

    protected override DigitalSignageDbContext CreateContext()
    {
      return new DigitalSignageDbContext(NameOrConnectionString);
    }
  }
}