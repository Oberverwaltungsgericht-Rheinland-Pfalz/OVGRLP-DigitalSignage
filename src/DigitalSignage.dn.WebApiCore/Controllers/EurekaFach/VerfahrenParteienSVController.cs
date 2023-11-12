// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Net;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/parteiensv")]
    public class VerfahrenParteienSVController : Controller
    {
        private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParteienSV>>> GetAllParteienSVByVerfahren(Int64 verfid)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.ParteienSV);
        }

        [Route("{id}", Name = "GetParteienSVById")]
        [HttpGet]
        public async Task<ActionResult<ParteienSV>> GetParteienSV(Int64 verfid, int id)
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
        public async Task<ActionResult> PutParteienSV(Int64 verfid, int id, ParteienSV parteienSV)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return NoContent();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<ParteienSV>> PostParteienSV(Int64 verfid, ParteienSV parteienSV)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetParteienSVById", new { id = parteienSV.ParteiId }, parteienSV);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<ParteienSV>> DeleteParteienSV(Int64 verfid, int id)
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(parteienSV);
        }
    }
}