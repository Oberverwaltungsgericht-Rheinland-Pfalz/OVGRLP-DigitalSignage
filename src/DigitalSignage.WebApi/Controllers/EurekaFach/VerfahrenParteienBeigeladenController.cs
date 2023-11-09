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
  [RoutePrefix("daten/verfahren/{verfid}/parteienbeigeladen")]
  public class VerfahrenParteienBeigeladenController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ParteienBeigeladen>))]
    public async Task<IHttpActionResult> GetAllParteienBeigeladenByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ParteienBeigeladen).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ParteienBeigeladen);
    }

    [Route("{id}", Name = "GetParteienBeigeladenById")]
    [HttpGet]
    [ResponseType(typeof(ParteienBeigeladen))]
    public async Task<IHttpActionResult> GetParteienBeigeladen(Int64 verfid, int id)
    {
      var parteienBeigeladen = await context.ParteienBeigeladen.FindAsync(id);

      if (parteienBeigeladen == null)
      {
        return NotFound();
      }

      return Ok(parteienBeigeladen);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutParteienBeigeladen(Int64 verfid, int id, ParteienBeigeladen parteienBeigeladen)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != parteienBeigeladen.ParteiId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(parteienBeigeladen).State = EntityState.Modified;
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
    [ResponseType(typeof(ParteienBeigeladen))]
    public async Task<IHttpActionResult> PostParteienBeigeladen(Int64 verfid, ParteienBeigeladen parteienBeigeladen)
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
        await context.Entry(verfahren).Collection(v => v.ParteienBeigeladen).LoadAsync();
        verfahren.ParteienBeigeladen.Add(parteienBeigeladen);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetParteienBeigeladenById", new { id = parteienBeigeladen.ParteiId }, parteienBeigeladen);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ParteienBeigeladen))]
    public async Task<IHttpActionResult> DeleteParteienBeigeladen(Int64 verfid, int id)
    {
      var parteienBeigeladen = await context.ParteienBeigeladen.FindAsync(id);

      if (parteienBeigeladen == null)
      {
        return NotFound();
      }

      try
      {
        context.ParteienBeigeladen.Remove(parteienBeigeladen);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(parteienBeigeladen);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}