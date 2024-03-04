using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing EventChanges
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class EventChangeController(ILogger<EventChangeController> logger, IWorkService workService) : IBaseController<EventChange>(logger, workService)
{
}
