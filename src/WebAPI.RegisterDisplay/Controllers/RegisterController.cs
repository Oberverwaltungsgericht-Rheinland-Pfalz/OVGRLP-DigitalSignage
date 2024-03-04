using Microsoft.AspNetCore.Mvc;
using Core.Extensions;
using Core.Models;

namespace Services.Register.Controllers;

[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class RegisterController(ILogger<RegisterController> logger, IConfiguration config) : Controller
{
    protected readonly IConfiguration _config = config;
    protected readonly ILogger _logger = logger;
    protected HttpClient _httpClient = new();

    /// <summary>
    /// Creates an Display Entity in the Database
    /// </summary>
    /// <returns>A Display Entity</returns>
    [HttpPost]
    public async Task<ActionResult> RegisterDisplay(Display display)
    {
        var dp = await HttpExtensions.HttpGetSingleAsync<Display>(_httpClient, $"{_config["Services:Displays"]}/getByMac/{display.MacStr}");
        if (dp == null)
            return NotFound("A Display with this mac does not exist");

        _logger.LogDebug("Display matched with:\n{dp}", dp);

        display.Id = dp.Id;
        dp = await HttpExtensions.HttpPutSingleAsync<Display>(_httpClient, _config["Services:Displays"], display);

        _logger.LogDebug("Display was updated with:\n{dp}", dp);
        if (dp == null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        return Ok(dp);
    }

    /// <summary>
    /// Downloads the latest Client Binary for the given OS
    /// </summary>
    /// <param name="os">The Client OS</param>
    /// <returns>A Client Binary</returns>
    [HttpGet("client/{os}")]
    public async Task<ActionResult> DownloadClientBinary(PlatformID os)
    {
        return await DownloadFile($"{_config["Services:ClientVersions"]}/latest/{os}");
    }
    /// <summary>
    /// Downloads the latest Register Binary for the given OS
    /// </summary>
    /// <param name="os">The Client OS</param>
    /// <returns>A Register Binary</returns>
    [HttpGet("register/{os}")]
    public async Task<ActionResult> DownloadRegisterBinary(PlatformID os)
    {
        return await DownloadFile($"{_config["Services:RegisterVersions"]}/latest/{os}");
    }

    /// <summary>
    /// Downloads a File from a given URL
    /// </summary>
    /// <param name="url">The Url</param>
    /// <returns>The File</returns>
    private async Task<ActionResult> DownloadFile(string? url)
    {
        var binary = await HttpExtensions.HttpGetFileSingleAsync(_httpClient, url);
        if (binary?.Data == null || binary?.ContentType == "" || binary?.FileName == "")
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        _logger.LogDebug("Filename: {name}", binary?.FileName);
        _logger.LogDebug("Filetype: {type}", binary?.ContentType);
        _logger.LogDebug("Data: {data}", binary?.Data.ToString());

        return File(binary?.Data ?? [], binary?.ContentType ?? "", binary?.FileName);
    }
}
