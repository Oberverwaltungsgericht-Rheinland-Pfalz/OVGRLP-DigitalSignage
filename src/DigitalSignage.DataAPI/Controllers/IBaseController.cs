// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignage.DataAPI.Controllers;

/// <summary>
/// Base-Implementation of a REST-Controller for basic CRUD Operations.
/// </summary>
/// <typeparam name="TEntity">The Entity Model of the Model</typeparam>
public class IBaseController<TDbContext, TService, TEntity, TId> : Controller
    where TDbContext : DbContext, new()
    where TService : BaseService<TDbContext, TEntity, TId>
    where TEntity : class, IBaseModel<TId>
    where TId : struct
{
    protected ILogger<IBaseController<TDbContext, TService, TEntity, TId>> _logger;
    protected IWorkService<TDbContext, TId> _workService;
    protected TService _ownService;


    public IBaseController(ILogger<IBaseController<TDbContext, TService, TEntity, TId>> logger, IWorkService<TDbContext, TId> workService, TService ownService)
    {
        _workService = workService;
        _ownService = ownService;
        _logger = logger;
    }

    /// <summary>
    /// Deletes an given Entity
    /// </summary>
    /// <param name="entity">Entity to Delete</param>
    /// <returns>The deleted Entity</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteEntity(TId id)
    {
        var entity = await _ownService.GetEntityById(id);
        if (entity == null) return NotFound();

        var e = await _ownService.DeleteEntity(entity);
        if (e == null) return NotFound();

        return NoContent();
    }

    /// <summary>
    /// Adds a new entity to the Database
    /// </summary>
    /// <param name="entity">Entity to Insert</param>
    /// <returns>The inserted Entity</returns>
    [HttpPost]
    public async Task<ActionResult<TEntity>> insertEntity(TEntity entity)
    {
        var e = await _ownService.InsertEntity(entity);

        return Created(ControllerContext.HttpContext.Request.Path + "/" + e?.Id, e);
    }

    /// <summary>
    /// Get all Entities from the Database
    /// </summary>
    /// <returns>List of All Entities</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TEntity>>> getEntities()
    {
        return Ok(await _ownService.GetEntites());
    }

    /// <summary>
    /// Retrieves an Entity by Id
    /// </summary>
    /// <param name="id">The Id of the Entity</param>
    /// <returns>The found Entity</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<TEntity>> getEntityById(TId id)
    {
        var e = await _ownService.GetEntityById(id);
        if (e == null) return NotFound();

        return Ok(e);
    }

    /// <summary>
    /// Updates an Entity
    /// </summary>
    /// <param name="id">The Id of the Entity to update</param>
    /// <param name="entity">The Entity to update</param>
    /// <returns>204 NoContent on success</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult> updateEntity(TId id, TEntity entity)
    {
        // Check if the Ids of the Path and the Objects match...
        if (!entity.Id.Equals(id)) return BadRequest();

        var e = await _ownService.UpdateEntity(id, entity);
        if (e == null) return NotFound();

        return NoContent();
    }
}
