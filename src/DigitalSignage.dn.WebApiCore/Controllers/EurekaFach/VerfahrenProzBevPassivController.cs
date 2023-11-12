// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace DigitalSignage.WebApi.Controllers.EurekaFach;

[Route("daten/verfahren/{verfid}/prozbevpassiv")]
public class VerfahrenProzBevPassivController : Controller
{
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProzBevPassiv>>> GetAllProzBevPassivByVerfahren(Int64 verfid)
    {
        var verfahren = await context.Verfahren.FindAsync(verfid);

        if (verfahren == null)
        {
            return NotFound();
        }

        try
        {
            await context.Entry(verfahren).Collection(v => v.ProzBevPassiv).LoadAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok(verfahren.ProzBevPassiv);
    }

    [Route("{id}", Name = "GetProzBevPassivById")]
    [HttpGet]
    public async Task<ActionResult<ProzBevPassiv>> GetProzBevPassiv(Int64 verfid, int id)
    {
        var prozBevPassiv = await context.ProzBevPassiv.FindAsync(id);

        if (prozBevPassiv == null)
        {
            return NotFound();
        }

        return Ok(prozBevPassiv);
    }

    [Route("{id}")]
    [HttpPut]
    public async Task<ActionResult> PutProzBevPassiv(Int64 verfid, int id, ProzBevPassiv prozBevPassiv)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != prozBevPassiv.ProzBevId)
        {
            return BadRequest();
        }

        try
        {
            context.Entry(prozBevPassiv).State = EntityState.Modified;
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
    public async Task<ActionResult<ProzBevPassiv>> PostProzBevPassiv(Int64 verfid, ProzBevPassiv prozBevPassiv)
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
            await context.Entry(verfahren).Collection(v => v.ProzBevPassiv).LoadAsync();
            verfahren.ProzBevPassiv.Add(prozBevPassiv);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return CreatedAtRoute("GetProzBevPassivById", new { id = prozBevPassiv.ProzBevId }, prozBevPassiv);
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult<ProzBevPassiv>> DeleteProzBevPassiv(Int64 verfid, int id)
    {
        var prozBevPassiv = await context.ProzBevPassiv.FindAsync(id);

        if (prozBevPassiv == null)
        {
            return NotFound();
        }

        try
        {
            context.ProzBevPassiv.Remove(prozBevPassiv);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok(prozBevPassiv);
    }
}