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
  [RoutePrefix("daten/verfahren/{verfid}/prozbevaktiv")]
  public class VerfahrenProzBevAktivController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ProzBevAktiv>))]
    public async Task<IHttpActionResult> GetAllProzBevAktivByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ProzBevAktiv).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ProzBevAktiv);
    }

    [Route("{id}", Name = "GetProzBevAktivById")]
    [HttpGet]
    [ResponseType(typeof(ProzBevAktiv))]
    public async Task<IHttpActionResult> GetProzBevAktiv(Int64 verfid, int id)
    {
      var prozBevAktiv = await context.ProzBevAktiv.FindAsync(id);

      if (prozBevAktiv == null)
      {
        return NotFound();
      }

      return Ok(prozBevAktiv);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutProzBevAktiv(Int64 verfid, int id, ProzBevAktiv prozBevAktiv)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != prozBevAktiv.ProzBevId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(prozBevAktiv).State = EntityState.Modified;
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
    [ResponseType(typeof(ProzBevAktiv))]
    public async Task<IHttpActionResult> PostProzBevAktiv(Int64 verfid, ProzBevAktiv prozBevAktiv)
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
        await context.Entry(verfahren).Collection(v => v.ProzBevAktiv).LoadAsync();
        verfahren.ProzBevAktiv.Add(prozBevAktiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetProzBevAktivById", new { id = prozBevAktiv.ProzBevId }, prozBevAktiv);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ProzBevAktiv))]
    public async Task<IHttpActionResult> DeleteProzBevAktiv(Int64 verfid, int id)
    {
      var prozBevAktiv = await context.ProzBevAktiv.FindAsync(id);

      if (prozBevAktiv == null)
      {
        return NotFound();
      }

      try
      {
        context.ProzBevAktiv.Remove(prozBevAktiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(prozBevAktiv);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}