// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Data.JsonData;
using DigitalSignage.DataAPI.Extensions;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Template<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Templates
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class TemplateController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public TemplateController(ILogger<TemplateController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._templateService)
    {
    }

    [HttpPost("{id}/html")]
    public async Task<ActionResult<TEntity?>> SetHtmlData(TId id, List<TemplateHtmlJson> html)
    {
        TEntity? template = await _ownService.GetEntityById(id);
        if (template == null) return this.NotFound("Template");

        template.Html = html;
        template = await _ownService.UpdateEntity(id, template);
        if (template == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(template);
    }

    [HttpPost("{id}/css")]
    public async Task<ActionResult<TEntity?>> SetCssData(TId id, List<TemplateCssJson> css)
    {
        TEntity? template = await _ownService.GetEntityById(id);
        if (template == null) return this.NotFound("Template");

        template.Css = css;
        template = await _ownService.UpdateEntity(id, template);
        if (template == null) return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(template);
    }
}
