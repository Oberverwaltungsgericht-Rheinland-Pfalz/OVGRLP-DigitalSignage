using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing ClientVersions
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class ClientVersionController(ILogger<ClientVersionController> logger, IWorkService workService) : IBaseController<ClientVersion>(logger, workService)
{
    /// <summary>
    /// Downloads the latest ClientVersion Binary
    /// </summary>
    /// <param name="platformId">The desired OS for the Binary</param>
    /// <returns>A Executable File</returns>
    [HttpGet("latest/{platformId}")]
    public async Task<ActionResult> GetLatestClientBinary(PlatformID platformId)
    {
        var clientVersions = await _repository.GetEntities();
        var versionsOrdered = clientVersions
            .Where(cv => cv.PlatformID == platformId)
            .OrderBy(cv => cv.Version);
        var lastVersion = versionsOrdered.Last();
        if (lastVersion == null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        var fileName = $"dsClient-v{lastVersion.Version}" + (platformId == PlatformID.Win32NT ? ".exe" : "");

        // If File is not stored in Database
        if (!string.IsNullOrEmpty(lastVersion.Path))
        {
            if (System.IO.File.Exists(lastVersion.Path))
            {
                var fileData = System.IO.File.ReadAllBytes(lastVersion.Path);
                lastVersion.Data = fileData;
            }
        }

        return File(lastVersion.Data, platformId switch
        {
            PlatformID.Win32NT => "application/x-msdownload",
            PlatformID.Unix => "application/octet-stream",
            _ => "application/x-msdownload"
        }, fileName);
    }

    /// <summary>
    /// Checks if the Server has a newer Version of the Client binary
    /// </summary>
    /// <param name="platformId">The Platform to check for</param>
    /// <param name="version">The Version that the client is currently running</param>
    /// <returns></returns>
    [HttpGet("updates/{platformId}/{version}")]
    public async Task<ActionResult> CheckForUpdates(PlatformID platformId, string version)
    {
        var clientVersions = await _repository.GetEntities();
        var versionsOrdered = clientVersions
            .Where(cv => cv.PlatformID == platformId)
            .OrderBy(cv => cv.Version);
        var latestVersion = versionsOrdered.Last();
        if (latestVersion == null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        Version clientVersion = Version.Parse(version);
        Version serverVersion = Version.Parse(latestVersion.Version);

        if (serverVersion > clientVersion)
        {
            var fileName = $"dsClient-v{latestVersion.Version}" + (platformId == PlatformID.Win32NT ? ".exe" : "");

            // If File is not stored in Database
            if (!string.IsNullOrEmpty(latestVersion.Path))
            {
                if (System.IO.File.Exists(latestVersion.Path))
                {
                    var fileData = System.IO.File.ReadAllBytes(latestVersion.Path);
                    latestVersion.Data = fileData;
                }
            }

            return File(latestVersion.Data, platformId switch
            {
                PlatformID.Win32NT => "application/x-msdownload",
                PlatformID.Unix => "application/octet-stream",
                _ => "application/x-msdownload"
            }, fileName);
        }

        return NoContent();
    }
}