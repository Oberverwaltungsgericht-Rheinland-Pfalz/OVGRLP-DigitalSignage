// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
  [RoutePrefix("daten/verfahren/{verfid}/besetzung")]
  public class VerfahrenBesetzungController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<Besetzung>))]
    public async Task<IHttpActionResult> GetAllBesetzungByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.Besetzung).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.Besetzung);
    }

    [Route("{id}", Name = "GetBesetzungById")]
    [HttpGet]
    [ResponseType(typeof(Besetzung))]
    public async Task<IHttpActionResult> GetBesetzung(Int64 verfid, int id)
    {
      var besetzung = await context.Besetzung.FindAsync(id);

      if (besetzung == null)
      {
        return NotFound();
      }

      return Ok(besetzung);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutBesetzung(Int64 verfid, int id, Besetzung besetzung)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != besetzung.BesetzungsId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(besetzung).State = EntityState.Modified;
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return StatusCode(HttpStatusCode.NoContent);
    }

    [Route("")]
    [HttpPost]
    [ResponseType(typeof(Besetzung))]
    public async Task<IHttpActionResult> PostBesetzung(Int64 verfid, Besetzung besetzung)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.Besetzung).LoadAsync();
        verfahren.Besetzung.Add(besetzung);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetBesetzungById", new { id = besetzung.BesetzungsId }, besetzung);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(Besetzung))]
    public async Task<IHttpActionResult> DeleteBesetzung(Int64 verfid, int id)
    {
      var besetzung = await context.Besetzung.FindAsync(id);

      if (besetzung == null)
      {
        return NotFound();
      }

      try
      {
        context.Besetzung.Remove(besetzung);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(besetzung);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}