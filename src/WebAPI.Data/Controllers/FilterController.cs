using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;
using Core.Models.Json;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Filters
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class FilterController(ILogger<FilterController> logger, IWorkService workService) : IBaseController<Filter>(logger, workService)
{

    /// <summary>
    /// Endpoint for setting FilterData in an Filter Entity
    /// </summary>
    /// <param name="id">The Id of the Filter</param>
    /// <param name="data">A List of FilterDataJson that should be set</param>
    /// <returns>A Filter Entity</returns>
    [HttpPost("{id}/data")]
    public async Task<ActionResult<Filter?>> SetFilterData(Guid id, List<FilterDataJson> data)
    {
        Filter? filter = await _repository.GetEntityById(id);
        if (filter == null) return NotFound("A Filter with this Id was not found");

        filter.Data = data;
        var f = await _repository.UpdateEntity(filter);
        if (!f) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(filter);
    }

    /// <summary>
    /// Endpoint for adding FilterDataJson to an existing Filter Entity
    /// </summary>
    /// <param name="id">The Id of the Filter</param>
    /// <param name="data">The FIlterJsonData Entity that should be added</param>
    /// <returns>AA Filter Entity</returns>
    [HttpPost("{id}/data/add")]
    public async Task<ActionResult<Filter?>> AddFilterData(Guid id, FilterDataJson data)
    {
        Filter? filter = await _repository.GetEntityById(id);
        if (filter == null) return NotFound("A Filter with this Id was not found");

        FilterDataJson? _ = filter.Data.Find(f => f.FilterId == data.FilterId);
        if (_ != null) return BadRequest("A FilterData with this Id already exists");

        filter.Data.Add(data);
        var f = await _repository.UpdateEntity(filter);
        if (!f) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(filter);
    }
}
