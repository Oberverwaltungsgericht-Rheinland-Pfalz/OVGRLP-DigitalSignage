using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Departments
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class DepartmentController(ILogger<DepartmentController> logger, IWorkService workService) : IBaseController<Department>(logger, workService)
{
}
