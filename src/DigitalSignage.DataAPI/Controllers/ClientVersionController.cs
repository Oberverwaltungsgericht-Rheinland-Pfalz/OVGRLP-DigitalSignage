// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = ClientVersion<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing ClientVersions
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class ClientVersionController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public ClientVersionController(ILogger<ClientVersionController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._clientVersionService)
    {
    }
}
