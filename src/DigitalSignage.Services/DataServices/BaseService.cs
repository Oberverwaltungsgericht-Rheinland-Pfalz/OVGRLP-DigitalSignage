// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;

namespace DigitalSignage.Services.DataServices;

public class BaseService<TDbContext, TEntity, TId>
    where TEntity : class, IBaseModel<TId>
    where TId : struct
    where TDbContext : DbContext, new()
{
    protected TDbContext _context;
    protected ILogger<WorkService<TDbContext, TId>> _logger;

    public BaseService(TDbContext context, ILogger<WorkService<TDbContext, TId>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<TEntity?> DeleteEntity(TEntity entity)
    {
        // Is this necessary?
        // Can we assume TEntity is a valid entity?
        var e = await _context.Set<TEntity>().FindAsync(entity.Id);
        if (e == null) return null;

        _context.Set<TEntity>().Remove(e);
        try
        {
            await _context.SaveChangesAsync();

            _logger.LogDebug("{entity} deleted successful", typeof(TEntity));

            return e;
        }
        catch (Exception ex) when (ex is DbUpdateException || ex is DbUpdateConcurrencyException || ex is OperationCanceledException)
        {
            _logger.LogError("Error while deleting {entity} - {id}: {error}", typeof(TEntity), e.Id, ex.Message);
            return null;
        }
    }

    public async Task<TEntity?> InsertEntity(TEntity entity)
    {
        EntityEntry<TEntity>? e;

        _logger.LogDebug("Try to insert {entity}", typeof(TEntity));

        try
        {
            e = await _context.AddAsync(entity);
            await _context.SaveChangesAsync();

            _logger.LogDebug("{entity} inserted successful", e.Entity);

            return e.Entity;
        }
        catch (Exception ex) when (ex is DbUpdateException || ex is DbUpdateConcurrencyException || ex is OperationCanceledException)
        {
            _logger.LogError("Error while inserting/saving {entity} to database: {error}", typeof(TEntity), ex.Message);
            return null;
        }
    }

    public async Task<List<TEntity>> GetEntites()
    {
        try
        {
            return await _context.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex) when (ex is ArgumentNullException || ex is OperationCanceledException)
        {
            _logger.LogError("Error while loading multiple {entity} from database: {error}", typeof(TEntity), ex);
            return [];
        }
    }

    public async Task<TEntity?> GetEntityById(TId id)
    {
        var e = await _context.Set<TEntity>().FindAsync(id);
        if (e == null) return null;

        return e;
    }

    public async Task<TEntity?> UpdateEntity(TId id, TEntity entity)
    {
        if (!entity.Id.Equals(id)) return null;

        var e = await _context.Set<TEntity>().FindAsync(id);
        if (e == null) return null;

        _context.Entry(e).CurrentValues.SetValues(entity);
        _context.Entry(e).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();

            _logger.LogDebug("{entity} - {id} updated successful", typeof(TEntity), e.Id);
            
            return entity;
        }
        catch (Exception ex) when (ex is DbUpdateException || ex is DbUpdateConcurrencyException || ex is OperationCanceledException)
        {
            _logger.LogError("Error while updating/saving {entity} - {id} to database: {error}", typeof(TEntity), e.Id, ex.Message);
            return null;
        }
    }
}
