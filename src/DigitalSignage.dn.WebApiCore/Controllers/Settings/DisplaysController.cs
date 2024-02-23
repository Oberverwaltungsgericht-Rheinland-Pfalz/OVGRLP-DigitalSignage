// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.dn.WebApiCore.DtoModels;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.WebApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DigitalSignage.WebApi.Controllers.Settings;

[Authorize]
[Route("settings/displays")]
[ApiController]
public class DisplaysController : ControllerBase
{
    private readonly DigitalSignageDbContext _context;
    private readonly DisplayManagementService _displayManagementService;

    public DisplaysController(DigitalSignageDbContext context, DisplayManagementService displayManagementService)
    {
        _context = context;
        _displayManagementService = displayManagementService;
    }

    [Route("")]
    [HttpGet]
    public IEnumerable<Display> GetAllDisplays()
    {
        return _context.Displays.Where(d => d.Dummy == false);
    }

    [Route("DisplaysEx")]
    [HttpGet]
    public IEnumerable<DisplayDto> GetAllDisplaysEx()
    {
        List<Display> displays = _context.Displays.Where(d => d.Dummy == false).ToList();
        List<DisplayDto> displaysEx = new List<DisplayDto>();
        foreach (Display disp in displays)
        {
            displaysEx.Add(DisplayDto.FromDisplay(disp, _displayManagementService));
        }

        return displaysEx;
    }

    [Route("{name}", Name = "GetDisplay")]
    [HttpGet]
    public async Task<ActionResult> GetDisplay(string name)
    {
        var display = await _context.Displays
          .FirstAsync(
            d => d.Name == name);

        if (display == null)
            return NotFound();

        var displayDto = new
        {
            Id = display.Id,
            Description = display.Description,
            Name = display.Name,
            Title = display.Title,
            Template = display.Template,
            Styles = display.Styles,
            ControlUrl = display.ControlUrl,
            Group = display.Group
        };

        return Ok(displayDto);
    }

    [Route("{name}/DisplayEx", Name = "GetDisplayEx")]
    [HttpGet]
    public async Task<ActionResult<DisplayDto>> GetDisplayEx(string name)
    {
        var display = await _context.Displays
          .FirstAsync(
            d => d.Name == name);

        if (display == null)
            return NotFound();

        DisplayDto displayDto = DisplayDto.FromDisplay(display, _displayManagementService);

        return Ok(displayDto);
    }

    [Route("{name}/termine")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VerfahrenDto>>> GetAllTermine(string name)
    {
        var display = await _context.Displays.FirstAsync(
          d => d.Name == name);

        if (display == null)
            return NotFound();

        List<VerfahrenDto> dtos = new List<VerfahrenDto>();

        List<Verfahren> verfahrenList;
        
        if(display.Filter is null or "")
        {
            verfahrenList = _context.Verfahren.ToList();
        }
        else
        {
            verfahrenList = _context.Verfahren
                .FromSql<Verfahren>($"SELECT * FROM Verfahren WHERE {display.Filter}")
                .ToList();
        }

        verfahrenList.ForEach(v =>
            {
                _context.Entry(v).Reference("Stammdaten").Load();
                _context.Entry(v).Collection("ParteienAktiv").Load();
                _context.Entry(v).Collection("ParteienPassiv").Load();
                _context.Entry(v).Collection("Besetzung").Load();
                _context.Entry(v).Collection("ProzBevAktiv").Load();
                _context.Entry(v).Collection("ProzBevPassiv").Load();
                _context.Entry(v).Collection("ParteienBeigeladen").Load();
                _context.Entry(v).Collection("ProzBevBeigeladen").Load();
                _context.Entry(v).Collection("ParteienZeugen").Load();
                _context.Entry(v).Collection("ParteienSV").Load();
                _context.Entry(v).Collection("ParteienBeteiligt").Load();
                _context.Entry(v).Collection("Objekte").Load();
                dtos.Add(new VerfahrenDto(v));
            }
        );

        return Ok(dtos);
    }

    [Route("{name}/activenotes")]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Note>>> GetActiveNotes(string name, DateTime? timestamp)
    {
        DateTime validTimestamp = timestamp.GetValueOrDefault(DateTime.Now);
        var display = await _context.Displays
          .Include(d => d.NotesAssignments)
          .ThenInclude(n => n.Note)
          .FirstAsync( d => d.Name == name);

        if (display == null)
            return NotFound();

        bool IsActiveNoteAssignment(NoteAssignment noteAssignment, DateTime dateTime)
        {
            bool erval = true;

            if (noteAssignment.Start.HasValue && noteAssignment.Start.Value > dateTime)
                erval = false;

            if (noteAssignment.End.HasValue && noteAssignment.End.Value < dateTime)
                erval = false;

            return erval;
        }
        var result = display.NotesAssignments.Where(na => IsActiveNoteAssignment(na, validTimestamp)).Select(na => na.Note);

        return Ok(result);
    }


    [Route("{name}/status")]
    [HttpGet]
    public async Task<ActionResult<DisplayStatus>> GetStatusForDisplay(string name)
    {
        var display = await _context
            .Displays
            .FirstAsync(d => d.Name == name);

        if (display == null)
            return NotFound();

        return Ok(_displayManagementService.GetDisplayStatus(display));
    }

    [Route("{name}/start")]
    [HttpGet]
    public async Task<ActionResult> StartDisplay(string name)
    {
        var display = await _context.Displays.FirstAsync(
          d => d.Name == name);

        if (display == null)
            return NotFound();

        try
        {
            _displayManagementService.StartDisplay(display);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok();
    }

    [Route("{name}/restart")]
    [HttpGet]
    public async Task<ActionResult> RestartDisplay(string name)
    {
        var display = await _context.Displays.FirstAsync(
          d => d.Name == name);

        if (display == null)
            return NotFound();

        try
        {
            await _displayManagementService.RestartDisplay(display);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok();
    }

    [Route("{name}/stop")]
    [HttpGet]
    public async Task<ActionResult> StopDisplay(string name)
    {
        var display = await _context.Displays.FirstAsync(
          d => d.Name == name);

        if (display == null)
            return NotFound();

        try
        {
            await _displayManagementService.StopDisplay(display);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return Ok();
    }

    [Route("{name}/ScreenshotUrl")]
    [HttpGet]
    public async Task<ActionResult> GetScreenshotUrl(string name)
    {
        string url = string.Empty;
        var display = await _context.Displays.FirstAsync(
          d => d.Name == name);

        if (display == null)
            return NotFound();

        try
        {
            url = _displayManagementService.GetDisplayScreenshotUrl(display);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        return Ok(url);
    }

    [Route("{name}")]
    [HttpPut]
    public async Task<ActionResult> PutDisplay(string name, Display display)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (name != display.Name)
        {
            return BadRequest();
        }

        try
        {
            _context.Entry(display).State = EntityState.Modified;
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
    public async Task<ActionResult> PostDisplay(Display display)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            _context.Displays.Add(display);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            return StatusCode(StatusCodes.Status500InternalServerError, ex);
        }

        return CreatedAtRoute("GetDisplay", new { name = display.Name }, display);
    }

    [Route("{name}")]
    [HttpDelete]
    public async Task<ActionResult<Display>> DeleteDisplay(string name)
    {
        var display = await _context.Displays.FindAsync(name);

        if (display == null)
        {
            return NotFound();
        }

        _context.Displays.Remove(display);
        await _context.SaveChangesAsync();

        return Ok(display);
    }
}