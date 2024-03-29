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
  [RoutePrefix("daten/verfahren/{verfid}/parteienaktiv")]
  public class VerfahrenParteienAktivController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ParteienAktiv>))]
    public async Task<IHttpActionResult> GetAllParteienAktivByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ParteienAktiv);
    }

    [Route("{id}", Name = "GetParteienAktivById")]
    [HttpGet]
    [ResponseType(typeof(ParteienAktiv))]
    public async Task<IHttpActionResult> GetParteienAktiv(Int64 verfid, int id)
    {
      var parteienAktiv = await context.ParteienAktiv.FindAsync(id);

      if (parteienAktiv == null)
      {
        return NotFound();
      }

      return Ok(parteienAktiv);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutParteienAktiv(Int64 verfid, int id, ParteienAktiv parteienAktiv)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != parteienAktiv.ParteiId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(parteienAktiv).State = EntityState.Modified;
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
    [ResponseType(typeof(ParteienAktiv))]
    public async Task<IHttpActionResult> PostParteienAktiv(Int64 verfid, ParteienAktiv parteienAktiv)
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
        await context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
        verfahren.ParteienAktiv.Add(parteienAktiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetParteienAktivById", new { id = parteienAktiv.ParteiId }, parteienAktiv);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ParteienAktiv))]
    public async Task<IHttpActionResult> DeleteParteienAktiv(Int64 verfid, int id)
    {
      var parteienAktiv = await context.ParteienAktiv.FindAsync(id);

      if (parteienAktiv == null)
      {
        return NotFound();
      }

      try
      {
        context.ParteienAktiv.Remove(parteienAktiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(parteienAktiv);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}