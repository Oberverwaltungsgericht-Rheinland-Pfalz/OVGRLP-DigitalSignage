// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Services.DataServices;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.DataAPI.Controllers;

using TId = Guid;
using TEntity = Department<Guid>;
using TDbContext = ApplicationDbContext;

/// <summary>
/// Controller for Managing Departments
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class DepartmentController : IBaseController<TDbContext, BaseService<TDbContext, TEntity, TId>, TEntity, TId>
{
    public DepartmentController(ILogger<DepartmentController> logger, IWorkService<TDbContext, TId> workService) : base(logger, workService, workService._departmentService)
    {
    }
}
