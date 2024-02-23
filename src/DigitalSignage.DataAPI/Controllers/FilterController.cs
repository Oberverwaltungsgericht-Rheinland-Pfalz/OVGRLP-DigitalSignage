// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Data.JsonData;
using DigitalSignage.DataAPI.Extensions;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Filter<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Filters
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class FilterController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public FilterController(ILogger<FilterController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._filterService) { }

    [HttpPost("{id}/data")]
    public async Task<ActionResult<TEntity?>> SetFilterData(TId id, List<FilterDataJson<TId>> data)
    {
        TEntity? filter = await _ownService.GetEntityById(id);
        if (filter == null) return this.NotFound("Filter");

        filter.Data = data;
        filter = await _ownService.UpdateEntity(id, filter);
        if (filter == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(filter);
    }

    [HttpPost("{id}/data/add")]
    public async Task<ActionResult<TEntity?>> AddFilterData(TId id, FilterDataJson<TId> data)
    {
        TEntity? filter = await _ownService.GetEntityById(id);
        if (filter == null) return this.NotFound("Filter");

        FilterDataJson<TId>? _ = filter.Data.Find(f => f.Id == data.Id);
        if (_ != null) return this.AlreadyExists("FilterData");

        filter.Data.Add(data);
        filter = await _ownService.UpdateEntity(id, filter);
        if (filter == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(filter);
    }
}
