using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;
using System.Net;
using System.Net.NetworkInformation;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Displays
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class DisplayController(ILogger<DisplayController> logger, IWorkService workService) : IBaseController<Display>(logger, workService)
{
    /// <summary>
    /// Does the same as the normal post, but has some more Sanity-checks
    /// </summary>
    /// <param name="display">The Display to insert</param>
    /// <returns>The inserted Display Entity</returns>
    [HttpPost("safe")]
    public async Task<ActionResult<Display>> CreateDisplaySafe(Display display)
    {
        // Check all the Properties
        if (display.Name.Length == 0) return BadRequest("You must provide a Displayname");
        if (PhysicalAddress.TryParse(display.MacStr, out PhysicalAddress? addr) == false) return BadRequest("The Mac-Address is in an invalid format");
        if (IPAddress.TryParse(display.IpStr, out IPAddress? ip) == false) return BadRequest("The Ip-Address is in an invalid format");

        var e = await _repository.GetEntityById(display.Id);
        if (e != null) return BadRequest("A Display with this Id already exists");

        var displays = await _repository.GetEntities();
        foreach (var d in displays)
        {
            if (d.IpStr == display.IpStr || d.MacStr == display.MacStr)
                return BadRequest("An Display with this IP or MAC already exists");
        }

        display.Ip = ip;
        display.Mac = addr;

        e = await _repository.InsertEntity(display);
        if (e == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);    

        return Created(ControllerContext.HttpContext.Request.Path + "/" + e.Id, e);
    }

    /// <summary>
    /// Returns a Display by searching by Mac for it
    /// </summary>
    /// <param name="mac">The Mac-Address to search for</param>
    /// <returns>A Display Entity</returns>
    [HttpGet("getByMac/{mac}")]
    public async Task<ActionResult<Display>> CheckDisplayExistenceByNic(string mac)
    {
        _logger.LogDebug($"Search for Device with:\n\tMac:\t\t{mac}");
        var display = await _repository.GetEntityByCustom(d => d.Mac.ToString() == PhysicalAddress.Parse(mac).ToString());
        _logger.LogDebug($"Search found the following Display:\n{display}");
        if (display == null) return NotFound();

        return display; 
    }

    /// <summary>
    /// Endpoint for updating the Status of a Display
    /// </summary>
    /// <param name="id">The Id of the Display</param>
    /// <param name="displayStatus">The Status to be set</param>
    /// <returns>204 - NoContent</returns>
    [HttpGet("{id}/{displayStatus}")]
    public async Task<ActionResult> UpdateDisplayStatus(Guid id, DisplayStatus displayStatus)
    {
        Display? display = await _repository.GetEntityById(id);
        if (display == null) return NotFound("A Display with this Id was not found");

        display.Status = displayStatus;
        var d = await _repository.UpdateEntity(display);
        if (!d) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return NoContent();
    }

    /// <summary>
    /// Endpoint for adding a Filter to the Display by Id
    /// </summary>
    /// <param name="id">The Id of the Display</param>
    /// <param name="filter_id">The ID of the existing Filter to add to the Display</param>
    /// <returns>The Display Entity</returns>
    [HttpGet("{id}/filter/{filter_id}")]
    public async Task<ActionResult<Display>> AddFilterById(Guid id, Guid filter_id)
    {
        Display? display = await _repository.GetEntityById(id);
        if (display == null) return NotFound("A Display with this Id was not found");

        var filterRepo = _workService.Repository<Filter>();
        if (filterRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Filter? filter = await filterRepo.GetEntityById(filter_id);
        if (filter == null) return NotFound("A Filter with this Id was not found");

        display.Filter = filter;
        var d = await _repository.UpdateEntity(display);
        if (!d) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(display);
    }

    /// <summary>
    /// Endpoint for creating and adding a Filter to a Display
    /// </summary>
    /// <param name="id">THe Id of the Display</param>
    /// <param name="filter">The Filter Entity to be created and added</param>
    /// <returns></returns>
    [HttpPost("{id}/filter")]
    public async Task<ActionResult<Display>> AddFilter(Guid id, Filter filter)
    {
        Display? display = await _repository.GetEntityById(id);
        if (display == null) return NotFound("A Display with this Id was not found");

        var filterRepo = _workService.Repository<Filter>();
        if (filterRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Filter? filterEntity = await filterRepo.GetEntityById(filter.Id);
        if (filterEntity != null) return BadRequest("A Filter with this Id already exists");

        filterEntity = await filterRepo.InsertEntity(filter);
        if (filterEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await AddFilterById(id, filterEntity.Id);
    }
}
