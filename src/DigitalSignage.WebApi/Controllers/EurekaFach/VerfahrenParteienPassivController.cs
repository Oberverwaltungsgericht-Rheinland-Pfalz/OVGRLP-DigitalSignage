using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Data.Entity;
using System.Diagnostics;
using System.Net;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [RoutePrefix("daten/verfahren/{verfid}/parteienpassiv")]
    public class VerfahrenParteienPassivController : ApiController
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        [ResponseType(typeof(IEnumerable<ParteienPassiv>))]
        public async Task<IHttpActionResult> GetAllParteienPassivByVerfahren(int verfid)
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
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(verfahren.ParteienPassiv);
        }

        [Route("{id}", Name = "GetParteienPassivById")]
        [HttpGet]
        [ResponseType(typeof(ParteienPassiv))]
        public async Task<IHttpActionResult> GetParteienPassiv(int verfid, int id)
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
        public async Task<IHttpActionResult> PutParteienPassiv(int verfid, int id, ParteienPassiv parteienPassiv)
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
        public async Task<IHttpActionResult> PostParteienPassiv(int verfid, ParteienPassiv parteienPassiv)
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
                await context.Entry(verfahren).Collection(v => v.ParteienPassiv).LoadAsync();
                verfahren.ParteienPassiv.Add(parteienPassiv);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return InternalServerError(ex);
            }

            return CreatedAtRoute("GetParteienPassivById", new { id = parteienPassiv.ParteiId }, parteienPassiv);
        }

        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(ParteienPassiv))]
        public async Task<IHttpActionResult> DeleteParteienPassiv(int verfid, int id)
        {
            var parteienPassiv = await context.ParteienPassiv.FindAsync(id);
            
            if(parteienPassiv == null)
            {
                return NotFound();
            }

            try
            {
                context.ParteienPassiv.Remove(parteienPassiv);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
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