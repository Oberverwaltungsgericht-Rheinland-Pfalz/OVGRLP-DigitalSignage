using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;
using Core.Models.Json;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Schedules
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class ScheduleController(ILogger<ScheduleController> logger, IWorkService workService) : IBaseController<Schedule>(logger, workService)
{

    /// <summary>
    /// Endpoint for adding ScheduleDataJson for an Schedule
    /// </summary>
    /// <param name="id">The Schedule Id</param>
    /// <param name="data">The ScheduleDataJson Entity to be created and assigned</param>
    /// <returns>A Schedule Entity</returns>
    [HttpPost("{id}/data/add")]
    public async Task<ActionResult<Schedule>> AddScheduleData(Guid id, ScheduleDataJson data)
    {
        Schedule? schedule = await _repository.GetEntityById(id);
        if (schedule == null) return NotFound("A Schedule with this Id was not found");

        ScheduleDataJson? _ = schedule.Data.Find(d => d.ScheduleId == data.ScheduleId);
        if (_ != null) return BadRequest("A ScheduleData with this Id already exists");

        schedule.Data.Add(data);
        var s = await _repository.UpdateEntity(schedule);
        if (!s) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(schedule);
    }

    /// <summary>
    /// Endpoint for setting a List of ScheduleDataJson for an Schedule
    /// </summary>
    /// <param name="id">The Schedule Id</param>
    /// <param name="data">A List of ScheduleDataJson to override</param>
    /// <returns>A Schedule Entity</returns>
    [HttpPost("{id}/data")]
    public async Task<ActionResult<Schedule>> SetScheduleData(Guid id, List<ScheduleDataJson> data)
    {
        Schedule? schedule = await _repository.GetEntityById(id);
        if (schedule == null) return NotFound("A Schedule with this Id was not found");

        schedule.Data = data;
        var s = await _repository.UpdateEntity(schedule);
        if (!s) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(schedule);
    }
}
