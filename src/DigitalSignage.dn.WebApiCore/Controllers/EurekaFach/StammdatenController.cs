// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Diagnostics;

namespace DigitalSignage.WebApi.Controllers.EurekaFach
{
    [Route("daten/stammdaten")]
    public class StammdatenController : Controller
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
        public async Task<ActionResult<Stammdaten>> GetStammdaten(int id)
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
                context.Entry(stammdaten).State = EntityState.Modified;
                await context.SaveChangesAsync();
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
                context.Stammdaten.Add(stammdaten);
                await context.SaveChangesAsync();
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
            var stammdaten = await context.Stammdaten.FindAsync(id);

            if (stammdaten == null)
            {
                return NotFound();
            }

            context.Stammdaten.Remove(stammdaten);
            await context.SaveChangesAsync();

            return Ok(stammdaten);
        }
    }
}