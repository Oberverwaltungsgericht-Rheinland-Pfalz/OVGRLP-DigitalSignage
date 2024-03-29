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
  [RoutePrefix("daten/verfahren/{verfid}/parteienzeugen")]
  public class VerfahrenParteienZeugenController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ParteienZeugen>))]
    public async Task<IHttpActionResult> GetAllParteienZeugenByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ParteienZeugen).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ParteienZeugen);
    }

    [Route("{id}", Name = "GetParteienZeugenById")]
    [HttpGet]
    [ResponseType(typeof(ParteienZeugen))]
    public async Task<IHttpActionResult> GetParteienZeugen(Int64 verfid, int id)
    {
      var parteienZeugen = await context.ParteienZeugen.FindAsync(id);

      if (parteienZeugen == null)
      {
        return NotFound();
      }

      return Ok(parteienZeugen);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutParteienZeugen(Int64 verfid, int id, ParteienZeugen parteienZeugen)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != parteienZeugen.ParteiId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(parteienZeugen).State = EntityState.Modified;
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
    [ResponseType(typeof(ParteienZeugen))]
    public async Task<IHttpActionResult> PostParteienZeugen(Int64 verfid, ParteienZeugen parteienZeugen)
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
        await context.Entry(verfahren).Collection(v => v.ParteienZeugen).LoadAsync();
        verfahren.ParteienZeugen.Add(parteienZeugen);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetParteienZeugenById", new { id = parteienZeugen.ParteiId }, parteienZeugen);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ParteienZeugen))]
    public async Task<IHttpActionResult> DeleteParteienZeugen(Int64 verfid, int id)
    {
      var parteienZeugen = await context.ParteienZeugen.FindAsync(id);

      if (parteienZeugen == null)
      {
        return NotFound();
      }

      try
      {
        context.ParteienZeugen.Remove(parteienZeugen);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(parteienZeugen);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}