using Breeze.ContextProvider.EF6;
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalSignage.ImportCLI.Service
{
  public class DBService
  {
    public string NameOrConnectionString;
    public List<string> Warnings = new List<string>();

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
      var contextProvider = new EfContextProvider(this.NameOrConnectionString);

      //Kopfdaten
      var st = new DigitalSignage.Infrastructure.Models.EurekaFach.Stammdaten();
      st.Datum = data.Stammdaten.Datum;
      st.Gerichtsname = data.Stammdaten.Gerichtsname.TrimEnd();
      contextProvider.Context.Stammdaten.Add(st);

      //alle Verfahren aufnehmen
      foreach (TerminsaushangVerfahren verf in data.Terminiert)
      {
        var v = new Verfahren();

        // Aufnahme der Verfahrensdaten
        v.StammdatenId = st.StammdatenId;
        AddVerfahrensdaten(v, verf);
        contextProvider.Context.Verfahren.Add(v);
      }

      contextProvider.Context.SaveChanges();
    }

    public void UpdateData(Terminsaushang data)
    {
      var contextProvider = new EfContextProvider(this.NameOrConnectionString);

      // StammdatenID ahand Gericht und Datum finden
      var st = contextProvider.Context.Stammdaten.Where(s => s.Datum == data.Stammdaten.Datum && s.Gerichtsname == data.Stammdaten.Gerichtsname).ToList();
      if (null == st || st.Count == 0)
        throw new Exception(string.Format("Stammdaten '{0}' vom {1} konnten nicht gefunden werden", data.Stammdaten.Gerichtsname, data.Stammdaten.Datum));
      if (st.Count > 1)
        throw new Exception(string.Format("Zu den Stammdaten '{0}' vom {1} konnte kein eindeutiger Datensatz ermittelt werden", data.Stammdaten.Gerichtsname, data.Stammdaten.Datum));
      int StammdatenID = st[0].StammdatenId;

      foreach (TerminsaushangVerfahren verf in data.Terminiert)
      {
        var ver = contextProvider.Context.Verfahren.Where(v => v.StammdatenId == StammdatenID && v.Az == verf.Az).ToList();
        if (null == ver || ver.Count == 0)
        {
          var v = new Verfahren();
          v.StammdatenId = StammdatenID;
          AddVerfahrensdaten(v, verf);
          contextProvider.Context.Verfahren.Add(v);
        }
        else
        {
          if (ver.Count > 1)
            throw new Exception(string.Format("Zu dem Verfahren '{0}' konnte kein eindeutiger Datensatz ermittelt werden", verf.Az));
          Int64 VerfahrenID = ver[0].VerfahrensId;
          DeleteVerfahrensParteien(contextProvider, ver[0]);
          AddVerfahrensdaten(ver[0], verf);
        }
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
            aktivPartei.Add(new ParteienAktiv { Partei = par.TrimEnd() });
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
            aktivProzBev.Add(new ProzBevAktiv { PB = pro.TrimEnd() });
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
            passivPartei.Add(new ParteienPassiv { Partei = par.TrimEnd() });
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
            passivProzBev.Add(new ProzBevPassiv { PB = pro.TrimEnd() });
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
            beigeladenPartei.Add(new ParteienBeigeladen { Partei = par.TrimEnd() });
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
            beigeladenProzBev.Add(new ProzBevBeigeladen { PB = pro.TrimEnd() });
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
          svPartei.Add(new ParteienSV { Partei = par.TrimEnd() });
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
          besetzung.Add(new Besetzung { Richter = bes.TrimEnd() });
        }
      }
      return besetzung;
    }

    private List<ParteienBeteiligt> DetermineBeteiligte(TerminsaushangVerfahren verf)
    {
      var beteiligt = new List<ParteienBeteiligt>();
      if (null != verf.Beteiligt)
      {
        foreach (var bet in verf.Beteiligt.Parteien)
        {
          if (bet.TrimEnd() == "Beteiligte:")
            continue;
          beteiligt.Add(new ParteienBeteiligt { Partei = bet.TrimEnd() });
        }
      }

      return beteiligt;
    }

    private void AddVerfahrensdaten(Verfahren verfahren, TerminsaushangVerfahren verf)
    {
      // validations
      if (verf.Gegenstand.TrimEnd().Length > 255)
      {
        Warnings.Add(string.Format("Der Gegenstand von Verfahren {0} war mit {1} zeichen zu lang und musste auf 255 Zeichen gekürzt werden", verf.Az.TrimEnd(), verf.Gegenstand.TrimEnd().Length));
        verf.Gegenstand = verf.Gegenstand.TrimEnd().Substring(0, 255);
      }

      // Verfahrens-Kopfdaten
      verfahren.Lfdnr = Convert.ToByte(verf.Lfdnr);
      verfahren.Kammer = Convert.ToByte(verf.Kammer);
      verfahren.Sitzungssaal = verf.Sitzungssaal.TrimEnd();
      verfahren.UhrzeitAktuell = verf.Uhrzeit.TrimEnd();
      verfahren.UhrzeitPlan = verf.Uhrzeit.TrimEnd();
      verfahren.Status = verf.Status.TrimEnd();
      verfahren.Oeffentlich = verf.Oeffentlich.TrimEnd();
      verfahren.Art = verf.Art.TrimEnd();
      verfahren.Az = verf.Az.TrimEnd();
      verfahren.Gegenstand = verf.Gegenstand.TrimEnd();
      verfahren.Bemerkung1 = verf.Bemerkung1.TrimEnd();
      verfahren.Bemerkung2 = verf.Bemerkung2.TrimEnd();

      //Besetzung
      verfahren.Besetzung = DetermineBesetzung(verf);

      //Aktivpartei und Prozessbevollmächtigte Aktivpartei
      verfahren.ParteienAktiv = DetermineAktivParteien(verf); ;
      verfahren.ProzBevAktiv = DetermineAktivProzBev(verf); ;

      //Passivpartei und Prozessbevollmächtigte Passivpartei
      verfahren.ParteienPassiv = DeterminePassivParteien(verf);
      verfahren.ProzBevPassiv = DeterminePassivProzBev(verf);

      // Beigeladene
      verfahren.ParteienBeigeladen = DetermineBeigeladeneParteien(verf);
      verfahren.ProzBevBeigeladen = DetermineBeigeladeneProzBev(verf);

      // Sachverständige
      verfahren.ParteienSV = DetermineSachverstaendige(verf);

      // Zeugen
      verfahren.ParteienZeugen = DetermineZeugen(verf);

      // Beteiligte
      verfahren.ParteienBeteiligt = DetermineBeteiligte(verf);
    }

    private void DeleteVerfahrensParteien(EfContextProvider contextProvider, Verfahren verfahren)
    {
      var aktivPartei = contextProvider.Context.ParteienAktiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in aktivPartei)
        contextProvider.Context.ParteienAktiv.Remove(p);

      var aktivProzBev = contextProvider.Context.ProzBevAktiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in aktivProzBev)
        contextProvider.Context.ProzBevAktiv.Remove(p);

      var passivPartei = contextProvider.Context.ParteienPassiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in passivPartei)
        contextProvider.Context.ParteienPassiv.Remove(p);

      var passivProzBev = contextProvider.Context.ProzBevPassiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in passivProzBev)
        contextProvider.Context.ProzBevPassiv.Remove(p);

      var beigeladenPartei = contextProvider.Context.ParteienBeigeladen.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in beigeladenPartei)
        contextProvider.Context.ParteienBeigeladen.Remove(p);

      var beigeladenProzBev = contextProvider.Context.ProzBevBeigeladen.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in beigeladenProzBev)
        contextProvider.Context.ProzBevBeigeladen.Remove(p);

      var svPartei = contextProvider.Context.ParteienSV.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in svPartei)
        contextProvider.Context.ParteienSV.Remove(p);

      var zPartei = contextProvider.Context.ParteienZeugen.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in zPartei)
        contextProvider.Context.ParteienZeugen.Remove(p);

      var besetzung = contextProvider.Context.Besetzung.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
      foreach (var p in besetzung)
        contextProvider.Context.Besetzung.Remove(p);
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