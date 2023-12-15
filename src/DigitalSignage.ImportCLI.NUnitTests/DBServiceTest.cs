// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.Linq;

namespace DigitalSignage.ImportCLI.NUnitTests;

[TestFixture]
public class DBServiceTest : TestBase
{
    [Test, Order(1)]
    public void DatabaseConnectionWithConnectionString()
    {
        using (var db = new Data.DigitalSignageDbContext(dbContextOptions(CONNECTION_STRING_LABOR)))
        {
            Stammdaten[] st = db.Stammdaten.ToArray();
            ClassicAssert.AreEqual(db.Database.GetConnectionString(), CONNECTION_STRING_LABOR);
        }
    }

    [Test, Order(2)]
    public void DatabaseAddExamples()
    {
        CLIActions cliAction = CreateCliActions(true, CONNECTION_STRING_LABOR);
        cliAction.ExecuteActions();

        CheckExampleValues();
    }

    [Test, Order(3)]
    public void DatabaseUpdateExamples()
    {
        CLIActions cliAction = CreateCliActions(false, CONNECTION_STRING_LABOR, new string[] { EXAMPLE_UPDATE_XML }, true);
        cliAction.ExecuteActions();

        CheckExampleValues(true);
    }

    [Test, Order(4)]
    public void DatabaseDelete()
    {
        CLIActions cliAction = CreateCliActions(true, CONNECTION_STRING_LABOR, new string[] { });
        cliAction.ExecuteActions();

        using (var db = new Data.DigitalSignageDbContext(dbContextOptions(CONNECTION_STRING_LABOR)))
        {
            ClassicAssert.AreEqual(db.Stammdaten.ToArray().Count<Stammdaten>(), 0);
            ClassicAssert.AreEqual(db.Verfahren.ToArray().Count<Verfahren>(), 0);
            ClassicAssert.AreEqual(db.Besetzung.ToArray().Count<Besetzung>(), 0);
            ClassicAssert.AreEqual(db.ParteienAktiv.ToArray().Count<ParteienAktiv>(), 0);
            ClassicAssert.AreEqual(db.ParteienBeigeladen.ToArray().Count<ParteienBeigeladen>(), 0);
            ClassicAssert.AreEqual(db.ParteienPassiv.ToArray().Count<ParteienPassiv>(), 0);
            ClassicAssert.AreEqual(db.ParteienSV.ToArray().Count<ParteienSV>(), 0);
            ClassicAssert.AreEqual(db.ParteienZeugen.ToArray().Count<ParteienZeugen>(), 0);
            ClassicAssert.AreEqual(db.ProzBevAktiv.ToArray().Count<ProzBevAktiv>(), 0);
            ClassicAssert.AreEqual(db.ProzBevBeigeladen.ToArray().Count<ProzBevBeigeladen>(), 0);
            ClassicAssert.AreEqual(db.ProzBevPassiv.ToArray().Count<ProzBevPassiv>(), 0);
        }
    }

    [Test]
    public void CompareProdTest()
    {
        var xmlFiles = new List<string>();
        int i;

        // XML Files aus Echtbereich ermitteln
        using (var dbProd = new Data.DigitalSignageDbContext(dbContextOptions(CONNECTION_STRING_PROD)))
        {
            Basics[] basics = dbProd.Basics.ToArray();
            foreach (var b in basics)
            {
                xmlFiles.Add(b.toXMLFullPath);
            }
        }

        //XML´s aus dem Echtbereich einlesen
        CLIActions cliAction = CreateCliActions(true, CONNECTION_STRING_LABOR, xmlFiles.ToArray());
        cliAction.ExecuteActions();

        //Vergleichen
        using (var dbProd = new Data.DigitalSignageDbContext(dbContextOptions(CONNECTION_STRING_PROD)))
        using (var dbTest = new Data.DigitalSignageDbContext(dbContextOptions(CONNECTION_STRING_LABOR)))
        {
            Stammdaten[] PROD_st = dbProd.Stammdaten.ToArray();
            Stammdaten[] TEST_st = dbTest.Stammdaten.ToArray();
            for (i = 0; i < PROD_st.Count(); i++)
            {
                ClassicAssert.AreEqual(PROD_st[i].Datum, TEST_st[i].Datum);
                ClassicAssert.AreEqual(prodVal(PROD_st[i].Gerichtsname), TEST_st[i].Gerichtsname);
            }

            Verfahren[] PROD_verf = dbProd.Verfahren.ToArray();
            Verfahren[] TEST_verf = dbTest.Verfahren.ToArray();
            if (null != PROD_verf)
            {
                for (i = 0; i < PROD_verf.Count(); i++)
                {
                    ClassicAssert.AreEqual(PROD_verf[i].Lfdnr, TEST_verf[i].Lfdnr);
                    ClassicAssert.AreEqual(PROD_verf[i].Kammer, TEST_verf[i].Kammer);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Sitzungssaal), TEST_verf[i].Sitzungssaal);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].UhrzeitAktuell), TEST_verf[i].UhrzeitAktuell);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].UhrzeitPlan), TEST_verf[i].UhrzeitPlan);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Status), TEST_verf[i].Status);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Oeffentlich), TEST_verf[i].Oeffentlich);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Art), TEST_verf[i].Art);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Az), TEST_verf[i].Az);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Gegenstand), TEST_verf[i].Gegenstand);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Bemerkung1), TEST_verf[i].Bemerkung1);
                    ClassicAssert.AreEqual(prodVal(PROD_verf[i].Bemerkung2), TEST_verf[i].Bemerkung2);
                }
            }

            Besetzung[] PROD_bes = dbProd.Besetzung.ToArray();
            Besetzung[] TEST_bes = dbTest.Besetzung.ToArray();
            if (null != PROD_bes)
            {
                for (i = 0; i < PROD_bes.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_bes[i].Richter), TEST_bes[i].Richter);
                }
            }

            ParteienAktiv[] PROD_aktivPar = dbProd.ParteienAktiv.ToArray();
            ParteienAktiv[] TEST_aktivPar = dbTest.ParteienAktiv.ToArray();
            if (null != PROD_aktivPar)
            {
                for (i = 0; i < PROD_aktivPar.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_aktivPar[i].Partei), TEST_aktivPar[i].Partei);
                }
            }

            ProzBevAktiv[] PROD_aktivProzbez = dbProd.ProzBevAktiv.ToArray();
            ProzBevAktiv[] TEST_aktivProzbez = dbTest.ProzBevAktiv.ToArray();
            if (null != PROD_aktivProzbez)
            {
                for (i = 0; i < PROD_aktivProzbez.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_aktivProzbez[i].PB), TEST_aktivProzbez[i].PB);
                }
            }

            ParteienPassiv[] PROD_passivPar = dbProd.ParteienPassiv.ToArray();
            ParteienPassiv[] TEST_passivPar = dbTest.ParteienPassiv.ToArray();
            if (null != PROD_passivPar)
            {
                for (i = 0; i < PROD_passivPar.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_passivPar[i].Partei), TEST_passivPar[i].Partei);
                }
            }

            ProzBevPassiv[] PROD_passivProzBez = dbProd.ProzBevPassiv.ToArray();
            ProzBevPassiv[] TEST_passivProzBez = dbTest.ProzBevPassiv.ToArray();
            if (null != PROD_passivProzBez)
            {
                for (i = 0; i < PROD_passivProzBez.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_passivProzBez[i].PB), TEST_passivProzBez[i].PB);
                }
            }

            ParteienBeigeladen[] PROD_beiPar = dbProd.ParteienBeigeladen.ToArray();
            ParteienBeigeladen[] TEST_beiPar = dbTest.ParteienBeigeladen.ToArray();
            if (null != PROD_beiPar)
            {
                for (i = 0; i < PROD_beiPar.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_beiPar[i].Partei), TEST_beiPar[i].Partei);
                }
            }

            ProzBevBeigeladen[] PROD_beiProzBez = dbProd.ProzBevBeigeladen.ToArray();
            ProzBevBeigeladen[] TEST_beiProzBez = dbTest.ProzBevBeigeladen.ToArray();
            if (null != PROD_beiProzBez)
            {
                for (i = 0; i < PROD_beiProzBez.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_beiProzBez[i].PB), TEST_beiProzBez[i].PB);
                }
            }

            ParteienSV[] PROD_svPar = dbProd.ParteienSV.ToArray();
            ParteienSV[] TEST_svPar = dbTest.ParteienSV.ToArray();
            if (null != PROD_svPar)
            {
                for (i = 0; i < PROD_svPar.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_svPar[i].Partei), TEST_svPar[i].Partei);
                }
            }

            ParteienZeugen[] PROD_zegenPar = dbProd.ParteienZeugen.ToArray();
            ParteienZeugen[] TEST_zegenPar = dbTest.ParteienZeugen.ToArray();
            if (null != PROD_zegenPar)
            {
                for (i = 0; i < PROD_zegenPar.Count(); i++)
                {
                    ClassicAssert.AreEqual(prodVal(PROD_zegenPar[i].Partei), TEST_zegenPar[i].Partei);
                }
            }
        }
    }

    private void CheckExampleValues(bool updated = false)
    {
        using (var db = new Data.DigitalSignageDbContext(dbContextOptions(CONNECTION_STRING_LABOR)))
        {
            Stammdaten[] st = db.Stammdaten.ToArray();
            ClassicAssert.AreEqual(st.Count(), 5);
            ClassicAssert.AreEqual(st[0].Gerichtsname, "Oberverwaltungsgericht Rheinland-Pfalz");
            ClassicAssert.AreEqual(st[1].Gerichtsname, "Verfassungsgerichtshof Rheinland-Pfalz");
            ClassicAssert.AreEqual(st[1].Datum, "23.01.2018");
            ClassicAssert.AreEqual(st[2].Gerichtsname, "Verwaltungsgericht Koblenz");
            ClassicAssert.AreEqual(st[2].Datum, "23.01.2018");
            ClassicAssert.AreEqual(st[3].Gerichtsname, "Arbeitsgericht Koblenz");
            ClassicAssert.AreEqual(st[3].Datum, "02.02.2018");
            ClassicAssert.AreEqual(st[4].Gerichtsname, "Sozialgericht Koblenz");

            Verfahren[] verf = db.Verfahren.ToArray();
            ClassicAssert.AreEqual(verf.Count(), 9);
            ClassicAssert.AreEqual(verf[0].Lfdnr, 1);
            ClassicAssert.AreEqual(verf[0].Kammer, 1);
            ClassicAssert.AreEqual(verf[0].Sitzungssaal, "Sitzungssaal E009");
            ClassicAssert.AreEqual(verf[0].UhrzeitAktuell, "10:00");
            ClassicAssert.AreEqual(verf[0].UhrzeitPlan, "10:00");
            ClassicAssert.AreEqual(verf[0].Status, "");
            ClassicAssert.AreEqual(verf[0].Oeffentlich, "ja");
            ClassicAssert.AreEqual(verf[0].Art, "mündliche Verhandlung");
            ClassicAssert.AreEqual(verf[0].Az, "VGH A 01/01");
            ClassicAssert.AreEqual(verf[0].Gegenstand, "§ 72 Abs. 1 und 2 der Geschäftsordnung des Landtags Rheinland-Pfalz vom 31. Mai 2017");
            ClassicAssert.AreEqual(verf[1].Lfdnr, 1);
            ClassicAssert.AreEqual(verf[1].Kammer, 1);
            ClassicAssert.AreEqual(verf[1].Sitzungssaal, "Sitzungssaal A021");
            ClassicAssert.AreEqual(verf[1].UhrzeitAktuell, "09:00");
            ClassicAssert.AreEqual(verf[1].UhrzeitPlan, "09:00");
            ClassicAssert.AreEqual(verf[1].Status, "");
            ClassicAssert.AreEqual(verf[1].Oeffentlich, "ja");
            ClassicAssert.AreEqual(verf[1].Art, "mündliche Verhandlung");
            ClassicAssert.AreEqual(verf[1].Az, "9 A 321/23.KO");
            ClassicAssert.AreEqual(verf[1].Gegenstand, "Kommunalrechts");
            ClassicAssert.AreEqual(verf[2].Lfdnr, 2);
            ClassicAssert.AreEqual(verf[2].Kammer, 1);
            ClassicAssert.AreEqual(verf[2].Sitzungssaal, "Sitzungssaal A021");
            ClassicAssert.AreEqual(verf[2].UhrzeitAktuell, "09:45");
            ClassicAssert.AreEqual(verf[2].UhrzeitPlan, "09:45");
            ClassicAssert.AreEqual(verf[2].Status, "");
            ClassicAssert.AreEqual(verf[2].Oeffentlich, "ja");
            ClassicAssert.AreEqual(verf[2].Art, "mündliche Verhandlung");
            ClassicAssert.AreEqual(verf[2].Az, "1 A 123/23.KO");
            ClassicAssert.AreEqual(verf[2].Gegenstand, "Wasserrechts");
            ClassicAssert.AreEqual(verf[3].Lfdnr, 3);
            ClassicAssert.AreEqual(verf[3].Kammer, 1);
            ClassicAssert.AreEqual(verf[3].Sitzungssaal, "Sitzungssaal A021");
            ClassicAssert.AreEqual(verf[3].UhrzeitAktuell, "10:30");
            ClassicAssert.AreEqual(verf[3].UhrzeitPlan, "10:30");

            ClassicAssert.AreEqual(verf[3].Status, "");
            ClassicAssert.AreEqual(verf[3].Oeffentlich, "ja");
            ClassicAssert.AreEqual(verf[3].Art, "mündliche Verhandlung");
            ClassicAssert.AreEqual(verf[3].Az, "1 K 759/17.KO");
            ClassicAssert.AreEqual(verf[3].Gegenstand, "Kommunalverfassungsrechts");
            ClassicAssert.AreEqual(verf[4].Lfdnr, 1);
            ClassicAssert.AreEqual(verf[4].Kammer, 7);
            ClassicAssert.AreEqual(verf[4].Sitzungssaal, "Sitzungssaal A026");
            if (updated)
            {
                ClassicAssert.AreEqual(verf[4].UhrzeitAktuell, "09:10");
                ClassicAssert.AreEqual(verf[4].UhrzeitPlan, "09:10");
            }
            else
            {
                ClassicAssert.AreEqual(verf[4].UhrzeitAktuell, "09:00");
                ClassicAssert.AreEqual(verf[4].UhrzeitPlan, "09:00");
            }
            ClassicAssert.AreEqual(verf[4].Status, "");
            ClassicAssert.AreEqual(verf[4].Oeffentlich, "ja");
            ClassicAssert.AreEqual(verf[4].Art, "Gütetermin");
            ClassicAssert.AreEqual(verf[4].Az, "1 A 0185/24");
            ClassicAssert.AreEqual(verf[4].Gegenstand, "Zahlungsklagen\nSonstiges");
            ClassicAssert.AreEqual(verf[4].Bemerkung2, "bisher nur GT");

            Besetzung[] bes = db.Besetzung.ToArray();
            ClassicAssert.AreEqual(bes.Count(), 29);
            ClassicAssert.AreEqual(bes[0].Richter, "Präsident des Verfassungsgerichtshofs");
            ClassicAssert.AreEqual(bes[1].Richter, "Vizepräsidentin des Oberverwaltungsgerichts");
            ClassicAssert.AreEqual(bes[2].Richter, "Präsident des Finanzgerichts");
            ClassicAssert.AreEqual(bes[3].Richter, "Präsidentin des Oberlandesgerichts");
            ClassicAssert.AreEqual(bes[4].Richter, "Ehrenrat Dr. Sost");
            ClassicAssert.AreEqual(bes[5].Richter, "Rechtsanwältin Dr. Schlau");
            ClassicAssert.AreEqual(bes[6].Richter, "Univ.-Professor Dr. Müllersen");
            ClassicAssert.AreEqual(bes[7].Richter, "Kreisverwaltungsdirektorin Hammer");
            ClassicAssert.AreEqual(bes[8].Richter, "Univ.-Professor Dr. Rotor");
            ClassicAssert.AreEqual(bes[9].Richter, "Richter am Verwaltungsgericht Dr. Eicher");
            ClassicAssert.AreEqual(bes[10].Richter, "Richter am Verwaltungsgericht Putz");
            ClassicAssert.AreEqual(bes[11].Richter, "Richterin David");
            ClassicAssert.AreEqual(bes[12].Richter, "ehrenamtliche Richterin Maler Karl");
            ClassicAssert.AreEqual(bes[13].Richter, "ehrenamtliche Richterin Geschäftsführerin King");
            ClassicAssert.AreEqual(bes[14].Richter, "Richter am Verwaltungsgericht Dr. Eicher");
            ClassicAssert.AreEqual(bes[15].Richter, "Richter am Verwaltungsgericht Putz");
            ClassicAssert.AreEqual(bes[16].Richter, "Richterin David");
            ClassicAssert.AreEqual(bes[17].Richter, "ehrenamtliche Richterin Maler Karl");
            ClassicAssert.AreEqual(bes[18].Richter, "ehrenamtliche Richterin Geschäftsführerin King");
            ClassicAssert.AreEqual(bes[19].Richter, "Richter am Verwaltungsgericht Dr. Eicher");
            ClassicAssert.AreEqual(bes[20].Richter, "Richter am Verwaltungsgericht Putz");
            ClassicAssert.AreEqual(bes[21].Richter, "Richterin David");
            ClassicAssert.AreEqual(bes[22].Richter, "ehrenamtliche Richterin Maler Karl");
            ClassicAssert.AreEqual(bes[23].Richter, "ehrenamtliche Richterin Geschäftsführerin King");
            ClassicAssert.AreEqual(bes[24].Richter, "Richter am Arbeitsgericht Dr. Hüber");

            ParteienAktiv[] aktivPar = db.ParteienAktiv.ToArray();
            ClassicAssert.AreEqual(aktivPar.Count(), 12);
            ClassicAssert.AreEqual(aktivPar[0].Partei, "der Fraktion der alternativen Wähler");
            ClassicAssert.AreEqual(aktivPar[1].Partei, "Johannes Peter Müller");
            ClassicAssert.AreEqual(aktivPar[2].Partei, "Hans Müller Dorfstraße 10, 56067 Koblenz vertreten durch seine Betreuerin Anja Müller");
            ClassicAssert.AreEqual(aktivPar[3].Partei, "2. Hans Müller Dorfstraße 10, 56067 Koblenz vertreten durch seine Betreuerin Anja Müller");
            ClassicAssert.AreEqual(aktivPar[4].Partei, "1. SPD-Kreistagsfraktion des Rhein-Hunsrück-Kreises vertreten durch den Fraktionsvorsitzenden Michael Mustermann");
            ClassicAssert.AreEqual(aktivPar[5].Partei, "2. Fraktion der Freien Wähler Rhein-Hunsrück e.V. im Kreistrag vertreten durch den Fraktionsvorsitzenden Stefan Mustermann");
            ClassicAssert.AreEqual(aktivPar[6].Partei, "3. FDP-Kreistagsfraktion des Rhein-Hunsrück-Kreises vertreten durch den FraktionsvorsitzendenThomas Mustermann");
            if (updated)
            { ClassicAssert.AreEqual(aktivPar[7].Partei, "Sabrina van de meiklokjes"); }
            else
            { ClassicAssert.AreEqual(aktivPar[7].Partei, "Sabrina Muster"); }

            ProzBevAktiv[] aktivProzbez = db.ProzBevAktiv.ToArray();
            ClassicAssert.AreEqual(aktivProzbez.Count(), 7);
            ClassicAssert.AreEqual(aktivProzbez[0].PB, "Proz.-Bev.: Prof. Michael Muster");
            ClassicAssert.AreEqual(aktivProzbez[1].PB, "Proz.-Bev.: Rechtsanwalt Hans Mustername");
            ClassicAssert.AreEqual(aktivProzbez[2].PB, "Proz.-Bev.: zu 1-3: Kunz Rechtsanwälte");
            ClassicAssert.AreEqual(aktivProzbez[3].PB, "Proz.-Bev.: Rechtsanwälte Dr. Eich, Jakob & Partner mbB");

            ParteienPassiv[] passivPar = db.ParteienPassiv.ToArray();
            ClassicAssert.AreEqual(passivPar.Count(), 9);
            ClassicAssert.AreEqual(passivPar[0].Partei, "das Land Rheinland-Pfalz vertreten durch den Landtag Rheinland-Pfalz");
            ClassicAssert.AreEqual(passivPar[1].Partei, "Ortsgemeinde Rengsdorf vertreten durch den Bürgermeister der Verbandsgemeinde Rengsdorf-Waldbreitbach");
            ClassicAssert.AreEqual(passivPar[2].Partei, "Verbandsgemeinde Bad Ems vertreten durch den Bürgermeister");
            ClassicAssert.AreEqual(passivPar[3].Partei, "Landrat des Landkreis Dr. Micheal Müller");
            ClassicAssert.AreEqual(passivPar[4].Partei, "Ulrich Finzler");

            ProzBevPassiv[] passivProzBez = db.ProzBevPassiv.ToArray();
            ClassicAssert.AreEqual(passivProzBez.Count(), 8);
            ClassicAssert.AreEqual(passivProzBez[0].PB, "Proz.-Bev.: Rechtsanwälte Jeromin & Kerkmann");
            ClassicAssert.AreEqual(passivProzBez[1].PB, "Proz.-Bev.: Rechtsanwälte Dr. Martini, Mogg, Vogt PartGmbB");
            ClassicAssert.AreEqual(passivProzBez[2].PB, "Proz.-Bev.: Rechtsanwälte Dr. Martini2, Mogg, Vogt PartGmbB");
            ClassicAssert.AreEqual(passivProzBez[3].PB, "Proz.-Bev.: Rechtsanwälte Braun Baulig Berninger");

            ParteienBeigeladen[] beiPar = db.ParteienBeigeladen.ToArray();
            ClassicAssert.AreEqual(beiPar.Count(), 3);
            ClassicAssert.AreEqual(beiPar[0].Partei, "1. ABCERT AG");
            ClassicAssert.AreEqual(beiPar[1].Partei, "2. Helmut Grün");
            ClassicAssert.AreEqual(beiPar[2].Partei, "3. Erika Grün");

            ProzBevBeigeladen[] beiProzBez = db.ProzBevBeigeladen.ToArray();
            ClassicAssert.AreEqual(beiProzBez.Count(), 1);
            ClassicAssert.AreEqual(beiProzBez[0].PB, "Proz.-Bev.:\tzu 3: Rechtsanwälte Jeromin & Kerkmann");

            ParteienSV[] svPar = db.ParteienSV.ToArray();
            ClassicAssert.AreEqual(svPar.Count(), 2);
            ClassicAssert.AreEqual(svPar[0].Partei, "1. Edith Hof");
            ClassicAssert.AreEqual(svPar[1].Partei, "2. Willibald Hof");

            ParteienZeugen[] zegenPar = db.ParteienZeugen.ToArray();
            ClassicAssert.AreEqual(zegenPar.Count(), 2);
            ClassicAssert.AreEqual(zegenPar[0].Partei, "1. Hessel GmbH vertreten durch die Geschäftsführer");
            ClassicAssert.AreEqual(zegenPar[1].Partei, "2. Astrid Hessel");
        }
    }

    private string prodVal(string val)
    {
        return val.TrimEnd().Replace("\r\n", "\n").Replace("\r", "\n");
    }

    private CLIActions CreateCliActions(bool delete, string connectionString, string[] xmlFiles = null, bool Update = false)
    {
        var args = new List<string>();
        string AddOrUpdateCommand = COMMAND_ADD;
        if (Update)
            AddOrUpdateCommand = COMMAND_UPDATE;
        args.Add(COMMAND_CONNECTION);
        args.Add(connectionString);
        if (delete)
            args.Add(COMMAND_DELETE);
        if (null != xmlFiles)
        {
            foreach (string file in xmlFiles)
            {
                args.Add(AddOrUpdateCommand);
                args.Add(file);
            }
        }
        else
        {
            args.Add(AddOrUpdateCommand);
            args.Add(EXAMPLE_XML1);
            args.Add(AddOrUpdateCommand);
            args.Add(EXAMPLE_XML2);
            args.Add(AddOrUpdateCommand);
            args.Add(EXAMPLE_XML3);
            args.Add(AddOrUpdateCommand);
            args.Add(EXAMPLE_XML4);
            args.Add(AddOrUpdateCommand);
            args.Add(EXAMPLE_XML5);
        }

        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args.ToArray());
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        return cliActions;
    }
}