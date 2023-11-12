// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/parteienzeugen")]
    public class VerfahrenParteienZeugenController : Controller
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParteienZeugen>>> GetAllParteienZeugenByVerfahren(Int64 verfid)
        {
            var verfahren = await context.Verfahren.FindAsync(verfid);

            if (verfahren == null)
            {
                return NotFound();
            }

            try
            {
                await context.Entry(verfahren).Collection(v => v.ParteienZeugen).LoadAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.ParteienZeugen);
        }

        [Route("{id}", Name = "GetParteienZeugenById")]
        [HttpGet]
        public async Task<ActionResult<ParteienZeugen>> GetParteienZeugen(Int64 verfid, int id)
        {
            var parteienZeugen = await context.ParteienZeugen.FindAsync(id);

            if (parteienZeugen == null)
            {
                return NotFound();
            }

            return Ok(parteienZeugen);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> PutParteienZeugen(Int64 verfid, int id, ParteienZeugen parteienZeugen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parteienZeugen.ParteiId)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(parteienZeugen).State = EntityState.Modified;
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
        public async Task<ActionResult<ParteienZeugen>> PostParteienZeugen(Int64 verfid, ParteienZeugen parteienZeugen)
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
                await context.Entry(verfahren).Collection(v => v.ParteienZeugen).LoadAsync();
                verfahren.ParteienZeugen.Add(parteienZeugen);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetParteienZeugenById", new { id = parteienZeugen.ParteiId }, parteienZeugen);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<ParteienZeugen>> DeleteParteienZeugen(Int64 verfid, int id)
        {
            var parteienZeugen = await context.ParteienZeugen.FindAsync(id);

            if (parteienZeugen == null)
            {
                return NotFound();
            }

            try
            {
                context.ParteienZeugen.Remove(parteienZeugen);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(parteienZeugen);
        }
    }
}