// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Data.JsonData;
using DigitalSignage.DataAPI.Extensions;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Schedule<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Schedules
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class ScheduleController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public ScheduleController(ILogger<ScheduleController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._scheduleService)
    {
    }

    [HttpPost("{id}/data/add")]
    public async Task<ActionResult<TEntity>> AddScheduleData(TId id, ScheduleDataJson<TId> data)
    {
        TEntity? schedule = await _ownService.GetEntityById(id);
        if (schedule == null) return this.NotFound("Schedule");

        ScheduleDataJson<TId>? _ = schedule.Data.Find(d => d.Id == data.Id);
        if (_ != null) return this.AlreadyExists("ScheduleDataJson");

        schedule.Data.Add(data);
        schedule = await _ownService.UpdateEntity(id, schedule);
        if (schedule == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(schedule);
    }

    [HttpPost("{id}/data")]
    public async Task<ActionResult<TEntity>> SetScheduleData(TId id, List<ScheduleDataJson<TId>> data)
    {
        TEntity? schedule = await _ownService.GetEntityById(id);
        if (schedule == null) return this.NotFound("Schedulue");

        schedule.Data = data;
        schedule = await _ownService.UpdateEntity(id, schedule);
        if (schedule == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(schedule);
    }
}
