// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/stammdaten")]
    public class StammdatenController : ControllerBase
    {
        private readonly DigitalSignageDbContext _context;

        public StammdatenController(DigitalSignageDbContext context) {
            _context = context;
        }

        [Route("")]
        [HttpGet]
        public IEnumerable<Stammdaten> GetAllStammdaten()
        {
            return _context.Stammdaten;
        }

        [Route("{id}", Name = "GetStammdatenById")]
        [HttpGet]
        public async Task<ActionResult<Stammdaten>> GetStammdaten(int id)
        {
            var stammdaten = await _context.Stammdaten.FindAsync(id);

            if (stammdaten == null)
            {
                return NotFound();
            }

            return Ok(stammdaten);
        }

        [Route("{id}")]
        [HttpPut]
        public async Task<ActionResult> PutStammdaten(int id, Stammdaten stammdaten)
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
                _context.Entry(stammdaten).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return NoContent();
        }

        [Route("")]
        [HttpPost]
        public async Task<ActionResult<Stammdaten>> PostStammdaten(Stammdaten stammdaten)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                _context.Stammdaten.Add(stammdaten);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }

            return CreatedAtRoute("GetStammdatenById", new { id = stammdaten.StammdatenId }, stammdaten);
        }

        [Route("{id}")]
        [HttpDelete]
        public async Task<ActionResult<Stammdaten>> DeleteStammdaten(int id)
        {
            var stammdaten = await _context.Stammdaten.FindAsync(id);

            if (stammdaten == null)
            {
                return NotFound();
            }

            _context.Stammdaten.Remove(stammdaten);
            await _context.SaveChangesAsync();

            return Ok(stammdaten);
        }
    }
}