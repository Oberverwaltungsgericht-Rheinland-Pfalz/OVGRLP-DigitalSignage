// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.DataAPI.Extensions;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.NetworkInformation;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Display<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Displays
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class DisplayController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public DisplayController(ILogger<DisplayController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._displayService)
    {
    }

    [HttpPost("safe")]
    public async Task<ActionResult<TEntity>> CreateDisplaySafe(TEntity display)
    {
        // Check all the Properties
        if (display.Name.Length == 0) return this.EmptyField("Display", "Name");
        if (PhysicalAddress.TryParse(display.MacStr, out PhysicalAddress? addr) == false) return this.WrongFormat("Mac");
        if (IPAddress.TryParse(display.IpStr, out IPAddress? ip) == false) return this.WrongFormat("Ip");

        var e = await _ownService.GetEntityById(display.Id);
        if (e != null) return this.AlreadyExists("Display");

        display.Ip = ip;
        display.Mac = addr;

        e = await _ownService.InsertEntity(display);
        if (e == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);    

        return Created(ControllerContext.HttpContext.Request.Path + "/" + e.Id, e);
    }


    // TODO: Move to DisplayService
    /*[HttpGet("{id}/events/{date}")]
    public async Task<ActionResult<TEntityEvent>> GetEventsForDate(TId id, DateOnly date)
    {
        TEntity? display = await _ownService.GetEntityById(id);
        if (display == null) return NotFound("Did not find a Display with the given Id");

        Filter<TId>[] filters = [];
        Filter<TId>? filter = display.Filter;
        if (filter != null)
            filters.Append(filter);
        // TODO: Apply Filters

        Group<TId>? group = display.Group;
        if (group != null && group.Filter != null)
            filters.Append(group.Filter);
        // TODO: Apply Filters from Group

        //EventController ec = new EventController(_context);
        //var events = await ec.getEntitiesFiltered(filters);

        //return Ok(events);
        return NoContent();
    }*/

    [HttpGet("{id}/{displayStatus}")]
    public async Task<ActionResult> UpdateDisplayStatus(TId id, DisplayStatus displayStatus)
    {
        TEntity? display = await _ownService.GetEntityById(id);
        if (display == null) return this.NotFoundReturn("Display");

        display.Status = displayStatus;
        var d = await _ownService.UpdateEntity(id, display);
        if (d == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return NoContent();
    }

    [HttpGet("{id}/filter/{filter_id}")]
    public async Task<ActionResult> AddFilterById(TId id, TId filter_id)
    {
        TEntity? display = await _ownService.GetEntityById(id);
        if (display == null) return this.NotFoundReturn("Display");

        Filter<TId>? filter = await _workService._filterService.GetEntityById(filter_id);
        if (filter == null) return this.AlreadyExists("Filter");

        display.Filter = filter;
        var d = await _ownService.UpdateEntity(id, display);
        if (d == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(display);
    }

    [HttpPost("{id}/filter")]
    public async Task<ActionResult> AddFilter(TId id, Filter<TId> filter)
    {
        TEntity? display = await _ownService.GetEntityById(id);
        if (display == null) return this.NotFoundReturn("Display");

        Filter<TId>? filterEntity = await _workService._filterService.GetEntityById(filter.Id);
        if (filterEntity != null) return this.AlreadyExists("Filter");

        filterEntity = await _workService._filterService.InsertEntity(filter);
        if (filterEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await AddFilterById(id, filterEntity.Id);
    }
}
