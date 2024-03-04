using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;
using Core.Models.Repositories;

namespace Services.Database.Controllers;

/// <summary>
/// Base-Implementation of a REST-Controller for basic CRUD Operations.
/// </summary>
/// <typeparam name="TEntity">The Entity Model of the Model</typeparam>
public abstract class IBaseController<TEntity> : Controller
    where TEntity : class, IBaseModel
{
    protected ILogger _logger;
    protected IWorkService _workService;
    protected IGenericRepository<TEntity> _repository;

    public IBaseController(ILogger<IBaseController<TEntity>> logger, IWorkService workService)
    {
        _logger = logger;
        _workService = workService;

        var repo = _workService.Repository<TEntity>() ?? throw new ApplicationException($"Could not load Repository for {typeof(TEntity)}");
        _repository = repo;
    }

    /// <summary>
    /// Deletes an given Entity
    /// </summary>
    /// <param name="entity">Entity to Delete</param>
    /// <returns>The deleted Entity</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteEntity(Guid id)
    {
        var entity = await _repository.GetEntityById(id);
        if (entity == null) return NotFound();

        var result = await _repository.DeleteEntity(id);
        if (!result) return NotFound();

        return NoContent();
    }

    [HttpDelete]
    public async Task<ActionResult<int>> DeleteAllEntities()
    {
        var deletedRows = await _repository.DeleteAllEntities();
        return Ok(deletedRows);
    }

    /// <summary>
    /// Adds a new entity to the Database
    /// </summary>
    /// <param name="entity">Entity to Insert</param>
    /// <returns>The inserted Entity</returns>
    [HttpPost]
    public async Task<ActionResult<TEntity>> InsertEntity(TEntity entity)
    {
        _logger.LogDebug("Try to insert...\n {entity}", entity);

        var e = await _repository.InsertEntity(entity);
        if (e == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Created(ControllerContext.HttpContext.Request.Path + "/" + e?.Id, e);
    }

    /// <summary>
    /// Adds a new entities to the Database
    /// </summary>
    /// <param name="entity">List of Entities to Insert</param>
    /// <returns>The inserted Entity</returns>
    [HttpPost("range")]
    public async Task<ActionResult<int>> InsertEntityRange(ICollection<TEntity> entity)
    {
        var e = await _repository.InsertEntityRange(entity);

        return Ok(e);
    }

    /// <summary>
    /// Get all Entities from the Database
    /// </summary>
    /// <returns>List of All Entities</returns>
    [HttpGet]
    public async Task<ActionResult<ICollection<TEntity>>> GetEntities()
    {
        return Ok(await _repository.GetEntities());
    }

    /// <summary>
    /// Retrieves an Entity by Id
    /// </summary>
    /// <param name="id">The Id of the Entity</param>
    /// <returns>The found Entity</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TEntity>> GetEntityById(Guid id)
    {
        var e = await _repository.GetEntityById(id);
        if (e == null) return NotFound();

        return Ok(e);
    }

    /// <summary>
    /// Updates an Entity
    /// </summary>
    /// <param name="id">The Id of the Entity to update</param>
    /// <param name="entity">The Entity to update</param>
    /// <returns>204 NoContent on success</returns>
    [HttpPut]
    public async Task<ActionResult> UpdateEntity(TEntity entity)
    {
        _logger.LogDebug($"Update Entity with Values:\n{entity}");

        var e = await _repository.UpdateEntity(entity);
        if (!e) return NotFound();

        return NoContent();
    }
}
