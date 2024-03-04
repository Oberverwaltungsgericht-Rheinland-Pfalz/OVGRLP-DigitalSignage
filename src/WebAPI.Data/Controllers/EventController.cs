using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;
using Core.Models.Json;
using System.Collections.Concurrent;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Events
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class EventController(ILogger<EventController> logger, IWorkService workService) : IBaseController<Event>(logger, workService)
{
    /// <summary>
    /// Endpoint that returns a List of Events with a given List of Filters applied (List of Filter-Ids)
    /// </summary>
    /// <param name="filters">Array of Filter-Ids</param>
    /// <returns>A List of Events</returns>
    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<Event>>> getEntitiesFiltered([FromQuery] Guid[] filters)
    {
        var filterRepo = _workService.Repository<Filter>();
        if (filterRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // Save/Load all Filters ...
        List<Filter> f = [];
        foreach (var filter in filters)
        {
            Filter? _ = await filterRepo.GetEntityById(filter);
            if (_ == null) continue;

            f.Add(_);
        }

        if (f.Count != filters.Length) return BadRequest("At least one Filter that was passed was not found or is in an invalid format");

        IEnumerable<Event> allEvents = await _repository.GetEntities();

        // Apply each Filter ...
        foreach (var filter in f)
        {
            foreach (var filterData in filter.Data)
            {
                allEvents = ApplyFilter(allEvents, filterData);
            }
        }

        return Ok(allEvents);
    }

    /// <summary>
    /// Endpoint for applying a custom Filter to the Event Dataset
    /// </summary>
    /// <param name="filters">List of custom FIlterDataJson</param>
    /// <returns>A List of Events</returns>
    [HttpPost("filter")]
    public async Task<ActionResult<IEnumerable<Event>>> getEntitiesFilteredCustom(FilterDataJson[] filters)
    {
        IEnumerable<Event> allEvents = await _repository.GetEntities();

        _logger.LogDebug("Custom FilterData: {Length}", filters.Length);

        // Apply each FilterData ...
        var filterResults = new ConcurrentBag<IEnumerable<Event>>();
        Parallel.ForEach(filters, filter =>
        {
            filterResults.Add(ApplyFilter(allEvents, filter));
        });

        allEvents = filterResults.Aggregate((prev, next) => prev.Intersect(next)).ToList();

        return Ok(allEvents);
    }

    /// <summary>
    /// Internal Function for applying a filter to a list of Events, thus filtering them by some criteria
    /// </summary>
    /// <param name="allEvents">List of all Events</param>
    /// <param name="filterData">The Criteria to filter by</param>
    /// <returns>A Filtered List of Events</returns>
    private IEnumerable<Event> ApplyFilter(IEnumerable<Event> allEvents, FilterDataJson filterData)
    {
        _logger.LogDebug("Filter by: {Type}", filterData.Type);

        return filterData.Type switch
        {
            FilterType.Rooms => allEvents.Where((Event e) =>
                            {
                                _logger.LogDebug("Event {Id}:", e.Id);
                                return ApplyFilterMode(filterData,
                                    exclusive: e.Room != null && filterData.Targets.Contains(e.Room.Id),
                                    subtractive: e.Room == null || !filterData.Targets.Contains(e.Room.Id));
                            }),
            // TODO: Evtl. nicht notwendig
            FilterType.Persons => allEvents.Where((Event e) =>
                            {
                                return ApplyFilterMode(filterData,
                                    exclusive: false,
                                    subtractive: false);
                            }),
            FilterType.Departments => allEvents.Where((Event e) =>
                {
                    return ApplyFilterMode(filterData,
                        exclusive: e.Department != null && filterData.Targets.Contains(e.Department.Id),
                        subtractive: e.Department == null || !filterData.Targets.Contains(e.Department.Id));
                }),
            FilterType.Displays => allEvents.Where((Event e) =>
                {
                    return ApplyFilterMode(filterData,
                        exclusive: filterData.Targets.Where(
                            t => e.Room?.Displays.Where(d => d.Id == t).Count() > 0
                        ).Any(),
                        subtractive: filterData.Targets.Where(
                            t => e.Room?.Displays.Where(d => d.Id == t).Count() == 0
                        ).Any());
                }),
            _ => allEvents,
        };
    }

    /// <summary>
    /// Internal Function for filtering Events based on FilterMode
    /// </summary>
    /// <param name="filterData">Filterdata</param>
    /// <param name="exclusive">Value to be returned in Exclusevly mode</param>
    /// <param name="subtractive">Value to be returned in Subtractive mode</param>
    /// <param name="def">The default Parameter if no Mode is matched</param>
    /// <returns>Bool value that determines if the Event should be filtered or not</returns>
    private bool ApplyFilterMode(FilterDataJson filterData, bool exclusive, bool subtractive, bool def = false)
    {
        _logger.LogDebug("FilterMode: {FilterMode}", filterData.FilterMode);
        _logger.LogDebug("\tExclusive Keep: {exclusive}", exclusive);
        _logger.LogDebug("\tSubtractive Keep: {subtractive}", subtractive);
        _logger.LogDebug("\tDefault Keep: {def}", def);

        return filterData.FilterMode switch
        {
            FilterMode.Exclusive => exclusive,
            FilterMode.Subtractive => subtractive,
            _ => def,
        };
    }
}
