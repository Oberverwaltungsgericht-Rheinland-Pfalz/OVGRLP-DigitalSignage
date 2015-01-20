using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DigitalSignage.WebApi.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [RoutePrefix("daten/verfahren/{verfid}/prozbevbeigeladen")]
    public class VerfahrenProzBevBeigeladenController : ApiController
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ProzBevBeigeladen>))]
        public async Task<IHttpActionResult> GetAllProzBevBeigeladenByVerfahren(int verfid)
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
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(verfahren.ProzBevBeigeladen);
        }

        [Route("{id}", Name = "GetProzBevBeigeladenById")]
        [HttpGet]
        [ResponseType(typeof(ProzBevBeigeladen))]
        public async Task<IHttpActionResult> GetProzBevBeigeladen(int verfid, int id)
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
        public async Task<IHttpActionResult> PutProzBevBeigeladen(int verfid, int id, ProzBevBeigeladen prozBevBeigeladen)
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
        public async Task<IHttpActionResult> PostProzBevBeigeladen(int verfid, ProzBevBeigeladen prozBevBeigeladen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var verfahren = await context.Verfahren.FindAsync(verfid);
            
            if(verfahren == null)
            {
                return NotFound();
            }
            
            try
            {
                await context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
                verfahren.ProzBevBeigeladen.Add(prozBevBeigeladen);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return CreatedAtRoute("GetProzBevBeigeladenById", new { id = prozBevBeigeladen.ProzBevId }, prozBevBeigeladen);
        }

        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(ProzBevBeigeladen))]
        public async Task<IHttpActionResult> DeleteProzBevBeigeladen(int verfid, int id)
        {
            var prozBevBeigeladen = await context.ProzBevBeigeladen.FindAsync(id);
            
            if(prozBevBeigeladen == null)
            {
                return NotFound();
            }

            try
            {
                context.ProzBevBeigeladen.Remove(prozBevBeigeladen);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
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