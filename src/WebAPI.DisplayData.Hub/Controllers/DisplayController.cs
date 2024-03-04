using Microsoft.AspNetCore.Mvc;
using Core.Extensions;
using Core.Models;
using Core.Models.Json;

namespace Services.Display.Controllers;

/// <summary>
/// Controller for Displays to retrieve important Data like Events
/// </summary>
/// <param name="logger"></param>
/// <param name="config"></param>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class DisplayController(ILogger<DisplayController> logger, IConfiguration config) : Controller
{
    protected readonly IConfiguration _config = config;
    protected readonly ILogger _logger = logger;
    protected HttpClient _httpClient = new();

    /// <summary>
    /// Endpoint for Loading all the Events that should be displayed on this Display
    /// </summary>
    /// <param name="id">The Id of the Display</param>
    /// <returns>A List of Events to display</returns>
    [HttpGet("events/{id}")]
    public async Task<ActionResult<ICollection<Event>>> GetEventsForDisplay(Guid id)
    {
        var display = await HttpExtensions.HttpGetSingleAsync<Core.Models.Display>(_httpClient, $"{_config["Services:Displays"]}/{id}");
        if (display == null || display.Id != id)
            return BadRequest("Either the Display with this id was not found or had an error retrieving it");

        List<FilterDataJson> filters = [];

        if (display.RoomId != null)
        {
            FilterDataJson roomFilter = new()
            {
                Type = FilterType.Rooms,
                Targets = [(Guid)display.RoomId],
                FilterMode = FilterMode.Exclusive
            };
            _logger.LogDebug("Add Roomfilter to filters ... {roomFilter}", roomFilter);
            filters.Add(roomFilter);
        }

        if (display.GroupId != null && display.Group?.FilterId != null)
        {
            _logger.LogDebug("Add filter from Groups to filters ...");
            filters.AddRange(display.Group?.Filter?.Data ?? []);
        }

        if (display.FilterId != null)
        {
            _logger.LogDebug("Add filter from Display to filters ...");
            filters.AddRange(display.Filter?.Data ?? []);
        }

        _logger.LogDebug("Final FilterData: {filters}", filters);

        var events = await HttpExtensions.HttpPostMultipleAsync<Event>(_httpClient, $"{_config["Services:Events"]}/filter", filters);

        return Ok(events);
    }

    /// <summary>
    /// Endpoint for Loading the Template for an given Display.
    /// The Assigned Template for a Display is forced against group Templates.
    /// </summary>
    /// <param name="id">The Id of a Display</param>
    /// <returns>A Template Entity or nothing</returns>
    [HttpGet("template/{id}")]
    public async Task<ActionResult<Template>> GetTemplate(Guid id)
    {
        var display = await HttpExtensions.HttpGetSingleAsync<Core.Models.Display>(_httpClient, $"{_config["Services:Displays"]}/{id}");
        if (display == null || display.Id != id)
            return BadRequest("Either the Display with this id was not found or had an error retrieving it");

        if (display.TemplateId != null)
            return Ok(display.Template);

        if (display.GroupId != null && display.Group?.TemplateId != null)
            return Ok(display.Group?.Template);

        return NoContent();
    }

    /// <summary>
    /// Updates the DisplayStatus. This should be done only via the Client-Software running on the Display
    /// </summary>
    /// <param name="id">The Id of a Display</param>
    /// <param name="status">The Sttauscode to set</param>
    /// <returns>The updated Display Entity</returns>
    [HttpGet("status/{id}/{status}")]
    public async Task<ActionResult<Core.Models.Display>> SetStatus(Guid id, DisplayStatus status)
    {
        var display = await HttpExtensions.HttpGetSingleAsync<Core.Models.Display>(_httpClient, $"{_config["Services:Displays"]}/{id}/{status}");
        if (display == null || display.Id != id)
            return BadRequest("Either the Display with this id was not found or had an error retrieving it");

        return Ok(display);
    }

    /// <summary>
    /// Checks if a newer ClientVersion is available
    /// </summary>
    /// <param name="fromVersion">The Version String to check against</param>
    /// <returns>The new Cleint binary</returns>
    [HttpGet("update/{fromVersion}")]
    [Produces("application/octet-stream")]
    public async Task<ActionResult> CheckClientForUpdate(string fromVersion)
    {
        var clientVersions = await HttpExtensions.HttpGetMultipleAsync<ClientVersion>(_httpClient, _config["Services:ClientVersions"]);
        var versionsOrdered = clientVersions.OrderBy(cv => cv.Version);
        var latestVersion = new Version(versionsOrdered.Last().Version);
        var compareVersion = new Version(fromVersion);

        if (fromVersion.CompareTo(latestVersion) < 0)
            return Redirect($"{_config["Services:ClientVersions"]}/latest");

        return NoContent();
    }
}
