// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.DataAPI.Extensions;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Notification<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Notifications
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class NotificationController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public NotificationController(ILogger<NotificationController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._notificationService)
    {
    }

    [HttpGet("{id}/template/{template_id}")]
    public async Task<ActionResult<TEntity>> SetTemplateById(TId id, TId template_id)
    {
        TEntity? notification = await _ownService.GetEntityById(id);
        if (notification == null) return this.NotFound("Notification");

        Template<TId>? template = await _workService._templateService.GetEntityById(template_id);
        if (template == null) return this.NotFound("Template");

        notification.Template = template;
        notification.TemplateId = template_id;

        notification = await _ownService.UpdateEntity(id, notification);
        if (notification == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(notification);
    }

    [HttpPost("{id}/template")]
    public async Task<ActionResult<TEntity>> SetTemplate(TId id, Template<TId> template)
    {
        TEntity? notification = await _ownService.GetEntityById(id);
        if (notification == null) return this.NotFound("Notification");

        Template<TId>? templateEntity = await _workService._templateService.GetEntityById(template.Id);
        if (templateEntity != null) return this.AlreadyExists("Template");

        templateEntity = await _workService._templateService.InsertEntity(template);
        if (templateEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await SetTemplateById(id, templateEntity.Id);
    }

    [HttpGet("{id}/filter/{filter_id}")]
    public async Task<ActionResult<TEntity>> SetFilterById(TId id, TId filter_id)
    {
        TEntity? notification = await _ownService.GetEntityById(id);
        if (notification == null) return this.NotFound("Notification");

        Filter<TId>? filter = await _workService._filterService.GetEntityById(filter_id);
        if (filter == null) return this.NotFound("Filter");

        notification.Filter = filter;
        notification.FilterId = filter_id;

        notification = await _ownService.UpdateEntity(id, notification);
        if (notification == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(notification);
    }

    [HttpPost("{id}/filter")]
    public async Task<ActionResult<TEntity>> SetFilter(TId id, Filter<TId> filter)
    {
        TEntity? notification = await _ownService.GetEntityById(id);
        if (notification == null) return this.NotFound("Notification");

        Filter<TId>? filterEntity = await _workService._filterService.GetEntityById(filter.Id);
        if (filterEntity != null) return this.AlreadyExists("Filter");

        filterEntity = await _workService._filterService.InsertEntity(filter);
        if (filterEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await SetTemplateById(id, filterEntity.Id);
    }
}
