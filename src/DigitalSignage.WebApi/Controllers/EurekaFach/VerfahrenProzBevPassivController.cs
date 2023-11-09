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
  [RoutePrefix("daten/verfahren/{verfid}/prozbevpassiv")]
  public class VerfahrenProzBevPassivController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ProzBevPassiv>))]
    public async Task<IHttpActionResult> GetAllProzBevPassivByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ProzBevPassiv).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ProzBevPassiv);
    }

    [Route("{id}", Name = "GetProzBevPassivById")]
    [HttpGet]
    [ResponseType(typeof(ProzBevPassiv))]
    public async Task<IHttpActionResult> GetProzBevPassiv(Int64 verfid, int id)
    {
      var prozBevPassiv = await context.ProzBevPassiv.FindAsync(id);

      if (prozBevPassiv == null)
      {
        return NotFound();
      }

      return Ok(prozBevPassiv);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutProzBevPassiv(Int64 verfid, int id, ProzBevPassiv prozBevPassiv)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != prozBevPassiv.ProzBevId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(prozBevPassiv).State = EntityState.Modified;
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
    [ResponseType(typeof(ProzBevPassiv))]
    public async Task<IHttpActionResult> PostProzBevPassiv(Int64 verfid, ProzBevPassiv prozBevPassiv)
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
        await context.Entry(verfahren).Collection(v => v.ProzBevPassiv).LoadAsync();
        verfahren.ProzBevPassiv.Add(prozBevPassiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetProzBevPassivById", new { id = prozBevPassiv.ProzBevId }, prozBevPassiv);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ProzBevPassiv))]
    public async Task<IHttpActionResult> DeleteProzBevPassiv(Int64 verfid, int id)
    {
      var prozBevPassiv = await context.ProzBevPassiv.FindAsync(id);

      if (prozBevPassiv == null)
      {
        return NotFound();
      }

      try
      {
        context.ProzBevPassiv.Remove(prozBevPassiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(prozBevPassiv);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}