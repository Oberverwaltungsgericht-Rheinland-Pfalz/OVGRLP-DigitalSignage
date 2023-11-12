// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/prozbevaktiv")]
    public class VerfahrenProzBevAktivController : Controller
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProzBevAktiv>>> GetAllProzBevAktivByVerfahren(Int64 verfid)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.ProzBevAktiv);
        }

        [Route("{id}", Name = "GetProzBevAktivById")]
        [HttpGet]
        public async Task<ActionResult<ProzBevAktiv>> GetProzBevAktiv(Int64 verfid, int id)
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
        public async Task<ActionResult> PutProzBevAktiv(Int64 verfid, int id, ProzBevAktiv prozBevAktiv)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return NoContent();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<ProzBevAktiv>> PostProzBevAktiv(Int64 verfid, ProzBevAktiv prozBevAktiv)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetProzBevAktivById", new { id = prozBevAktiv.ProzBevId }, prozBevAktiv);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<ProzBevAktiv>> DeleteProzBevAktiv(Int64 verfid, int id)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(prozBevAktiv);
        }
    }
}