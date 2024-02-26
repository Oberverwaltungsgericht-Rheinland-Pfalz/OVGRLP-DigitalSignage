// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DigitalSignage.WebApi.Controllers.EurekaFach;

[Authorize]
[Route("daten/verfahren")]
public class VerfahrenController : ControllerBase
{
    private readonly DigitalSignageDbContext _context;

    public VerfahrenController(DigitalSignageDbContext context)
    {
        _context = context;
    }


    [Route("")]
    [HttpGet]
    public IEnumerable<VerfahrenDto> GetAllVerfahren()
    {
        List<VerfahrenDto> dtos = new List<VerfahrenDto>();

        _context.Verfahren
            .Include(v => v.Stammdaten)
            .Include(v => v.ParteienAktiv)
            .Include(v => v.ParteienPassiv)
            .Include(v => v.Besetzung)
            .Include(v => v.ProzBevAktiv)
            .Include(v => v.ProzBevPassiv)
            .Include(v => v.ParteienBeigeladen)
            .Include(v => v.ProzBevBeigeladen)
            .Include(v => v.ParteienZeugen)
            .Include(v => v.ParteienSV)
            .Include(v => v.ParteienBeteiligt)
            .Include(v => v.Objekte)
            .ToList()
            .ForEach(v =>
            {
                dtos.Add(new VerfahrenDto(v));
            });

        return dtos;
    }

    [Route("{id}", Name = "GetVerfahrenById")]
    [HttpGet]
    public async Task<ActionResult<VerfahrenDto>> GetVerfahren(Int64 id)
    {
        var verfahren = await _context.Verfahren.FindAsync(id);
        if (verfahren == null)
            return NotFound();

        await _context.Entry(verfahren).Reference(v => v.Stammdaten).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ParteienAktiv).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ParteienPassiv).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.Besetzung).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ProzBevAktiv).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ProzBevPassiv).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ParteienBeigeladen).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ProzBevBeigeladen).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ParteienZeugen).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ParteienSV).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.ParteienBeteiligt).LoadAsync();
        await _context.Entry(verfahren).Collection(v => v.Objekte).LoadAsync();

        return Ok(new VerfahrenDto(verfahren));
    }

    [Route("{id}")]
    [HttpPut]
    public async Task<ActionResult> PutVerfahren([FromRoute] Int64 id, [FromBody] VerfahrenDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != dto.Id)
        {
            return BadRequest();
        }

        try
        {
            var verfahren = dto.GetVerfahrenFromDto();

            _context.Entry(verfahren).State = EntityState.Modified;
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
    public async Task<ActionResult<VerfahrenDto>> PostVerfahren([FromBody] VerfahrenDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var verfahren = dto.GetVerfahrenFromDto();

        try
        {
            _context.Verfahren.Add(verfahren);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        var newDto = new VerfahrenDto(verfahren);

        return CreatedAtRoute("GetVerfahrenById", new { id = newDto.Id }, newDto);
    }

    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult<Verfahren>> DeleteVerfahren(Int64 id)
    {
        var verfahren = await _context.Verfahren.FindAsync(id);

        if (verfahren == null)
        {
            return NotFound();
        }

        _context.Verfahren.Remove(verfahren);
        await _context.SaveChangesAsync();

        return Ok(verfahren);
    }
}