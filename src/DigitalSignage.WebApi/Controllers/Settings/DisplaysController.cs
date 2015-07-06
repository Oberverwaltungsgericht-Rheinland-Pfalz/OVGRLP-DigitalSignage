﻿using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Controllers.EurekaFach;
using DigitalSignage.WebApi.Data;
using DigitalSignage.WebApi.Services;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace DigitalSignage.WebApi.Controllers.Settings
{
  [RoutePrefix("settings/displays")]
  public class DisplaysController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();
    private readonly DisplayManagementService displayManagementService = new DisplayManagementService();

    [Route("")]
    [HttpGet]
    public IEnumerable<Display> GetAllDisplays()
    {
      return context.Displays;
    }

    [Route("{id}", Name = "GetDisplayById")]
    [HttpGet]
    [ResponseType(typeof(Display))]
    public async Task<IHttpActionResult> GetDisplay(int id)
    {
      var display = await context.Displays.FindAsync(id);

      if (display == null)
        return NotFound();

      return Ok(display);
    }

    [Route("{id}/termine")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<VerfahrenDto>))]
    public async Task<IHttpActionResult> GetAllTermine(int id)
    {
      var display = await context.Displays.FindAsync(id);

      if (display == null)
        return NotFound();

      List<VerfahrenDto> dtos = new List<VerfahrenDto>();

      context.Verfahren
          .SqlQuery("SELECT * FROM Verfahren" + (!String.IsNullOrEmpty(display.Filter) ? " WHERE " + display.Filter : ""))
          .ToList()
          .ForEach(v =>
              {
                context.Entry(v).Reference("Stammdaten").Load();
                context.Entry(v).Collection("ParteienAktiv").Load();
                context.Entry(v).Collection("ParteienPassiv").Load();
                context.Entry(v).Collection("Besetzung").Load();
                context.Entry(v).Collection("ProzBevAktiv").Load();
                context.Entry(v).Collection("ProzBevPassiv").Load();
                context.Entry(v).Collection("ParteienBeigeladen").Load();
                context.Entry(v).Collection("ProzBevBeigeladen").Load();
                context.Entry(v).Collection("ParteienZeugen").Load();
                context.Entry(v).Collection("ParteienSV").Load();
                dtos.Add(VerfahrenController.GetDtoFromVerfahren(v));
              }
      );

      return Ok(dtos);
    }

    [Route("{id}/status")]
    [HttpGet]
    [ResponseType(typeof(DisplayStatus))]
    public async Task<IHttpActionResult> GetStatusForDisplay(int id)
    {
      var display = await context.Displays.FindAsync(id);

      if (display == null)
        return NotFound();

      return Ok(displayManagementService.GetDisplayStatus(display));
    }

    [Route("{id}/start")]
    [HttpGet]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> StartDisplay(int id)
    {
      var display = await context.Displays.FindAsync(id);

      if (display == null)
        return NotFound();

      try
      {
        displayManagementService.StartDisplay(display);
      }
      catch
      {
        return InternalServerError();
      }

      return Ok();
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutDisplay(int id, Display display)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != display.Id)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(display).State = EntityState.Modified;
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return InternalServerError(ex);
      }

      return StatusCode(HttpStatusCode.NoContent);
    }

    [Route("")]
    [HttpPost]
    [ResponseType(typeof(Display))]
    public async Task<IHttpActionResult> PostDisplay(Display display)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        context.Displays.Add(display);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        Debug.WriteLine(ex);
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetDisplayById", new { id = display.Id }, display);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(Display))]
    public async Task<IHttpActionResult> DeleteDisplay(int id)
    {
      var display = await context.Displays.FindAsync(id);

      if (display == null)
      {
        return NotFound();
      }

      context.Displays.Remove(display);
      await context.SaveChangesAsync();

      return Ok(display);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}