using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Models.Repositories;

/// <summary>
/// Repository Implementation for CRUD-Operations in the Database
/// </summary>
/// <typeparam name="TEntity">The Model of an Entity to Manage</typeparam>
public class GenericRepository<TEntity>(DbContext context) : IGenericRepository<TEntity>, IDisposable
    where TEntity : class, IBaseModel
{
    private readonly DbContext context = context;
    private bool disposed = false;

    /// <summary>
    /// Deletes an Entity from the Database
    /// </summary>
    /// <param name="id">The Id of the Entity to delete</param>
    /// <returns>True on success; false otherwise</returns>
    public async Task<bool> DeleteEntity(Guid id)
    {
        TEntity? entity = await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        if (entity == null) return false;

        return context.Set<TEntity>().Remove(entity).State == EntityState.Deleted && await Save() > 0;
    }

    /// <summary>
    /// Delete all Entities from the Database
    /// </summary>
    /// <returns>The number of Rows deleted</returns>
    public async Task<int> DeleteAllEntities()
    {
        return await context.Set<TEntity>().ExecuteDeleteAsync();
    }

    /// <summary>
    /// Disposal function 
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
            context.Dispose();

        disposed = true;
    }

    /// <summary>
    /// Disposal Function
    /// </summary>
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Returns all Entities from the Database
    /// </summary>
    /// <returns>A List of all Entities</returns>
    public async Task<ICollection<TEntity>> GetEntities()
    {
        return await context.Set<TEntity>().AsNoTracking().ToListAsync();
    }

    /// <summary>
    /// Fetches an Entity from the Database by Id
    /// </summary>
    /// <param name="id">The Id of the Entity</param>
    /// <returns>An Entity on sucess; null otherwise</returns>
    public async Task<TEntity?> GetEntityById(Guid id)
    {
        return await context.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
    }

    /// <summary>
    /// Feteches an Entity from the Database with a given search predicate
    /// </summary>
    /// <param name="predicate">The Search predicate</param>
    /// <returns>The found Entity; null otherwise</returns>
    public async Task<TEntity?> GetEntityByCustom(Expression<Func<TEntity, bool>> predicate)
    {
        return await context.Set<TEntity>().AsNoTracking().SingleOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Inserts an Entity into the Database
    /// </summary>
    /// <param name="entity">THe Entity to insert</param>
    /// <returns>An Entity on success; null otherwise</returns>
    public async Task<TEntity?> InsertEntity(TEntity entity)
    {
        // TODO: Do Not Track
        var e = await context.Set<TEntity>().AddAsync(entity);

        return await Save() > 0 ? e.Entity : null;
    }

    /// <summary>
    /// Inserts a list of Entities into the Database
    /// </summary>
    /// <param name="entities">A List of Entities</param>
    /// <returns>The number of Entities inserted</returns>
    public async Task<int> InsertEntityRange(ICollection<TEntity> entities)
    {
        // TODO: Do Not Track
        await context.Set<TEntity>().AddRangeAsync(entities);

        return await Save();
    }

    /// <summary>
    /// Saves all changes to the Database
    /// </summary>
    /// <returns>The Number of rows changed</returns>
    public async Task<int> Save()
    {
        return await context.SaveChangesAsync();
    }

    /// <summary>
    /// Updates an Entity
    /// </summary>
    /// <param name="entity">The Entity</param>
    /// <returns>True on success; false otherwise</returns>
    public async Task<bool> UpdateEntity(TEntity entity)
    {
        context.ChangeTracker.Clear();
        var e = context.Set<TEntity>().Update(entity);
        if (e == null)
            return false;

        return await Save() > 0;
    }
}
