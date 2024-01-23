// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Data.JsonData;
using DigitalSignage.DataAPI.Extensions;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Event<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Events
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class EventController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public EventController(ILogger<EventController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._eventService)
    {
    }

    [HttpGet("filter")]
    public async Task<ActionResult<IEnumerable<TEntity>>> getEntitiesFiltered([FromQuery] TId[] filters)
    {
        // Save/Load all Filters ...
        List<Filter<TId>> f = [];
        foreach (var filter in filters)
        {
            Filter<TId>? _ = await _workService._filterService.GetEntityById(filter);
            if (_ == null) continue;

            f.Add(_);
        }

        if (f.Count() != filters.Length) return this.WrongContent("filters");
        
        IEnumerable<TEntity> allEvents = await _ownService.GetEntites();

        // Apply each Filter ...
        foreach (var filter in f)
        {
            foreach (var filterData in filter.Data)
            {
                switch (filterData.Type)
                {
                    case FilterType.Rooms:
                        {
                            allEvents = allEvents.Where((TEntity e) =>
                            {
                                switch (filterData.FilterMode)
                                {
                                    case FilterMode.Exclusive:
                                        {
                                            return e.Room != null ? filterData.Targets.Contains(e.Room.Id) : false;
                                        }
                                    case FilterMode.Subtractive:
                                        {
                                            return e.Room != null ? !filterData.Targets.Contains(e.Room.Id) : true;
                                        }
                                    default:
                                        {
                                            return false;
                                        }
                                }
                            });
                            break;
                        }
                    // TODO: Evtl. nicht notwendig
                    case FilterType.Persons:
                        {
                            allEvents = allEvents.Where((TEntity e) =>
                            {
                                switch (filterData.FilterMode)
                                {
                                    case FilterMode.Exclusive:
                                        {
                                            return false;
                                        }
                                    case FilterMode.Subtractive:
                                        {
                                            return false;
                                        }
                                    default:
                                        {
                                            return false;
                                        }
                                }
                            });
                            break;
                        }
                    case FilterType.Departments:
                        {
                            allEvents = allEvents.Where((TEntity e) =>
                            {
                                switch (filterData.FilterMode)
                                {
                                    case FilterMode.Exclusive:
                                        {
                                            return e.Department != null ? filterData.Targets.Contains(e.Department.Id) : false;
                                        }
                                    case FilterMode.Subtractive:
                                        {
                                            return e.Department != null ? !filterData.Targets.Contains(e.Department.Id) : true;
                                        }
                                    default:
                                        {
                                            return false;
                                        }
                                }
                            });
                            break;
                        }
                    case FilterType.Displays:
                        {
                            allEvents = allEvents.Where((TEntity e) =>
                            {
                                switch (filterData.FilterMode)
                                {
                                    case FilterMode.Exclusive:
                                        {
                                            return filterData.Targets.Where(
                                                t => e.Room?.Displays.Where(d => d.Id == t).Count() > 0
                                            ).Count() > 0;
                                        }
                                    case FilterMode.Subtractive:
                                        {
                                            return filterData.Targets.Where(
                                                t => e.Room?.Displays.Where(d => d.Id == t).Count() == 0
                                            ).Count() > 0;
                                        }
                                    default:
                                        return false;
                                }
                            });
                            break;
                        }
                }
            }
        }

        return Ok(allEvents);
    }
}
