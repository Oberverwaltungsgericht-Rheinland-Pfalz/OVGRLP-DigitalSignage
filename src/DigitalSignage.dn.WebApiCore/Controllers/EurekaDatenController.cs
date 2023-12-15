// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Breeze.AspNetCore;
using Breeze.Persistence;
using DigitalSignage.dn.WebApiCore.Services;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace DigitalSignage.WebApi.Controllers;

[Authorize]
[BreezeQueryFilter]
[Route("breeze/[controller]/[action]")]
public class EurekaDatenController : ControllerBase
{
    private readonly DigitalSignagePersistenceManager _context;
    private readonly DisplayManagementService _displayManagementService;

    public EurekaDatenController(DigitalSignagePersistenceManager digitalSignagePersistenceManager, DisplayManagementService displayManagementService)
    {
        _context = digitalSignagePersistenceManager;
        _displayManagementService = displayManagementService;
    }



    // ~/breeze/EurekaDaten/Metadata
    [HttpGet]
    public string Metadata()
    {
        return _context.Metadata();
    }

    // ~/breeze/EurekaDaten/Verfahren
    [HttpGet]
    public IQueryable<Verfahren> Verfahren()
    {
        return _context.Context.Verfahren;
    }

    // ~/breeze/EurekaDaten/VerfahrenList
    [HttpGet]
    public IQueryable<object> VerfahrenList()
    {
        var query = from v in _context.Context.Verfahren
                      .Include("Stammdaten")
                      .Include("ParteienAktiv")
                      .Include("ParteienPassiv")
                    select new
                    {
                        VerfahrensId = v.VerfahrensId,
                        Az = v.Az,
                        Status = v.Status,
                        UhrzeitPlan = v.UhrzeitPlan,
                        UhrzeitAktuell = v.UhrzeitAktuell,
                        Gericht = v.Stammdaten.Gerichtsname,
                        Datum = v.Stammdaten.Datum,
                        Sitzungssaal = v.Sitzungssaal,
                        ParteienAktiv = v.ParteienAktiv,
                        ParteienPassiv = v.ParteienPassiv
                    };

        return query.AsQueryable();
    }

    // ~/breeze/EurekaDaten/Displays
    [HttpGet]
    public IQueryable<object> Displays()
    {
        return _context.Context.Displays.Where(d => d.Dummy == false);
    }

    // ~/breeze/EurekaDaten/Notes
    [HttpGet]
    public IQueryable<Note> Notes()
    {
        return _context.Context.Notes;
    }

    // ~/breeze/EurekaDaten/DisplayStatus
    [HttpGet]
    [Route("Display/{id}/status")]
    public async Task<ActionResult> DisplayStatus(int id)
    {
        var display = await _context.Context.Displays.FindAsync(id);

        if (display == null)
            return NotFound();

        return Ok((int)_displayManagementService.GetDisplayStatus(display));
    }

    [Route("Display/{id}/poweron")]
    [HttpGet]
    public async Task<ActionResult> DisplayPowerOn(int id)
    {
        var display = await _context.Context.Displays.FindAsync(id);

        if (display == null)
            return NotFound();

        try
        {
            _displayManagementService.StartDisplay(display);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok();
    }

    // ~/breeze/EurekaDaten/SaveChanges
    [HttpPost]
    public SaveResult SaveChanges(JObject saveBundle)
    {
        return _context.SaveChanges(saveBundle);
    }
}