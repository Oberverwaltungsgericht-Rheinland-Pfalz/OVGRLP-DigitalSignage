// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.ImportCLI.Service;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DigitalSignage.ImportCLI.NUnitTests;

[TestFixture]
public class DBServiceTest
{
    private DBService _dBService { get; set; }
    private DigitalSignageDbContext context { get; set; }
    private DbContextOptions<DigitalSignageDbContext> _contextOpitons
    {
        get
        {
            return new DbContextOptionsBuilder<DigitalSignageDbContext>()
                .UseInMemoryDatabase("prodTest")
                .Options;
        }
    }

    [SetUp]
    public void SetItUp()
    {
        context = new DigitalSignageDbContext(_contextOpitons);
        _dBService = new DBService(context);
    }

    [TearDown]
    public void TearItDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Test]
    public void TestCLIParse()
    {
        var args = CreateCliActions(true , "");
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliActions = cliService.ParseCommandLineArguments(args.ToArray(), false);
        ClassicAssert.IsInstanceOf<CLIActions>(cliActions);
    }

    public void AddExamples ()
    {
        var args = new List<string>() { TestSets.COMMAND_CONNECTION, "-" };
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML1);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML2);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML3);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML4);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML5);

        CLIActions cliActions = new Service.CLIService().ParseCommandLineArguments(args.ToArray());
        cliActions.ExecuteActions(context);
    }


    [Test]
    public void CheckStammdaten()
    {
        AddExamples();

        CheckStammdatenA();
    }
    public void CheckStammdatenA()
    {
        CollectionAssert.IsNotEmpty(context.Stammdaten);
        Stammdaten[] st = context.Stammdaten.ToArray();
        Assert.That(st, Has.Exactly(5).Items);
        Assert.That(st[0].Gerichtsname, Is.EqualTo("Oberverwaltungsgericht Rheinland-Pfalz"));
        Assert.That(st[1].Gerichtsname, Is.EqualTo("Verfassungsgerichtshof Rheinland-Pfalz"));
        Assert.That(st[1].Datum, Is.EqualTo("23.01.2018"));
        Assert.That(st[2].Gerichtsname, Is.EqualTo("Verwaltungsgericht Koblenz"));
        Assert.That(st[2].Datum, Is.EqualTo("23.01.2018"));
        Assert.That(st[3].Gerichtsname, Is.EqualTo("Arbeitsgericht Koblenz"));
        Assert.That(st[3].Datum, Is.EqualTo("02.02.2018"));
        Assert.That(st[4].Gerichtsname, Is.EqualTo("Sozialgericht Koblenz"));
    }


    [Test]
    public void DatabaseAddExamples()
    {
        // prepare
        AddExamples();

        // assert
        CheckExampleValues(context);
    }

    [Test]
    public void TestComplexObjectStructure()
    {
        // prepare, read only VGH_TO.xml
        var args = new List<string>() { TestSets.COMMAND_CONNECTION, "-", TestSets.COMMAND_ADD, TestSets.EXAMPLE_XML2 };
        CLIActions cliActions = new CLIService().ParseCommandLineArguments(args.ToArray());
        
        // act
        var db = context;
        cliActions.ExecuteActions(db);

        // assert
        CollectionAssert.IsNotEmpty(db.Stammdaten);
        CollectionAssert.IsNotEmpty(db.Verfahren);
        CollectionAssert.IsNotEmpty(db.ParteienAktiv);
        CollectionAssert.IsNotEmpty(db.ParteienPassiv);
        CollectionAssert.IsNotEmpty(db.ProzBevAktiv);

        CollectionAssert.IsEmpty(db.ParteienBeigeladen);
        CollectionAssert.IsEmpty(db.ParteienSV);
        CollectionAssert.IsEmpty(db.ParteienZeugen);
        CollectionAssert.IsEmpty(db.ProzBevBeigeladen);
        CollectionAssert.IsEmpty(db.ProzBevPassiv);


        Assert.That(db.Stammdaten.Count, Is.EqualTo(1));
        Assert.That(db.Verfahren.Count, Is.EqualTo(1));

        Assert.That(context.Stammdaten, Has.Exactly(1).Matches(new Predicate<Stammdaten>(s => 
            s.Gerichtsname.Equals("Verfassungsgerichtshof Rheinland-Pfalz") && s.Datum.Equals("23.01.2018"))));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v => 
            v.Lfdnr.Equals(1) && v.Kammer.Equals(1) && v.Sitzungssaal.Equals("Sitzungssaal E009"))));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
            v.Oeffentlich.Equals("ja") && v.Art.Equals("mündliche Verhandlung") && v.Az.Equals("VGH A 01/01"))));

        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
            v.Gegenstand.Equals("§ 72 Abs. 1 und 2 der Geschäftsordnung des Landtags Rheinland-Pfalz vom 31. Mai 2017") 
            && v.Bemerkung1.Length == 0 && v.Bemerkung2.Length == 0)));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v => 
            v.Besetzung.Count == 9 && v.Besetzung.All(b => !string.IsNullOrEmpty(b.Richter)))));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
            v.ParteienAktiv.Count == 1 && v.ProzBevAktiv.Count == 1)));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
            v.ParteienPassiv.Count == 1 && v.ProzBevPassiv.Count == 0)));

        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
            v.ParteienPassiv.Count(p => p.Partei.Equals("das Land Rheinland-Pfalz vertreten durch den Landtag Rheinland-Pfalz")) == 1)));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
            v.ParteienAktiv.Count(p => p.Partei.Equals("der Fraktion der alternativen Wähler")) == 1)));
        Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(v =>
           v.ProzBevAktiv.Count(p => p.PB.Equals("Proz.-Bev.: Prof. Michael Muster")) == 1)));
    }

    [Test]
    public void DatabaseUpdateExamples()
    {
        var args = new List<string>() { TestSets.COMMAND_CONNECTION, "-" };
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML1);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML2);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML3);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML4);
        args.Add(TestSets.COMMAND_ADD);
        args.Add(TestSets.EXAMPLE_XML5);
        args.Add(TestSets.COMMAND_UPDATE);
        args.Add(TestSets.EXAMPLE_UPDATE_XML);

        CLIActions cliActions = new Service.CLIService().ParseCommandLineArguments(args.ToArray());

        cliActions.ExecuteActions(context);

        CheckExampleValues(context, true);
    }

    [Test]
    public void DatabaseDelete()
    {
        var db = context;
        //prepare
        AddExamples();
        CollectionAssert.IsNotEmpty(db.Stammdaten);

        Assert.That(db.Stammdaten, Is.Not.Empty);
        CollectionAssert.IsNotEmpty(db.Stammdaten);
        CollectionAssert.IsNotEmpty(db.Verfahren);
        CollectionAssert.IsNotEmpty(db.ParteienAktiv);
        CollectionAssert.IsNotEmpty(db.ParteienBeigeladen);
        CollectionAssert.IsNotEmpty(db.ParteienPassiv);
        CollectionAssert.IsNotEmpty(db.ParteienSV);
        CollectionAssert.IsNotEmpty(db.ParteienZeugen);
        CollectionAssert.IsNotEmpty(db.ProzBevAktiv);
        CollectionAssert.IsNotEmpty(db.ProzBevBeigeladen);
        CollectionAssert.IsNotEmpty(db.ProzBevPassiv);

        // act
        _dBService.DeleteAll();

        // assert
        Assert.That(db, Is.Not.Null);
        CollectionAssert.IsEmpty(db.Stammdaten);
        CollectionAssert.IsEmpty(db.Verfahren);
        CollectionAssert.IsEmpty(db.ParteienAktiv);
        CollectionAssert.IsEmpty(db.ParteienBeigeladen);
        CollectionAssert.IsEmpty(db.ParteienPassiv);
        CollectionAssert.IsEmpty(db.ParteienSV);
        CollectionAssert.IsEmpty(db.ParteienZeugen);
        CollectionAssert.IsEmpty(db.ProzBevAktiv);
        CollectionAssert.IsEmpty(db.ProzBevBeigeladen);
        CollectionAssert.IsEmpty(db.ProzBevPassiv);
    }

//    [Test]
    public void CompareProdTest()
    {
        var xmlFiles = new List<string>();
        int i;

        // XML Files aus Echtbereich ermitteln
        using (var dbProd = new Data.DigitalSignageDbContext(TestSets.dbContextOptions(TestSets.CONNECTION_STRING_PROD)))
        {
            Basics[] basics = context.Basics.ToArray();
            foreach (var b in basics)
            {
                xmlFiles.Add(b.toXMLFullPath);
            }
        }

        //XML´s aus dem Echtbereich einlesen
        var args = CreateCliActions(true, "", xmlFiles.ToArray());
        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        CLIActions cliAction = cliService.ParseCommandLineArguments(args.ToArray());
        ClassicAssert.IsInstanceOf<CLIActions>(cliAction);

        cliAction.ExecuteActions(context);

        //Vergleichen
        using (var dbProd = new Data.DigitalSignageDbContext(TestSets.dbContextOptions(TestSets.CONNECTION_STRING_PROD)))
        using (var dbTest = new Data.DigitalSignageDbContext(TestSets.dbContextOptions(TestSets.CONNECTION_STRING_LABOR)))
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

    private void CheckExampleValues(DigitalSignageDbContext db, bool updated = false)
    {
        CheckStammdatenA();

        CheckVerfahrenA(updated);

        CheckBesetzungA();

        CheckParteienA(updated);

        CheckProzBevAktivA();

        CheckParteienPassivA();

        CheckProzBevPassivA();

        CheckParteienBeigeladenA();

        CheckProzBeigeladenA();

        CheckParteienSVA();

        CheckZeugenA();
        }


    [Test]
    public void CheckVerfahren()
    {
        AddExamples();
        CheckVerfahrenA(false);
    }
    public void CheckVerfahrenA(bool updated)
    {
        Verfahren[] verf = context.Verfahren.ToArray();
        Assert.That(verf.Count(), Is.EqualTo(9));
        Assert.That(verf[0].Lfdnr, Is.EqualTo(1));
        Assert.That(verf[0].Kammer, Is.EqualTo(1));
        Assert.That(verf[0].Sitzungssaal, Is.EqualTo("Sitzungssaal E009"));
        Assert.That(verf[0].UhrzeitAktuell, Is.EqualTo("10:00"));
        Assert.That(verf[0].UhrzeitPlan, Is.EqualTo("10:00"));
        Assert.That(verf[0].Status, Is.EqualTo(""));
        Assert.That(verf[0].Oeffentlich, Is.EqualTo("ja"));
        Assert.That(verf[0].Art, Is.EqualTo("mündliche Verhandlung"));
        Assert.That(verf[0].Az, Is.EqualTo("VGH A 01/01"));
        Assert.That(verf[0].Gegenstand, Is.EqualTo("§ 72 Abs. 1 und 2 der Geschäftsordnung des Landtags Rheinland-Pfalz vom 31. Mai 2017"));
        Assert.That(verf[1].Lfdnr, Is.EqualTo(1));
        Assert.That(verf[1].Kammer, Is.EqualTo(1));
        Assert.That(verf[1].Sitzungssaal, Is.EqualTo("Sitzungssaal A021"));
        Assert.That(verf[1].UhrzeitAktuell, Is.EqualTo("09:00"));
        Assert.That(verf[1].UhrzeitPlan, Is.EqualTo("09:00"));
        Assert.That(verf[1].Status, Is.EqualTo(""));
        Assert.That(verf[1].Oeffentlich, Is.EqualTo("ja"));
        Assert.That(verf[1].Art, Is.EqualTo("mündliche Verhandlung"));
        Assert.That(verf[1].Az, Is.EqualTo("9 A 321/23.KO"));
        Assert.That(verf[1].Gegenstand, Is.EqualTo("Kommunalrechts"));
        Assert.That(verf[2].Lfdnr, Is.EqualTo(2));
        Assert.That(verf[2].Kammer, Is.EqualTo(1));
        Assert.That(verf[2].Sitzungssaal, Is.EqualTo("Sitzungssaal A021"));
        Assert.That(verf[2].UhrzeitAktuell, Is.EqualTo("09:45"));
        Assert.That(verf[2].UhrzeitPlan, Is.EqualTo("09:45"));
        Assert.That(verf[2].Status, Is.EqualTo(""));
        Assert.That(verf[2].Oeffentlich, Is.EqualTo("ja"));
        Assert.That(verf[2].Art, Is.EqualTo("mündliche Verhandlung"));
        Assert.That(verf[2].Az, Is.EqualTo("1 A 123/23.KO"));
        Assert.That(verf[2].Gegenstand, Is.EqualTo("Wasserrechts"));
        Assert.That(verf[3].Lfdnr, Is.EqualTo(3));
        Assert.That(verf[3].Kammer, Is.EqualTo(1));
        Assert.That(verf[3].Sitzungssaal, Is.EqualTo("Sitzungssaal A021"));
        Assert.That(verf[3].UhrzeitAktuell, Is.EqualTo("10:30"));
        Assert.That(verf[3].UhrzeitPlan, Is.EqualTo("10:30"));

        Assert.That(verf[3].Status, Is.EqualTo(""));
        Assert.That(verf[3].Oeffentlich, Is.EqualTo("ja"));
        Assert.That(verf[3].Art, Is.EqualTo("mündliche Verhandlung"));
        Assert.That(verf[3].Az, Is.EqualTo("1 K 759/17.KO"));
        Assert.That(verf[3].Gegenstand, Is.EqualTo("Kommunalverfassungsrechts"));



        Assert.That(verf[4].Lfdnr, Is.EqualTo(5));
        Assert.That(verf[4].Kammer, Is.EqualTo(7));
        Assert.That(verf[4].Sitzungssaal, Is.EqualTo("Sitzungssaal A026"));
        
        if (updated)
            Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(p =>
                p.Lfdnr == 1 && p.Kammer == 7 && p.Sitzungssaal == "Sitzungssaal A026"
                && p.UhrzeitAktuell.Equals("09:10") && p.UhrzeitPlan.Equals("09:10") &&
                p.Status.Equals("") && p.Oeffentlich.Equals("ja")
                && p.Art.Equals("Gütetermin") && p.Az.Equals("1 A 0185/24") && p.Gegenstand.Equals("Zahlungsklagen\nSonstiges")
                && p.Bemerkung2.Equals("bisher nur GT")))
                );
        else
            Assert.That(context.Verfahren, Has.Exactly(1).Matches(new Predicate<Verfahren>(p => 
                p.Lfdnr == 1 && p.Kammer == 7 && p.Sitzungssaal == "Sitzungssaal A026" 
                && p.UhrzeitAktuell.Equals("09:00") && p.UhrzeitPlan.Equals("09:00") &&
                p.Status.Equals("") && p.Oeffentlich.Equals("ja") 
                && p.Art.Equals("Gütetermin") && p.Az.Equals("1 A 0185/24") && p.Gegenstand.Equals("Zahlungsklagen\nSonstiges") 
                && p.Bemerkung2.Equals("bisher nur GT")))
                );
    }

    [Test]
    public void CheckBesetzung()
    {
        AddExamples();

        CheckBesetzungA();
    }
    public void CheckBesetzungA()
    {
        Besetzung[] bes = context.Besetzung.ToArray();
        Assert.That(bes.Count(), Is.EqualTo(29));
        Assert.That(bes[0].Richter, Is.EqualTo("Präsident des Verfassungsgerichtshofs"));
        Assert.That(bes[1].Richter, Is.EqualTo("Vizepräsidentin des Oberverwaltungsgerichts"));
        Assert.That(bes[2].Richter, Is.EqualTo("Präsident des Finanzgerichts"));
        Assert.That(bes[3].Richter, Is.EqualTo("Präsidentin des Oberlandesgerichts"));
        Assert.That(bes[4].Richter, Is.EqualTo("Ehrenrat Dr. Sost"));
        Assert.That(bes[5].Richter, Is.EqualTo("Rechtsanwältin Dr. Schlau"));
        Assert.That(bes[6].Richter, Is.EqualTo("Univ.-Professor Dr. Müllersen"));
        Assert.That(bes[7].Richter, Is.EqualTo("Kreisverwaltungsdirektorin Hammer"));
        Assert.That(bes[8].Richter, Is.EqualTo("Univ.-Professor Dr. Rotor"));
        Assert.That(context.Besetzung, Has.Exactly(3).Matches(new Predicate<Besetzung>(b => b.Richter.Equals("Richterin David"))));
        Assert.That(context.Besetzung, Has.Exactly(3).Matches(new Predicate<Besetzung>(b => b.Richter.Equals("Richter am Verwaltungsgericht Putz"))));
        Assert.That(context.Besetzung, Has.Exactly(3).Matches(new Predicate<Besetzung>(b => b.Richter.Equals("ehrenamtliche Richterin Maler Karl"))));
        Assert.That(context.Besetzung, Has.Exactly(3).Matches(new Predicate<Besetzung>(b => b.Richter.Equals("Richter am Verwaltungsgericht Dr. Eicher"))));
        Assert.That(context.Besetzung, Has.Exactly(3).Matches(new Predicate<Besetzung>(b => b.Richter.Equals("ehrenamtliche Richterin Geschäftsführerin King"))));

        Assert.That(context.Verfahren, Has.Exactly(3).Matches(new Predicate<Verfahren>(v => 
            v.Besetzung.Count(b => b.Richter.Equals("Richterin David")) == 1 &&
            v.Besetzung.Count(b => b.Richter.Equals("Richter am Verwaltungsgericht Putz")) == 1 &&
            v.Besetzung.Count(b => b.Richter.Equals("ehrenamtliche Richterin Maler Karl")) == 1 &&
            v.Besetzung.Count(b => b.Richter.Equals("Richter am Verwaltungsgericht Dr. Eicher")) == 1 &&
            v.Besetzung.Count(b => b.Richter.Equals("ehrenamtliche Richterin Geschäftsführerin King")) == 1
            )));
        //Assert.That(bes[9].Richter, Is.EqualTo("Richter am Verwaltungsgericht Dr. Eicher"));
        //Assert.That(bes[10].Richter, Is.EqualTo("Richter am Verwaltungsgericht Putz"));
        //Assert.That(bes[11].Richter, Is.EqualTo("Richterin David"));
        //Assert.That(bes[12].Richter, Is.EqualTo("ehrenamtliche Richterin Maler Karl"));
        //Assert.That(bes[13].Richter, Is.EqualTo("ehrenamtliche Richterin Geschäftsführerin King"));
        //Assert.That(bes[14].Richter, Is.EqualTo("Richter am Verwaltungsgericht Dr. Eicher"));
        //Assert.That(bes[15].Richter, Is.EqualTo("Richter am Verwaltungsgericht Putz"));
        //Assert.That(bes[16].Richter, Is.EqualTo("Richterin David"));
        //Assert.That(bes[17].Richter, Is.EqualTo("ehrenamtliche Richterin Maler Karl"));
        //Assert.That(bes[18].Richter, Is.EqualTo("ehrenamtliche Richterin Geschäftsführerin King"));
        //Assert.That(bes[19].Richter, Is.EqualTo("Richter am Verwaltungsgericht Dr. Eicher"));
        //Assert.That(bes[20].Richter, Is.EqualTo("Richter am Verwaltungsgericht Putz"));
        //Assert.That(bes[21].Richter, Is.EqualTo("Richterin David"));
        //Assert.That(bes[22].Richter, Is.EqualTo("ehrenamtliche Richterin Maler Karl"));
        //Assert.That(bes[23].Richter, Is.EqualTo("ehrenamtliche Richterin Geschäftsführerin King")   );
        Assert.That(bes[24].Richter, Is.EqualTo("Richter am Arbeitsgericht Dr. Hüber"));
        Assert.That(context.Besetzung.Last().Richter, Is.EqualTo("Richter am Arbeitsgericht Dr. Hüber"));
    }

    [Test]
    public void CheckParteien()
    {
        AddExamples();
        CheckParteienA(false);
    }


    public void CheckParteienA(bool updated)
    {
        ParteienAktiv[] aktivPar = context.ParteienAktiv.ToArray();
        Assert.That(aktivPar.Count(), Is.EqualTo(12));
        Assert.That(aktivPar[0].Partei, Is.EqualTo("der Fraktion der alternativen Wähler"));
        Assert.That(aktivPar[1].Partei, Is.EqualTo("Johannes Peter Müller"));
        Assert.That(aktivPar[2].Partei, Is.EqualTo("Hans Müller Dorfstraße 10, 56067 Koblenz vertreten durch seine Betreuerin Anja Müller"));
        Assert.That(aktivPar[3].Partei, Is.EqualTo("2. Hans Müller Dorfstraße 10, 56067 Koblenz vertreten durch seine Betreuerin Anja Müller"));
        Assert.That(aktivPar[4].Partei, Is.EqualTo("1. SPD-Kreistagsfraktion des Rhein-Hunsrück-Kreises vertreten durch den Fraktionsvorsitzenden Michael Mustermann"));
        Assert.That(aktivPar[5].Partei, Is.EqualTo("2. Fraktion der Freien Wähler Rhein-Hunsrück e.V. im Kreistrag vertreten durch den Fraktionsvorsitzenden Stefan Mustermann"));
        Assert.That(aktivPar[6].Partei, Is.EqualTo("3. FDP-Kreistagsfraktion des Rhein-Hunsrück-Kreises vertreten durch den FraktionsvorsitzendenThomas Mustermann"));

        if (updated)
            Assert.That(aktivPar[7].Partei, Is.EqualTo("Sabrina van de meiklokjes")); 
        else
            Assert.That(context.ParteienAktiv.Last().Partei, Is.EqualTo("Sabrina Muster"));
    }

    [Test]
    public void CheckProzBevAktiv()
    {
        AddExamples();
        CheckProzBevAktivA();    
    }

    public void CheckProzBevAktivA()
    {
        ProzBevAktiv[] aktivProzbez = context.ProzBevAktiv.ToArray();
        Assert.That(aktivProzbez.Count(), Is.EqualTo(7));
        Assert.That(aktivProzbez[0].PB, Is.EqualTo("Proz.-Bev.: Prof. Michael Muster"));
        Assert.That(aktivProzbez[1].PB, Is.EqualTo("Proz.-Bev.: Rechtsanwalt Hans Mustername"));
        Assert.That(aktivProzbez[2].PB, Is.EqualTo("Proz.-Bev.: zu 1-3: Kunz Rechtsanwälte"));

        Assert.That(context.ProzBevAktiv, Has.Exactly(1).Matches(new Predicate<ProzBevAktiv>(p => p.PB.Equals("Proz.-Bev.: Rechtsanwälte Dr. Eich, Jakob & Partner mbB"))));
        Assert.That(context.ProzBevAktiv, Has.Exactly(1).Matches(new Predicate<ProzBevAktiv>(p => p.PB.Equals("Proz.-Bev.: Rechtsanwälte Tholen pp."))));
    }

    [Test]
    public void CheckParteienPassiv()
    { 
        AddExamples();
        CheckParteienPassivA();
    }
    public void CheckParteienPassivA()
    {
        ParteienPassiv[] passivPar = context.ParteienPassiv.ToArray();
        Assert.That(passivPar.Count(), Is.EqualTo(9));
        Assert.That(passivPar[0].Partei, Is.EqualTo("das Land Rheinland-Pfalz vertreten durch den Landtag Rheinland-Pfalz"));
        Assert.That(passivPar[1].Partei, Is.EqualTo("Ortsgemeinde Rengsdorf vertreten durch den Bürgermeister der Verbandsgemeinde Rengsdorf-Waldbreitbach"));
        Assert.That(passivPar[2].Partei, Is.EqualTo("Verbandsgemeinde Bad Ems vertreten durch den Bürgermeister"));
        Assert.That(passivPar[3].Partei, Is.EqualTo("Landrat des Landkreis Dr. Micheal Müller"));
        Assert.That(context.ParteienPassiv, Has.Exactly(1).Matches(new Predicate<ParteienPassiv>(p => p.Partei == "POS Polsterservice GmbH")));
        Assert.That(context.ParteienPassiv, Has.Exactly(1).Matches(new Predicate<ParteienPassiv>(p => p.Partei == "Ulrich Finzler")));
    }

    [Test]
    public void CheckProzBevPassiv()
    { 
        AddExamples();
        CheckProzBevPassivA();
    }
    public void CheckProzBevPassivA()
    {
        CollectionAssert.IsNotEmpty(context.ProzBevPassiv);
        ProzBevPassiv[] passivProzBez = context.ProzBevPassiv.ToArray();
        Assert.That(passivProzBez, Has.Exactly(8).Items);
        Assert.That(passivProzBez[0].PB, Is.EqualTo("Proz.-Bev.: Rechtsanwälte Jeromin & Kerkmann"));
        Assert.That(passivProzBez[1].PB, Is.EqualTo("Proz.-Bev.: Rechtsanwälte Dr. Martini, Mogg, Vogt PartGmbB"));
        Assert.That(passivProzBez[2].PB, Is.EqualTo("Proz.-Bev.: Rechtsanwälte Dr. Martini2, Mogg, Vogt PartGmbB"));
        Assert.That(context.ProzBevPassiv, Has.Exactly(1).Matches(new Predicate<ProzBevPassiv>(p => p.PB == "Proz.-Bev.: Rechtsanwälte Braun Baulig Berninger")));
        //Assert.That(passivProzBez[3].PB, Is.EqualTo("Proz.-Bev.: Rechtsanwälte Braun Baulig Berninger"));
    }

    [Test]
    public void CheckParteienBeigeladen()
    { 
        AddExamples();
        CheckParteienBeigeladenA();
    }
    public void CheckParteienBeigeladenA()
    {
        CollectionAssert.IsNotEmpty(context.ParteienBeigeladen);
        ParteienBeigeladen[] beiPar = context.ParteienBeigeladen.ToArray();
        Assert.That(beiPar.Count(), Is.EqualTo(3));

        //Assert.That(beiPar[0].Partei, Is.EqualTo("1. ABCERT AG"));
        Assert.That(context.ParteienBeigeladen, Has.Exactly(1).Matches(new Predicate<ParteienBeigeladen>(p => p.Partei == "1. ABCERT AG")));
        Assert.That(beiPar[1].Partei, Is.EqualTo("2. Helmut Grün"));
        Assert.That(context.ParteienBeigeladen, Has.Exactly(1).Matches(new Predicate<ParteienBeigeladen>(p => p.Partei == "3. Erika Grün")));
        //Assert.That(beiPar[2].Partei, Is.EqualTo("3. Erika Grün"));
    }

    [Test]
    public void CheckProzBeigeladen()
    {
        AddExamples();
        CheckProzBeigeladenA();
    }

    public void CheckProzBeigeladenA()
    {
        ProzBevBeigeladen[] beiProzBez = context.ProzBevBeigeladen.ToArray();
        Assert.That(beiProzBez.Count(), Is.EqualTo(1));
        Assert.That(beiProzBez[0].PB, Is.EqualTo("Proz.-Bev.:\tzu 3: Rechtsanwälte Jeromin & Kerkmann"));
    }

    [Test]
    public void CheckParteienSV()
    {
        AddExamples();
        CheckParteienSVA();
    }
    public void CheckParteienSVA()
    {
        CollectionAssert.IsNotEmpty(context.ParteienSV);

        ParteienSV[] svPar = context.ParteienSV.ToArray();
        Assert.That(svPar.Count(sv => sv.Partei.Equals("1. Edith Hof")), Is.EqualTo(1));
        Assert.That(svPar.Count(sv => sv.Partei.Equals("2. Willibald Hof")), Is.EqualTo(1));

        //Assert.That(svPar[0].Partei, Is.EqualTo("1. Edith Hof"));
        //Assert.That(svPar[1].Partei, Is.EqualTo("2. Willibald Hof"));
    }

    [Test]
    public void CheckZeugen()
    {
        AddExamples();
        CheckZeugenA();
    }
    public void CheckZeugenA() {
        CollectionAssert.IsNotEmpty(context.ParteienZeugen);
        ParteienZeugen[] zeugenPar = context.ParteienZeugen.ToArray();
        Assert.That(zeugenPar.Count(), Is.EqualTo(2));
        Assert.That(zeugenPar[0].Partei, Is.EqualTo("1. Hessel GmbH vertreten durch die Geschäftsführer"));
        Assert.That(zeugenPar[1].Partei, Is.EqualTo("2. Astrid Hessel"));
    }

    private string prodVal(string val)
    {
        return val.TrimEnd().Replace("\r\n", "\n").Replace("\r", "\n");
    }

    private List<string> CreateCliActions(bool delete, string connectionString, string[] xmlFiles = null, bool Update = false)
    {
        var args = new List<string>();
        string AddOrUpdateCommand = TestSets.COMMAND_ADD;
        if (Update)
            AddOrUpdateCommand = TestSets.COMMAND_UPDATE;
        args.Add(TestSets.COMMAND_CONNECTION);
        args.Add(connectionString);
        if (delete)
            args.Add(TestSets.COMMAND_DELETE);
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
            args.Add(TestSets.EXAMPLE_XML1);
            args.Add(AddOrUpdateCommand);
            args.Add(TestSets.EXAMPLE_XML2);
            args.Add(AddOrUpdateCommand);
            args.Add(TestSets.EXAMPLE_XML3);
            args.Add(AddOrUpdateCommand);
            args.Add(TestSets.EXAMPLE_XML4);
            args.Add(AddOrUpdateCommand);
            args.Add(TestSets.EXAMPLE_XML5);
        }

        var cliService = new Service.CLIService();
        ClassicAssert.IsInstanceOf<Service.CLIService>(cliService);

        //CLIActions cliActions = cliService.ParseCommandLineArguments(args.ToArray());
        //ClassicAssert.IsInstanceOf<CLIActions>(cliActions);

        return args;
    }
}