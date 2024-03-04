using System.Linq.Expressions;

namespace Core.Models.Repositories;

/// <summary>
/// Interface for an Repository for CRUD-Operations in the Database
/// </summary>
/// <typeparam name="TEntity">The Model to manage</typeparam>
public interface IGenericRepository<TEntity> : IDisposable
    where TEntity : IBaseModel
{
    Task<ICollection<TEntity>> GetEntities();
    Task<TEntity?> GetEntityById(Guid id);
    Task<TEntity?> GetEntityByCustom(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity?> InsertEntity(TEntity entity);
    Task<int> InsertEntityRange(ICollection<TEntity> entities);
    Task<bool> DeleteEntity(Guid id);
    Task<int> DeleteAllEntities();
    Task<bool> UpdateEntity(TEntity entity);
    Task<int> Save();
}
