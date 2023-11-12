// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/besetzung")]
    public class VerfahrenBesetzungController : Controller
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Besetzung>>> GetAllBesetzungByVerfahren(Int64 verfid)
        {
            var verfahren = await context.Verfahren.FindAsync(verfid);

            if (verfahren == null)
            {
                return NotFound();
            }

            try
            {
                await context.Entry(verfahren).Collection(v => v.Besetzung).LoadAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.Besetzung);
        }

        [Route("{id}", Name = "GetBesetzungById")]
        [HttpGet]
        public async Task<ActionResult<Besetzung>> GetBesetzung(Int64 verfid, int id)
        {
            var besetzung = await context.Besetzung.FindAsync(id);

            if (besetzung == null)
            {
                return NotFound();
            }

            return Ok(besetzung);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> PutBesetzung(Int64 verfid, int id, Besetzung besetzung)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != besetzung.BesetzungsId)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(besetzung).State = EntityState.Modified;
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
        public async Task<ActionResult<Besetzung>> PostBesetzung(Int64 verfid, Besetzung besetzung)
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
                await context.Entry(verfahren).Collection(v => v.Besetzung).LoadAsync();
                verfahren.Besetzung.Add(besetzung);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetBesetzungById", new { id = besetzung.BesetzungsId }, besetzung);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<Besetzung>> DeleteBesetzung(Int64 verfid, int id)
        {
            var besetzung = await context.Besetzung.FindAsync(id);

            if (besetzung == null)
            {
                return NotFound();
            }

            try
            {
                context.Besetzung.Remove(besetzung);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(besetzung);
        }
    }
}