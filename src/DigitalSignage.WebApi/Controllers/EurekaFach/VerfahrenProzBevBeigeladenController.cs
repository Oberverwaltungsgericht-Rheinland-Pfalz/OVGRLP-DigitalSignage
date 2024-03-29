﻿// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
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
  [RoutePrefix("daten/verfahren/{verfid}/prozbevbeigeladen")]
  public class VerfahrenProzBevBeigeladenController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ProzBevBeigeladen>))]
    public async Task<IHttpActionResult> GetAllProzBevBeigeladenByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ProzBevBeigeladen);
    }

    [Route("{id}", Name = "GetProzBevBeigeladenById")]
    [HttpGet]
    [ResponseType(typeof(ProzBevBeigeladen))]
    public async Task<IHttpActionResult> GetProzBevBeigeladen(Int64 verfid, int id)
    {
      var prozBevBeigeladen = await context.ProzBevBeigeladen.FindAsync(id);

      if (prozBevBeigeladen == null)
      {
        return NotFound();
      }

      return Ok(prozBevBeigeladen);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutProzBevBeigeladen(Int64 verfid, int id, ProzBevBeigeladen prozBevBeigeladen)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != prozBevBeigeladen.ProzBevId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(prozBevBeigeladen).State = EntityState.Modified;
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
    [ResponseType(typeof(ProzBevBeigeladen))]
    public async Task<IHttpActionResult> PostProzBevBeigeladen(Int64 verfid, ProzBevBeigeladen prozBevBeigeladen)
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
        await context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
        verfahren.ProzBevBeigeladen.Add(prozBevBeigeladen);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetProzBevBeigeladenById", new { id = prozBevBeigeladen.ProzBevId }, prozBevBeigeladen);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ProzBevBeigeladen))]
    public async Task<IHttpActionResult> DeleteProzBevBeigeladen(Int64 verfid, int id)
    {
      var prozBevBeigeladen = await context.ProzBevBeigeladen.FindAsync(id);

      if (prozBevBeigeladen == null)
      {
        return NotFound();
      }

      try
      {
        context.ProzBevBeigeladen.Remove(prozBevBeigeladen);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(prozBevBeigeladen);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}