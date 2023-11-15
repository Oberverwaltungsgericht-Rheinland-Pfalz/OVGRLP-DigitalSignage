// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2

using System.Linq;

namespace DigitalSignage.Infrastructure.Models.EurekaFach;

public class VerfahrenDto
{
    public Int64 Id { get; set; }
    public string Az { get; set; }
    public byte Lfdnr { get; set; }
    public byte Kammer { get; set; }
    public string Sitzungssaal { get; set; }
    public Nullable<Int64> SitzungssaalNr { get; set; }
    public string UhrzeitPlan { get; set; }
    public string UhrzeitAktuell { get; set; }
    public string Status { get; set; }
    public string Oeffentlich { get; set; }
    public string Art { get; set; }
    public string Gegenstand { get; set; }
    public string Bemerkung1 { get; set; }
    public string Bemerkung2 { get; set; }
    public ICollection<string> ParteienAktiv { get; set; }
    public ICollection<string> ProzBevAktiv { get; set; }
    public ICollection<string> ParteienPassiv { get; set; }
    public ICollection<string> ProzBevPassiv { get; set; }
    public ICollection<string> ParteienBeigeladen { get; set; }
    public ICollection<string> ProzBevBeigeladen { get; set; }
    public ICollection<string> ParteienZeugen { get; set; }
    public ICollection<string> ParteienSv { get; set; }
    public string ParteienAktivKurz { get; set; }
    public string ParteienPassivKurz { get; set; }
    public int StammdatenId { get; set; }
    public string Gericht { get; set; }
    public string Datum { get; set; }
    public ICollection<string> Besetzung { get; set; }
    public ICollection<string> ParteienBeteiligt { get; set; }
    public virtual ICollection<Objekte> Objekte { get; set; }

    public Verfahren GetVerfahrenFromDto()
    {
        return new Verfahren()
        {
            VerfahrensId = this.Id,
            StammdatenId = this.StammdatenId,
            Lfdnr = this.Lfdnr,
            Kammer = this.Kammer,
            Sitzungssaal = this.Sitzungssaal,
            SitzungssaalNr = this.SitzungssaalNr,
            UhrzeitPlan = this.UhrzeitPlan,
            UhrzeitAktuell = this.UhrzeitAktuell,
            Status = this.Status,
            Oeffentlich = this.Oeffentlich,
            Az = this.Az,
            Gegenstand = this.Gegenstand,
            Bemerkung1 = this.Bemerkung1,
            Bemerkung2 = this.Bemerkung2,
            Art = this.Art
        };
    }

    public VerfahrenDto() { }
    public VerfahrenDto (Verfahren verfahren)
    {
        string aktivparteiKurz = "";
        if (verfahren.ParteienAktiv.Count > 0)
        {
            aktivparteiKurz = verfahren.ParteienAktiv.First().Partei;
            if (verfahren.ParteienAktiv.Count > 1)
            {
                aktivparteiKurz += " u.a.";
            }
        }

        string passivparteiKurz = "";
        if (verfahren.ParteienPassiv.Count > 0)
        {
            passivparteiKurz = verfahren.ParteienPassiv.First().Partei;
            if (verfahren.ParteienPassiv.Count > 1)
            {
                passivparteiKurz += " u.a.";
            }
        }

        List<string> besetzung = new List<string>();
        foreach (var richter in verfahren.Besetzung)
        {
            besetzung.Add(richter.Richter);
        }

        List<string> aktivParteien = new List<string>();
        foreach (var ap in verfahren.ParteienAktiv)
        {
            aktivParteien.Add(ap.Partei);
        }

        List<string> passivParteien = new List<string>();
        foreach (var pp in verfahren.ParteienPassiv)
        {
            passivParteien.Add(pp.Partei);
        }

        List<string> aktivProzBev = new List<string>();
        foreach (var prozBev in verfahren.ProzBevAktiv)
        {
            aktivProzBev.Add(prozBev.PB);
        }

        List<string> passivProzBev = new List<string>();
        foreach (var prozBev in verfahren.ProzBevPassiv)
        {
            passivProzBev.Add(prozBev.PB);
        }

        List<string> parteienBeigeladen = new List<string>();
        foreach (var beigeladen in verfahren.ParteienBeigeladen)
        {
            parteienBeigeladen.Add(beigeladen.Partei);
        }

        List<string> prozBevBeigeladen = new List<string>();
        foreach (var prozBev in verfahren.ProzBevBeigeladen)
        {
            prozBevBeigeladen.Add(prozBev.PB);
        }

        List<string> parteienZeugen = new List<string>();
        foreach (var partei in verfahren.ParteienZeugen)
        {
            parteienZeugen.Add(partei.Partei);
        }

        List<string> parteienSv = new List<string>();
        foreach (var partei in verfahren.ParteienSV)
        {
            parteienSv.Add(partei.Partei);
        }

        List<string> parteienBeteiligt = new List<string>();
        foreach (var partei in verfahren.ParteienBeteiligt)
        {
            parteienBeteiligt.Add(partei.Partei);
        }

        this.Objekte = verfahren.Objekte.ToList();

        Id = verfahren.VerfahrensId;
        StammdatenId = verfahren.StammdatenId;
        Lfdnr = verfahren.Lfdnr;
        Az = verfahren.Az;
        Kammer = verfahren.Kammer;
        Sitzungssaal = verfahren.Sitzungssaal;
        SitzungssaalNr = verfahren.SitzungssaalNr;
        UhrzeitPlan = verfahren.UhrzeitPlan;
        UhrzeitAktuell = verfahren.UhrzeitAktuell;
        Status = verfahren.Status;
        Oeffentlich = verfahren.Oeffentlich;
        Gegenstand = verfahren.Gegenstand;
        Bemerkung1 = verfahren.Bemerkung1;
        Bemerkung2 = verfahren.Bemerkung2;
        ParteienAktivKurz = aktivparteiKurz;
        ParteienAktiv = aktivParteien;
        ProzBevAktiv = aktivProzBev;
        ParteienPassivKurz = passivparteiKurz;
        ParteienPassiv = passivParteien;
        ProzBevPassiv = passivProzBev;
        ParteienBeigeladen = parteienBeigeladen;
        ProzBevBeigeladen = prozBevBeigeladen;
            ParteienZeugen = parteienZeugen;
            ParteienSv = parteienSv;
        ParteienBeteiligt = parteienBeteiligt;
            Art = verfahren.Art;
        Gericht = verfahren.Stammdaten.Gerichtsname;
        Datum = verfahren.Stammdaten.Datum;
        Besetzung = besetzung;
    }
}