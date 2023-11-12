// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach;

[Route("daten/verfahren/{verfid}/parteienbeigeladen")]
public class VerfahrenParteienBeigeladenController : Controller
{
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParteienBeigeladen>>> GetAllParteienBeigeladenByVerfahren(Int64 verfid)
    {
        var verfahren = await context.Verfahren.FindAsync(verfid);

        if (verfahren == null)
        {
            return NotFound();
        }

        try
        {
            await context.Entry(verfahren).Collection(v => v.ParteienBeigeladen).LoadAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok(verfahren.ParteienBeigeladen);
    }

    [Route("{id}", Name = "GetParteienBeigeladenById")]
    [HttpGet]
    public async Task<ActionResult<ParteienBeigeladen>> GetParteienBeigeladen(Int64 verfid, int id)
    {
        var parteienBeigeladen = await context.ParteienBeigeladen.FindAsync(id);

        if (parteienBeigeladen == null)
        {
            return NotFound();
        }

        return Ok(parteienBeigeladen);
    }

    [Route("{id}")]
    [HttpPut]
    public async Task<ActionResult> PutParteienBeigeladen(Int64 verfid, int id, ParteienBeigeladen parteienBeigeladen)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != parteienBeigeladen.ParteiId)
        {
            return BadRequest();
        }

        try
        {
            context.Entry(parteienBeigeladen).State = EntityState.Modified;
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
    public async Task<ActionResult<ParteienBeigeladen>> PostParteienBeigeladen(Int64 verfid, ParteienBeigeladen parteienBeigeladen)
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
            await context.Entry(verfahren).Collection(v => v.ParteienBeigeladen).LoadAsync();
            verfahren.ParteienBeigeladen.Add(parteienBeigeladen);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return CreatedAtRoute("GetParteienBeigeladenById", new { id = parteienBeigeladen.ParteiId }, parteienBeigeladen);
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult<ParteienBeigeladen>> DeleteParteienBeigeladen(Int64 verfid, int id)
    {
        var parteienBeigeladen = await context.ParteienBeigeladen.FindAsync(id);

        if (parteienBeigeladen == null)
        {
            return NotFound();
        }

        try
        {
            context.ParteienBeigeladen.Remove(parteienBeigeladen);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok(parteienBeigeladen);
    }
}