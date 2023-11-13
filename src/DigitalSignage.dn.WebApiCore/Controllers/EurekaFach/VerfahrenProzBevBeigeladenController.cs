// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/verfahren/{verfid}/prozbevbeigeladen")]
    public class VerfahrenProzBevBeigeladenController : ControllerBase
    {
        private readonly DigitalSignageDbContext _context;

        public VerfahrenProzBevBeigeladenController(DigitalSignageDbContext context)
        {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProzBevBeigeladen>>> GetAllProzBevBeigeladenByVerfahren(Int64 verfid)
        {
            var verfahren = await _context.Verfahren.FindAsync(verfid);

            if (verfahren == null)
            {
                return NotFound();
            }

            try
            {
                await _context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(verfahren.ProzBevBeigeladen);
        }

        [Route("{id}", Name = "GetProzBevBeigeladenById")]
        [HttpGet]
        public async Task<ActionResult<ProzBevBeigeladen>> GetProzBevBeigeladen(Int64 verfid, int id)
        {
            var prozBevBeigeladen = await _context.ProzBevBeigeladen.FindAsync(id);

            if (prozBevBeigeladen == null)
            {
                return NotFound();
            }

            return Ok(prozBevBeigeladen);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> PutProzBevBeigeladen(Int64 verfid, int id, ProzBevBeigeladen prozBevBeigeladen)
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
                _context.Entry(prozBevBeigeladen).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return NoContent();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<ProzBevBeigeladen>> PostProzBevBeigeladen(Int64 verfid, ProzBevBeigeladen prozBevBeigeladen)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var verfahren = await _context.Verfahren.FindAsync(verfid);

            if (verfahren == null)
            {
                return NotFound();
            }

            try
            {
                await _context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
                verfahren.ProzBevBeigeladen.Add(prozBevBeigeladen);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetProzBevBeigeladenById", new { id = prozBevBeigeladen.ProzBevId }, prozBevBeigeladen);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<ProzBevBeigeladen>> DeleteProzBevBeigeladen(Int64 verfid, int id)
        {
            var prozBevBeigeladen = await _context.ProzBevBeigeladen.FindAsync(id);

            if (prozBevBeigeladen == null)
            {
                return NotFound();
            }

            try
            {
                _context.ProzBevBeigeladen.Remove(prozBevBeigeladen);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return Ok(prozBevBeigeladen);
        }
    }
}