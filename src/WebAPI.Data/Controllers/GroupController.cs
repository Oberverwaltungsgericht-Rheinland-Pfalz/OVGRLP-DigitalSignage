using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Groups
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class GroupController(ILogger<GroupController> logger, IWorkService workService) : IBaseController<Group>(logger, workService)
{

    /// <summary>
    /// Endpoint for setting the Template for a Group by Id
    /// </summary>
    /// <param name="id">The Id of the Group</param>
    /// <param name="template_id">The Id of the Template that should be assigned</param>
    /// <returns>A Group Entity</returns>
    [HttpGet("{id}/template/{template_id}")]
    public async Task<ActionResult<Group>> SetTemplateById(Guid id, Guid template_id)
    {
        Group? group = await _repository.GetEntityById(id);
        if (group == null) return NotFound("A Group with this Id was not found");

        var templateRepo = _workService.Repository<Template>();
        if (templateRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Template? template = await templateRepo.GetEntityById(template_id);
        if (template == null) return NotFound("A Template with this Id was not found");

        group.Template = template;
        group.TemplateId = template_id;

        var g = await _repository.UpdateEntity(group);
        if (!g) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(group);
    }

    /// <summary>
    /// Endpoint for setting the Template for a Group by Entity
    /// </summary>
    /// <param name="id">The Id of the Group</param>
    /// <param name="template">The Entity that should be created and assigned</param>
    /// <returns>A Group Entity</returns>
    [HttpPost("{id}/template")]
    public async Task<ActionResult<Group>> SetTemplate(Guid id, Template template)
    {
        Group? group = await _repository.GetEntityById(id);
        if (group == null) return NotFound("A Group with this Id was not found");

        var templateRepo = _workService.Repository<Template>();
        if (templateRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Template? templateEntity = await templateRepo.GetEntityById(template.Id);
        if (templateEntity != null) return BadRequest("A Template with this Id was not found");

        templateEntity = await templateRepo.InsertEntity(template);
        if (templateEntity == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        // TODO: Replace (Performance for Debugging is sufficient, but for Production we dont need to check the existence of the Entities twice)
        return await SetTemplateById(id, templateEntity.Id);
    }

    /// <summary>
    /// Endpoint for setting the Filter for a Group by Id
    /// </summary>
    /// <param name="id">The Id of the Group</param>
    /// <param name="filter_id">The Id of the Filter that should be assigned</param>
    /// <returns>A Group Entity</returns>
    [HttpGet("{id}/filter/{filter_id}")]
    public async Task<ActionResult<Group>> SetFilterById(Guid id, Guid filter_id)
    {
        Group? group = await _repository.GetEntityById(id);
        if (group == null) return NotFound("A Template with this Id was not found");

        var filterRepo = _workService.Repository<Filter>();
        if (filterRepo == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Filter? filter = await filterRepo.GetEntityById(filter_id);
        if (filter == null) return NotFound("A Filter with this Id was not found");

        group.Filter = filter;
        group.FilterId = filter_id;

        var g = await _repository.UpdateEntity(group);
        if (!g) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(group);
    }

    /// <summary>
    /// Endpoint for setting the Filter for a Group by Entity
    /// </summary>
    /// <param name="id">The Id of the Group</param>
    /// <param name="filter">The Entity of the Filter that should be created and assigned</param>
    /// <returns>A Group Entity</returns>
    [HttpPost("{id}/filter")]
    public async Task<ActionResult<Group>> SetFilter(Guid id, Filter filter)
    {
        Group? group = await _repository.GetEntityById(id);
        if (group == null) return NotFound("A Group with this Id was not found");

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
