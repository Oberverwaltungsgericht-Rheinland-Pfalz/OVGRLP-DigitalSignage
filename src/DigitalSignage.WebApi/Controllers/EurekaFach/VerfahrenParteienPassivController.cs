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
  [RoutePrefix("daten/verfahren/{verfid}/parteienpassiv")]
  public class VerfahrenParteienPassivController : ApiController
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    [ResponseType(typeof(IEnumerable<ParteienPassiv>))]
    public async Task<IHttpActionResult> GetAllParteienPassivByVerfahren(Int64 verfid)
    {
      var verfahren = await context.Verfahren.FindAsync(verfid);

      if (verfahren == null)
      {
        return NotFound();
      }

      try
      {
        await context.Entry(verfahren).Collection(v => v.ParteienPassiv).LoadAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(verfahren.ParteienPassiv);
    }

    [Route("{id}", Name = "GetParteienPassivById")]
    [HttpGet]
    [ResponseType(typeof(ParteienPassiv))]
    public async Task<IHttpActionResult> GetParteienPassiv(Int64 verfid, int id)
    {
      var parteienPassiv = await context.ParteienPassiv.FindAsync(id);

      if (parteienPassiv == null)
      {
        return NotFound();
      }

      return Ok(parteienPassiv);
    }

    [Route("{id}")]
    [HttpPut]
    [ResponseType(typeof(void))]
    public async Task<IHttpActionResult> PutParteienPassiv(Int64 verfid, int id, ParteienPassiv parteienPassiv)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      if (id != parteienPassiv.ParteiId)
      {
        return BadRequest();
      }

      try
      {
        context.Entry(parteienPassiv).State = EntityState.Modified;
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
    [ResponseType(typeof(ParteienPassiv))]
    public async Task<IHttpActionResult> PostParteienPassiv(Int64 verfid, ParteienPassiv parteienPassiv)
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
        await context.Entry(verfahren).Collection(v => v.ParteienPassiv).LoadAsync();
        verfahren.ParteienPassiv.Add(parteienPassiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return CreatedAtRoute("GetParteienPassivById", new { id = parteienPassiv.ParteiId }, parteienPassiv);
    }

    [Route("{id}")]
    [HttpDelete]
    [ResponseType(typeof(ParteienPassiv))]
    public async Task<IHttpActionResult> DeleteParteienPassiv(Int64 verfid, int id)
    {
      var parteienPassiv = await context.ParteienPassiv.FindAsync(id);

      if (parteienPassiv == null)
      {
        return NotFound();
      }

      try
      {
        context.ParteienPassiv.Remove(parteienPassiv);
        await context.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        return InternalServerError(ex);
      }

      return Ok(parteienPassiv);
    }

    protected override void Dispose(bool disposing)
    {
      if (disposing)
        context.Dispose();

      base.Dispose(disposing);
    }
  }
}