// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DigitalSignage.ImportCLI.Service;

public class DBService
{
    public string NameOrConnectionString { get; set; }
    private readonly DigitalSignageDbContext _context = null;
    public List<string> Warnings = new List<string>();
    
    public DBService(string nameOrConnectionString)
    {
        this.NameOrConnectionString = nameOrConnectionString;
        var contextOpitons = new DbContextOptionsBuilder<DigitalSignageDbContext>().UseSqlServer(this.NameOrConnectionString).Options;
        _context = new DigitalSignageDbContext(contextOpitons);

    }

    public DBService(DigitalSignageDbContext context)
    {
        _context = context;
    }

    public void DeleteAll()
    {
        foreach(Stammdaten st in _context.Stammdaten.ToArray())
          _context.Stammdaten.Remove(st);

        _context.SaveChanges();
    }

    public void AddData(Terminsaushang data)
    {
        //Kopfdaten
        var st = new DigitalSignage.Infrastructure.Models.EurekaFach.Stammdaten();
        st.Datum = data.Stammdaten.Datum;
        st.Gerichtsname = data.Stammdaten.Gerichtsname.TrimEnd();
        _context.Stammdaten.Add(st);

        //alle Verfahren aufnehmen
        foreach (TerminsaushangVerfahren verf in data.Terminiert)
        {
            var v = new Verfahren();

            // Aufnahme der Verfahrensdaten
            v.StammdatenId = st.StammdatenId;
            AddVerfahrensdaten(v, verf);
            _context.Verfahren.Add(v);
        }

        _context.SaveChanges();
    }

    public void UpdateData(Terminsaushang data)
    {
        // StammdatenID ahand Gericht und Datum finden
        var st = _context.Stammdaten.Where(s => s.Datum == data.Stammdaten.Datum && s.Gerichtsname == data.Stammdaten.Gerichtsname).ToList();
        if (null == st || st.Count == 0)
            throw new Exception(string.Format("Stammdaten '{0}' vom {1} konnten nicht gefunden werden", data.Stammdaten.Gerichtsname, data.Stammdaten.Datum));
        if (st.Count > 1)
            throw new Exception(string.Format("Zu den Stammdaten '{0}' vom {1} konnte kein eindeutiger Datensatz ermittelt werden", data.Stammdaten.Gerichtsname, data.Stammdaten.Datum));
        int StammdatenID = st[0].StammdatenId;

        foreach (TerminsaushangVerfahren verf in data.Terminiert)
        {
            var ver = _context.Verfahren.Where(v => v.StammdatenId == StammdatenID && v.Az == verf.Az).ToList();
            if (null == ver || ver.Count == 0)
            {
                var v = new Verfahren();
                v.StammdatenId = StammdatenID;
                AddVerfahrensdaten(v, verf);
                _context.Verfahren.Add(v);
            }
            else
            {
                if (ver.Count > 1)
                    throw new Exception(string.Format("Zu dem Verfahren '{0}' konnte kein eindeutiger Datensatz ermittelt werden", verf.Az));
                Int64 VerfahrenID = ver[0].VerfahrensId;
                DeleteVerfahrensParteien(_context, ver[0]);
                AddVerfahrensdaten(ver[0], verf);
            }
        }

        _context.SaveChanges();
    }

    private List<ParteienAktiv> DetermineAktivParteien(TerminsaushangVerfahren verf)
    {
        var aktivPartei = new List<ParteienAktiv>();
        foreach (string par in (verf.AktivPartei?.Parteien ?? Enumerable.Empty<string>()))
            aktivPartei.Add(new ParteienAktiv { Partei = par.TrimEnd() });
        
        return aktivPartei;
    }

    private List<ProzBevAktiv> DetermineAktivProzBev(TerminsaushangVerfahren verf)
    {
        var aktivProzBev = new List<ProzBevAktiv>();
        foreach (string pro in (verf.AktivPartei?.ProzBev?.PB ?? Enumerable.Empty<string>()))
            aktivProzBev.Add(new ProzBevAktiv { PB = pro.TrimEnd() });

        return aktivProzBev;
    }

    private List<ParteienPassiv> DeterminePassivParteien(TerminsaushangVerfahren verf)
    {
        var passivPartei = new List<ParteienPassiv>();
        foreach (string par in verf.PassivPartei?.Parteien ?? Enumerable.Empty<string>())
            passivPartei.Add(new ParteienPassiv { Partei = par.TrimEnd() });

        return passivPartei;
    }

    private List<ProzBevPassiv> DeterminePassivProzBev(TerminsaushangVerfahren verf)
    {
        var passivProzBev = new List<ProzBevPassiv>();
        foreach (string pro in verf.PassivPartei?.ProzBev?.PB ?? Enumerable.Empty<string>())
            passivProzBev.Add(new ProzBevPassiv { PB = pro.TrimEnd() });

        return passivProzBev;
    }

    private List<ParteienBeigeladen> DetermineBeigeladeneParteien(TerminsaushangVerfahren verf)
    {
        var beigeladenPartei = new List<ParteienBeigeladen>();
        foreach (string par in verf.Beigeladen?.Parteien ?? Enumerable.Empty<string>())
            beigeladenPartei.Add(new ParteienBeigeladen { Partei = par.TrimEnd() });

        return beigeladenPartei;
    }

    private List<ProzBevBeigeladen> DetermineBeigeladeneProzBev(TerminsaushangVerfahren verf)
    {
        var beigeladenProzBev = new List<ProzBevBeigeladen>();
        foreach (string pro in verf.Beigeladen?.ProzBev ?? Enumerable.Empty<string>())
            beigeladenProzBev.Add(new ProzBevBeigeladen { PB = pro.TrimEnd() });

        return beigeladenProzBev;
    }

    private List<ParteienSV> DetermineSachverstaendige(TerminsaushangVerfahren verf)
    {
        return verf.SV
            ?.Parteien
            ?.Select(par => 
                new ParteienSV { Partei = par.TrimEnd() }
            )
            .ToList() 
            ?? new List<ParteienSV>();
    }

    private List<ParteienZeugen> DetermineZeugen(TerminsaushangVerfahren verf)
    {
        var zPartei = new List<ParteienZeugen>();
        foreach (var zeu in verf.Zeugen?.Parteien ?? Enumerable.Empty<string>())
            zPartei.Add(new ParteienZeugen { Partei = zeu });

        return zPartei;
    }

    private List<Besetzung> DetermineBesetzung(TerminsaushangVerfahren verf)
    {
        var besetzung = new List<Besetzung>();
        foreach (string bes in verf.Besetzung ?? Enumerable.Empty<string>())
            besetzung.Add(new Besetzung { Richter = bes.TrimEnd() });

        return besetzung;
    }

    private List<ParteienBeteiligt> DetermineBeteiligte(TerminsaushangVerfahren verf)
    {
        var beteiligt = new List<ParteienBeteiligt>();
        foreach (var bet in verf.Beteiligt?.Parteien ?? Enumerable.Empty<string>())
        {
            if (bet.ToLower().TrimEnd() == "beteiligte:" || bet.ToLower().TrimEnd() == "beteiligt:")
                continue;
            beteiligt.Add(new ParteienBeteiligt { Partei = bet.TrimEnd() });
        }
        
        // die Prozessbevollmächtigten werden von Eureka-Export alle mit dem Präfix "Proz.-Bev.: " übergeben, daher werden diese erstmal als Beteiligte mit angezeigt #145
        foreach (var prozBev in verf.Beteiligt?.ProzBev ?? Enumerable.Empty<string>())
            beteiligt.Add(new ParteienBeteiligt { Partei = prozBev.TrimEnd() });   

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

        // Passivparteien sollen grundsätzlich eingelesen werden aufgrund eines Fehlers im Eureka-Saalanzeigenexport, wo die Beteiligten
        // zusätzlich nochmal als Passivpartei eingelesen werden (Mantis #4493), sollen die PP erstmal bei Personalvertretungssachen
        // ausgenommen werden! - Lt. Frau Baum gibt es dort keine PP!
        bool passivParteienEinlesen = true;
        if (verfahren.Az.Contains(".OVG") && (verfahren.Kammer == 4 || verfahren.Kammer == 5))
            passivParteienEinlesen = false;

        //Besetzung
        verfahren.Besetzung = DetermineBesetzung(verf);

        //Aktivpartei und Prozessbevollmächtigte Aktivpartei
        verfahren.ParteienAktiv = DetermineAktivParteien(verf);
        verfahren.ProzBevAktiv = DetermineAktivProzBev(verf);

        //Passivpartei und Prozessbevollmächtigte Passivpartei
        if (passivParteienEinlesen)
        {
            verfahren.ParteienPassiv = DeterminePassivParteien(verf);
            verfahren.ProzBevPassiv = DeterminePassivProzBev(verf);
        }

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

    private void DeleteVerfahrensParteien(DigitalSignageDbContext contextProvider, Verfahren verfahren)
    {
        var aktivPartei = contextProvider.ParteienAktiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in aktivPartei)
            contextProvider.ParteienAktiv.Remove(p);

        var aktivProzBev = contextProvider.ProzBevAktiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in aktivProzBev)
            contextProvider.ProzBevAktiv.Remove(p);

        var passivPartei = contextProvider.ParteienPassiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in passivPartei)
            contextProvider.ParteienPassiv.Remove(p);

        var passivProzBev = contextProvider.ProzBevPassiv.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in passivProzBev)
            contextProvider.ProzBevPassiv.Remove(p);

        var beigeladenPartei = contextProvider.ParteienBeigeladen.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in beigeladenPartei)
            contextProvider.ParteienBeigeladen.Remove(p);

        var beigeladenProzBev = contextProvider.ProzBevBeigeladen.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in beigeladenProzBev)
            contextProvider.ProzBevBeigeladen.Remove(p);

        var svPartei = contextProvider.ParteienSV.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in svPartei)
            contextProvider.ParteienSV.Remove(p);

        var zPartei = contextProvider.ParteienZeugen.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in zPartei)
            contextProvider.ParteienZeugen.Remove(p);

        var besetzung = contextProvider.Besetzung.Where(p => p.VerfahrensId == verfahren.VerfahrensId).ToList();
        foreach (var p in besetzung)
            contextProvider.Besetzung.Remove(p);
    }
}
