// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignage.WebApi.Controllers.EurekaFach;

[Authorize]
[Route("daten/verfahren/{verfid}/parteiensv")]
public class VerfahrenParteienSVController : ControllerBase
{
    private readonly DigitalSignageDbContext _context;

    public VerfahrenParteienSVController(DigitalSignageDbContext context)
    {
        _context = context;
    }


    [Route("")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ParteienSV>>> GetAllParteienSVByVerfahren(Int64 verfid)
    {
        var verfahren = await _context.Verfahren.FindAsync(verfid);

        if (verfahren == null)
        {
            return NotFound();
        }

        try
        {
            await _context.Entry(verfahren).Collection(v => v.ParteienSV).LoadAsync();
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
        var parteienSV = await _context.ParteienSV.FindAsync(id);

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
            _context.Entry(parteienSV).State = EntityState.Modified;
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
    public async Task<ActionResult<ParteienSV>> PostParteienSV(Int64 verfid, ParteienSV parteienSV)
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
            await _context.Entry(verfahren).Collection(v => v.ParteienSV).LoadAsync();
            verfahren.ParteienSV.Add(parteienSV);
            await _context.SaveChangesAsync();
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
        var parteienSV = await _context.ParteienSV.FindAsync(id);

        if (parteienSV == null)
        {
            return NotFound();
        }

        try
        {
            _context.ParteienSV.Remove(parteienSV);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok(parteienSV);
    }
}