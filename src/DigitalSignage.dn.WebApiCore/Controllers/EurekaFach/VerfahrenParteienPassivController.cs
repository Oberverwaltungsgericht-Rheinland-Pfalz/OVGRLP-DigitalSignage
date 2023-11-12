// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/parteienpassiv")]
    public class VerfahrenParteienPassivController : Controller
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParteienPassiv>>> GetAllParteienPassivByVerfahren(Int64 verfid)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.ParteienPassiv);
        }

        [Route("{id}", Name = "GetParteienPassivById")]
        [HttpGet]
        public async Task<ActionResult<ParteienPassiv>> GetParteienPassiv(Int64 verfid, int id)
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
        public async Task<ActionResult> PutParteienPassiv(Int64 verfid, int id, ParteienPassiv parteienPassiv)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return NoContent();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<ParteienPassiv>> PostParteienPassiv(Int64 verfid, ParteienPassiv parteienPassiv)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetParteienPassivById", new { id = parteienPassiv.ParteiId }, parteienPassiv);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<ParteienPassiv>> DeleteParteienPassiv(Int64 verfid, int id)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(parteienPassiv);
        }
    }
}