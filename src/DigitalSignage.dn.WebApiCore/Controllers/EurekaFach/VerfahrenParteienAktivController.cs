// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignage.WebApi.Controllers.EurekaFach;

[Authorize]
[Route("daten/verfahren/{verfid}/parteienaktiv")]
public class VerfahrenParteienAktivController : ControllerBase
{
    private readonly DigitalSignageDbContext _context;

    public VerfahrenParteienAktivController(DigitalSignageDbContext context)
    {
        _context = context;
    }


    [Route("")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParteienAktiv>>> GetAllParteienAktivByVerfahren(Int64 verfid)
    {
        var verfahren = await _context.Verfahren.FindAsync(verfid);

        if (verfahren == null)
        {
            return NotFound();
        }

        try
        {
            await _context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
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
        var parteienAktiv = await _context.ParteienAktiv.FindAsync(id);

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
            _context.Entry(parteienAktiv).State = EntityState.Modified;
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
    public async Task<ActionResult<ParteienAktiv>> PostParteienAktiv(Int64 verfid, ParteienAktiv parteienAktiv)
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
            await _context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
            verfahren.ParteienAktiv.Add(parteienAktiv);
            await _context.SaveChangesAsync();
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
        var parteienAktiv = await _context.ParteienAktiv.FindAsync(id);

        if (parteienAktiv == null)
        {
            return NotFound();
        }

        try
        {
            _context.ParteienAktiv.Remove(parteienAktiv);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok(parteienAktiv);
    }
}