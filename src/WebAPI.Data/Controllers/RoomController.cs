using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing Rooms
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class RoomController(ILogger<RoomController> logger, IWorkService workService) : IBaseController<Room>(logger, workService)
{
}
