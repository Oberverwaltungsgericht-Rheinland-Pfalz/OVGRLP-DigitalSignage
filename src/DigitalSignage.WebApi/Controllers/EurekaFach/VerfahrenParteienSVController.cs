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
  [RoutePrefix("daten/verfahren/{verfid}/parteiensv")]
  public class VerfahrenParteienSVController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ParteienSV>))]
    public async Task<IHttpActionResult> GetAllParteienSVByVerfahren(int verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ParteienSV).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ParteienSV);
    }

    [Route("{id}", Name = "GetParteienSVById")]
    [HttpGet]
    [ResponseType(typeof(ParteienSV))]
    public async Task<IHttpActionResult> GetParteienSV(int verfid, int id)
    {
      var parteienSV = await context.ParteienSV.FindAsync(id);

      if (parteienSV == null)
      {
        return NotFound();
      }

      return Ok(parteienSV);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutParteienSV(int verfid, int id, ParteienSV parteienSV)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != parteienSV.ParteiId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(parteienSV).State = EntityState.Modified;
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
    [ResponseType(typeof(ParteienSV))]
    public async Task<IHttpActionResult> PostParteienSV(int verfid, ParteienSV parteienSV)
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
        await context.Entry(verfahren).Collection(v => v.ParteienSV).LoadAsync();
        verfahren.ParteienSV.Add(parteienSV);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetParteienSVById", new { id = parteienSV.ParteiId }, parteienSV);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ParteienSV))]
    public async Task<IHttpActionResult> DeleteParteienSV(int verfid, int id)
    {
      var parteienSV = await context.ParteienSV.FindAsync(id);

      if (parteienSV == null)
      {
        return NotFound();
      }

      try
      {
        context.ParteienSV.Remove(parteienSV);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(parteienSV);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}