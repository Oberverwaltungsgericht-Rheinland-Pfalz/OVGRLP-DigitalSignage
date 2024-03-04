using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Notifications
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class NotificationController(ILogger<NotificationController> logger, IWorkService workService) : IBaseController<Notification>(logger, workService)
{

    /// <summary>
    /// Endpoint for setting the Template for an Notification by Id
    /// </summary>
    /// <param name="id">Id of the Notification</param>
    /// <param name="template_id">Id of the Template that should be assigned</param>
    /// <returns>The Notification Entity</returns>
    [HttpGet("{id}/template/{template_id}")]
    public async Task<ActionResult<Notification>> SetTemplateById(Guid id, Guid template_id)
    {
        Notification? notification = await _repository.GetEntityById(id);
        if (notification == null) return NotFound("A Notification with this Id was not found");

        var templateRepo = _workService.Repository<Template>();
        if (templateRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Template? template = await templateRepo.GetEntityById(template_id);
        if (template == null) return NotFound("A Template with this Id was not found");

        notification.Template = template;
        notification.TemplateId = template_id;

        var n = await _repository.UpdateEntity(notification);
        if (!n) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(notification);
    }

    /// <summary>
    /// Endpoint for setting the Template for an Notification by Entity
    /// </summary>
    /// <param name="id">Id of the Notification</param>
    /// <param name="template">The Template Entity that should be created and assigned</param>
    /// <returns>A Notification Entity</returns>
    [HttpPost("{id}/template")]
    public async Task<ActionResult<Notification>> SetTemplate(Guid id, Template template)
    {
        Notification? notification = await _repository.GetEntityById(id);
        if (notification == null) return NotFound("A Notification with this Id was not found");

        var templateRepo = _workService.Repository<Template>();
        if (templateRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Template? templateEntity = await templateRepo.GetEntityById(template.Id);
        if (templateEntity != null) return BadRequest("A Template with this Id already exists");

        templateEntity = await templateRepo.InsertEntity(template);
        if (templateEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await SetTemplateById(id, templateEntity.Id);
    }

    /// <summary>
    /// Endpoint for setting the Filter for an Notification by Id
    /// </summary>
    /// <param name="id">Id of the Notification</param>
    /// <param name="filter_id">The Filter Id that should be assigned</param>
    /// <returns>A Notification Entity</returns>
    [HttpGet("{id}/filter/{filter_id}")]
    public async Task<ActionResult<Notification>> SetFilterById(Guid id, Guid filter_id)
    {
        Notification? notification = await _repository.GetEntityById(id);
        if (notification == null) return NotFound("A Notification with this Id was not found");

        var filterRepo = _workService.Repository<Filter>();
        if (filterRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Filter? filter = await filterRepo.GetEntityById(filter_id);
        if (filter == null) return NotFound("A Filter with this Id was not found");

        notification.Filter = filter;
        notification.FilterId = filter_id;

        var n = await _repository.UpdateEntity(notification);
        if (!n) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(notification);
    }

    /// <summary>
    /// Endpoint for setting the Filter for an Notification by Entity
    /// </summary>
    /// <param name="id">Id of the Notification</param>
    /// <param name="filter">The Filter Enitty that should be created and assigned</param>
    /// <returns>A Notification Entity</returns>
    [HttpPost("{id}/filter")]
    public async Task<ActionResult<Notification>> SetFilter(Guid id, Filter filter)
    {
        Notification? notification = await _repository.GetEntityById(id);
        if (notification == null) return NotFound("A Notification with this Id was not found");

        var filterRepo = _workService.Repository<Filter>();
        if (filterRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Filter? filterEntity = await filterRepo.GetEntityById(filter.Id);
        if (filterEntity != null) return BadRequest("A Filter with this Id already exists");

        filterEntity = await filterRepo.InsertEntity(filter);
        if (filterEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await SetTemplateById(id, filterEntity.Id);
    }
}
