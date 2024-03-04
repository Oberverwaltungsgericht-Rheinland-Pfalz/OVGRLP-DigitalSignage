using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;
using Core.Models.Json;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Templates
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class TemplateController(ILogger<TemplateController> logger, IWorkService workService) : IBaseController<Template>(logger, workService)
{

    /// <summary>
    /// Endpoint for setting the Html-Data of a Template
    /// </summary>
    /// <param name="id">The Id of the Template</param>
    /// <param name="html">A List of TemplateHtmlJson that should be set</param>
    /// <returns>A Template Entity</returns>
    [HttpPost("{id}/html")]
    public async Task<ActionResult<Template?>> SetHtmlData(Guid id, List<TemplateHtmlJson> html)
    {
        Template? template = await _repository.GetEntityById(id);
        if (template == null) return NotFound("A Template with this Id was not found");

        template.Html = html;
        var t = await _repository.UpdateEntity(template);
        if (!t) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(template);
    }

    /// <summary>
    /// Endpoint for setting the Css-Data of a Template
    /// </summary>
    /// <param name="id">The Id of the Template</param>
    /// <param name="css">A List of TemplateCssJson that should be set</param>
    /// <returns>A Template Entity</returns>
    [HttpPost("{id}/css")]
    public async Task<ActionResult<Template?>> SetCssData(Guid id, List<TemplateCssJson> css)
    {
        Template? template = await _repository.GetEntityById(id);
        if (template == null) return NotFound("A Template with this Id was not found");

        template.Css = css;
        var t = await _repository.UpdateEntity(template);
        if (!t) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(template);
    }
}
