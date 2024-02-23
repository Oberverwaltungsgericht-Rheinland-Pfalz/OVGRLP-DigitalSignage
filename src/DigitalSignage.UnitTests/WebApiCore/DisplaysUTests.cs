global using System;
global using Shouldly;
global using System.Linq;
using System.Text;
using FluentAssertions;
using DigitalSignage.Data;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using DigitalSignage.WebApi.Services;
using DigitalSignage.WebApi.Controllers.Settings;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.Infrastructure.Models.EurekaFach;

namespace DigitalSignage.UnitTests.WebApiCore;

public class DisplaysUTests
{
    DisplaysController controller;
    DigitalSignageDbContext context;
    DisplayManagementService disManagementService;
    string displayName = "myNewDisplay";
    Display firstDisplay;

    [SetUp]
    public void SetUp()
    {
        context = new DigitalSignageDbContext(_contextOpitons);
        disManagementService = new DisplayManagementService();
        controller = new DisplaysController(context, disManagementService);
    }
    [TearDown]
    public void TearDown()
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }

    [Test]
    public void GetAllDisplays()
    {
        AddSamples();

        var response = controller.GetAllDisplays();
        response.ShouldNotBeEmpty();
        response.Count().ShouldBe(1);
        response.First().Name.ShouldBeEquivalentTo(displayName);
    }

    [Test]
    public void GetAllDisplaysEx()
    {
        AddSamples();

        var response = controller.GetAllDisplaysEx();
        response.ShouldNotBeEmpty();
        response.Count().ShouldBe(1);
        response.First().Name.ShouldBeEquivalentTo(displayName);
    }

    [Test]
    public async Task GetAllTermine()
    {
        AddSamples();

        var response = await controller.GetAllTermine(displayName);
        response.ShouldNotBeNull();
        //response.ShouldBeOfType<ActionResult>();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = response.Result as OkObjectResult;

        result.ShouldNotBeNull();
        result.Value.ShouldNotBeNull();
        var resultList = result.Value as IEnumerable<VerfahrenDto>;
        resultList.ShouldNotBeEmpty();
        resultList.Count().ShouldBe(1);
    }

    [Test]
    public async Task GetActiveNodes()
    {
        AddSamples();
        var response = await controller.GetActiveNotes(displayName, DateTime.Now);

        response.ShouldNotBeNull();
        //response.ShouldBeOfType<ActionResult>();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = response.Result as OkObjectResult;
        
        result.ShouldNotBeNull();
        result.Value.ShouldNotBeNull();
        var resultList = result.Value as IEnumerable<Note>;
        resultList.ShouldNotBeEmpty();
        resultList.Count().ShouldBe(1);
    }

    [Test]
    public async Task GetStatusForDisplay()
    {
        AddSamples();
        var response = await controller.GetStatusForDisplay(displayName);

        response.ShouldNotBeNull();
        response.Result.ShouldBeOfType<OkObjectResult>();

        var result = response.Result as OkObjectResult;

        result.ShouldNotBeNull();
        result.Value.ShouldNotBeNull();
        DisplayStatus resultValue = (DisplayStatus) result.Value;
        resultValue.ShouldBe(DisplayStatus.Unknown);
    }

    protected void AddSamples()
    {
        firstDisplay = new Display() { Name = displayName, Id = 123, Dummy = false };
        var no = new Note() { Name = "note", Id = 456 };
        var na = new NoteAssignment();
        na.Note = no;
        na.Display = firstDisplay;
        na.DisplayId = firstDisplay.Id;
        na.NoteId = no.Id;
        na.Start = DateTime.Now.AddMinutes(-1);
        na.End = DateTime.Now.AddMinutes(2);
        firstDisplay.NotesAssignments = new List<NoteAssignment>() { na };
        context.Displays.Add(firstDisplay);

        var verf = new Verfahren() { Art ="art", Az="az", Gegenstand="Gegenstand", Oeffentlich = "public", Sitzungssaal = "", UhrzeitAktuell = DateTime.Now.ToString(), UhrzeitPlan = DateTime.Now.ToString() };
        context.Verfahren.Add(verf);
        var st = new Stammdaten() { Datum = DateTime.Now.ToString(), Gerichtsname = "unit-test-gericht", Verfahren = new List<Verfahren> { verf } };
        context.Stammdaten.Add(st);

        context.SaveChanges();
    }

    protected DbContextOptions<DigitalSignageDbContext> _contextOpitons
    {
        get
        {
            return new DbContextOptionsBuilder<DigitalSignageDbContext>()
                .UseInMemoryDatabase("test-db")
                .Options;
        }
    }
}
