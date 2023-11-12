// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Breeze.AspNetCore;
using Breeze.Persistence;
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace DigitalSignage.WebApi.Controllers;

[BreezeQueryFilter]
[Route("breeze/[controller]/[action]")]
public class EurekaDatenController : Controller
{
private readonly EFContextProvider<DigitalSignageDbContext> contextProvider = new EFContextProvider<DigitalSignageDbContext>();
private readonly DisplayManagementService displayManagementService = new DisplayManagementService();

// ~/breeze/EurekaDaten/Metadata
[HttpGet]
public string Metadata()
{
  return contextProvider.Metadata();
}

// ~/breeze/EurekaDaten/Verfahren
[HttpGet]
public IQueryable<Verfahren> Verfahren()
{
  return contextProvider.Context.Verfahren;
}

// ~/breeze/EurekaDaten/VerfahrenList
[HttpGet]
public IQueryable<object> VerfahrenList()
{
  var query = from v in contextProvider.Context.Verfahren
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
  return contextProvider.Context.Displays.Where(d => d.Dummy == false);
}

// ~/breeze/EurekaDaten/Notes
[HttpGet]
public IQueryable<Note> Notes()
{
  return contextProvider.Context.Notes;
}

// ~/breeze/EurekaDaten/DisplayStatus
[HttpGet]
[Route("Display/{id}/status")]
public async Task<ActionResult> DisplayStatus(int id)
{
  var display = await contextProvider.Context.Displays.FindAsync(id);

  if (display == null)
    return NotFound();

  return Ok((int)displayManagementService.GetDisplayStatus(display));
}

[Route("Display/{id}/poweron")]
[HttpGet]
// [ResponseType(typeof(void))]
public async Task<ActionResult> DisplayPowerOn(int id)
{
  var display = await contextProvider.Context.Displays.FindAsync(id);

  if (display == null)
    return NotFound();

  try
  {
    displayManagementService.StartDisplay(display);
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
  return contextProvider.SaveChanges(saveBundle);
}
}