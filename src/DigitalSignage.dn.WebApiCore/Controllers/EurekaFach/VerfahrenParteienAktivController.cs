// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/parteienaktiv")]
    public class VerfahrenParteienAktivController : Controller
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParteienAktiv>>> GetAllParteienAktivByVerfahren(Int64 verfid)
        {
            var verfahren = await context.Verfahren.FindAsync(verfid);

            if (verfahren == null)
            {
                return NotFound();
            }

            try
            {
                await context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.ParteienAktiv);
        }

        [Route("{id}", Name = "GetParteienAktivById")]
        [HttpGet]
        public async Task<ActionResult<ParteienAktiv>> GetParteienAktiv(Int64 verfid, int id)
        {
            var parteienAktiv = await context.ParteienAktiv.FindAsync(id);

            if (parteienAktiv == null)
            {
                return NotFound();
            }

            return Ok(parteienAktiv);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> PutParteienAktiv(Int64 verfid, int id, ParteienAktiv parteienAktiv)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != parteienAktiv.ParteiId)
            {
                return BadRequest();
            }

            try
            {
                context.Entry(parteienAktiv).State = EntityState.Modified;
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
        public async Task<ActionResult<ParteienAktiv>> PostParteienAktiv(Int64 verfid, ParteienAktiv parteienAktiv)
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
                await context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
                verfahren.ParteienAktiv.Add(parteienAktiv);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetParteienAktivById", new { id = parteienAktiv.ParteiId }, parteienAktiv);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<ParteienAktiv>> DeleteParteienAktiv(Int64 verfid, int id)
        {
            var parteienAktiv = await context.ParteienAktiv.FindAsync(id);

            if (parteienAktiv == null)
            {
                return NotFound();
            }

            try
            {
                context.ParteienAktiv.Remove(parteienAktiv);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(parteienAktiv);
        }
    }
}