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
    [RoutePrefix("daten/stammdaten")]
    public class StammdatenController : ApiController
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public IEnumerable<Stammdaten> GetAllStammdaten()
        {
            return context.Stammdaten;
        }

        [Route("{id}", Name = "GetStammdatenById")]
        [HttpGet]
        [ResponseType(typeof(Stammdaten))]
        public async Task<IHttpActionResult> GetStammdaten(int id)
        {
            var stammdaten = await context.Stammdaten.FindAsync(id);

            if (stammdaten == null)
            {
                return NotFound();
            }

            return Ok(stammdaten);
        }

        [Route("{id}")]
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutStammdaten(int id, Stammdaten stammdaten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stammdaten.StammdatenId)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(stammdaten).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return InternalServerError(ex);
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        [Route("")]
        [HttpPost]
        [ResponseType(typeof(Stammdaten))]
        public async Task<IHttpActionResult> PostStammdaten(Stammdaten stammdaten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                context.Stammdaten.Add(stammdaten);
                await context.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex);
                return InternalServerError(ex);
            }

            return CreatedAtRoute("GetStammdatenById", new { id = stammdaten.StammdatenId }, stammdaten);
        }

        [Route("{id}")]
        [HttpDelete]
        [ResponseType(typeof(Stammdaten))]
        public async Task<IHttpActionResult> DeleteStammdaten(int id)
        {
            var stammdaten = await context.Stammdaten.FindAsync(id);
            
            if(stammdaten == null)
            {
                return NotFound();
            }

            context.Stammdaten.Remove(stammdaten);
            await context.SaveChangesAsync();

            return Ok(stammdaten);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                context.Dispose();

            base.Dispose(disposing);
        }
    }
}