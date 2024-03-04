using Core.Models;
using Core.Models.Repositories;

namespace Services.Database.Services;

/// <summary>
/// Interface for Controllers to get access to the Repositories
/// </summary>
public interface IWorkService
{
    public IGenericRepository<TEntity>? Repository<TEntity>() where TEntity : class, IBaseModel;
}
