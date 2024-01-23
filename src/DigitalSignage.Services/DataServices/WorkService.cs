// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DigitalSignage.Services.DataServices;

public class WorkService<TDbContext, TId> : IWorkService<TDbContext, TId>
    where TId : struct
    where TDbContext : DbContext, new()
{
    public BaseService<TDbContext, ClientVersion<TId>, TId> _clientVersionService { get; }

    public BaseService<TDbContext, Department<TId>, TId> _departmentService { get; }
    public BaseService<TDbContext, Display<TId>, TId> _displayService { get; }
    public BaseService<TDbContext, EventChange<TId>, TId> _eventChangeService { get; }
    public BaseService<TDbContext, Event<TId>, TId> _eventService { get; }
    public BaseService<TDbContext, Filter<TId>, TId> _filterService { get; }
    public BaseService<TDbContext, Group<TId>, TId> _groupService {  get; }
    public BaseService<TDbContext, Notification<TId>, TId> _notificationService {  get; }
    public BaseService<TDbContext, Person<TId>, TId> _personService { get; }
    public BaseService<TDbContext, Room<TId>, TId> _roomService {  get; }
    public BaseService<TDbContext, Schedule<TId>, TId> _scheduleService { get; }
    public BaseService<TDbContext, Template<TId>, TId> _templateService { get; }

    readonly TDbContext context;

    public WorkService(DbContextOptions dbOptions, ILogger<WorkService<TDbContext, TId>> logger)
    {
        TDbContext? _context = null;
        try
        {
            _context = Activator.CreateInstance(typeof(TDbContext), dbOptions) as TDbContext;
        } catch (Exception ex)
        {
            logger.LogCritical("Error creating DbContext: {error}", ex.Message);
        }
        if (_context == null)
        {
            logger.LogCritical("Could not create DbInstance");
            throw new Exception("Could not create DbInstance");
        }
        context = _context;

        _clientVersionService = new(context, logger);
        _departmentService = new(context, logger);
        _displayService = new(context, logger);
        _eventChangeService = new(context, logger);
        _eventService = new(context, logger);
        _filterService = new(context, logger);
        _groupService = new(context, logger);
        _notificationService = new(context, logger);
        _personService = new(context, logger);
        _roomService = new(context, logger);
        _scheduleService = new(context, logger);
        _templateService = new(context, logger);
    }
}
