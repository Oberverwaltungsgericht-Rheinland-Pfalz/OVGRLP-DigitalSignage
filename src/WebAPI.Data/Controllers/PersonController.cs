using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Persons
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class PersonController(ILogger<PersonController> logger, IWorkService workService) : IBaseController<Person>(logger, workService)
{
}
