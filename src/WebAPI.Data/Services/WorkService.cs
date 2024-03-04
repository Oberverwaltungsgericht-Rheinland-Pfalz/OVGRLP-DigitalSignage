using Microsoft.EntityFrameworkCore;
using Core.Models;
using Core.Models.Repositories;

namespace Services.Database.Services;

/// <summary>
/// Implementation of the WorkService
/// </summary>
public class WorkService : IWorkService
{
    public IGenericRepository<ClientVersion> _clientVersion;
    public IGenericRepository<RegisterVersion> _registerVersion;
    public IGenericRepository<Department> _department;
    public IGenericRepository<Display> _display;
    public IGenericRepository<Event> _event;
    public IGenericRepository<EventChange> _eventChange;
    public IGenericRepository<Filter> _filter;
    public IGenericRepository<Group> _group;
    public IGenericRepository<Notification> _notification;
    public IGenericRepository<Person> _person;
    public IGenericRepository<Room> _room;
    public IGenericRepository<Schedule> _schedule;
    public IGenericRepository<Template> _template;

    private readonly DSContext dbContext;
    private readonly ILogger logger;

    public WorkService(ILogger<IWorkService> logger, DbContextOptions dbOptions)
    {
        dbContext = new DSContext(dbOptions);
        this.logger = logger;

        _clientVersion = new GenericRepository<ClientVersion>(dbContext);
        _registerVersion = new GenericRepository<RegisterVersion>(dbContext);
        _department = new GenericRepository<Department>(dbContext);
        _display = new GenericRepository<Display>(dbContext);
        _event = new GenericRepository<Event>(dbContext);
        _eventChange = new GenericRepository<EventChange>(dbContext);
        _filter = new GenericRepository<Filter>(dbContext);
        _group = new GenericRepository<Group>(dbContext);
        _notification = new GenericRepository<Notification>(dbContext);
        _person = new GenericRepository<Person>(dbContext);
        _room = new GenericRepository<Room>(dbContext);
        _schedule = new GenericRepository<Schedule>(dbContext);
        _template = new GenericRepository<Template>(dbContext);
    }

    /// <summary>
    /// Automatically retrieves an GenericRepository of the desired Type
    /// </summary>
    /// <typeparam name="TEntity">The Repository Type to search for, must be a derivate of IBaseModel</typeparam>
    /// <returns>GenericRepository{TEntity}</returns>
    public IGenericRepository<TEntity>? Repository<TEntity>()
        where TEntity : class, IBaseModel
    {
        var members = typeof(WorkService).GetFields();

        foreach (var prop in members)
        {
            var obj = prop.GetValue(this);
            var type = obj?.GetType();

            if (type == typeof(GenericRepository<TEntity>))
            {
                logger.LogDebug("Repository found for {type}", typeof(TEntity));
                if (obj == null) return default;
                return (GenericRepository<TEntity>)obj;
            }
        }

        logger.LogWarning("The Repository of Type {type} was not Found!", typeof(TEntity));

        return default;
    }
}
