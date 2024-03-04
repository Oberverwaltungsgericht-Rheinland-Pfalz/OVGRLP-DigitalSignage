using Microsoft.AspNetCore.Mvc;
using Services.Database.Services;
using Core.Models;

namespace Services.Database.Controllers;

/// <summary>
/// Controller for Managing RegisterVersions
/// </summary>
[ApiController]
[Route("/v1/[controller]")]
[Produces("application/json")]
public class RegisterVersionController(ILogger<RegisterVersionController> logger, IWorkService workService) : IBaseController<RegisterVersion>(logger, workService)
{
    /// <summary>
    /// Downloads the latest RegisterVersion Binary
    /// </summary>
    /// <param name="platformId">The desired OS for the Binary</param>
    /// <returns>A Executable File</returns>
    [HttpGet("latest/{platformId}")]
    public async Task<ActionResult> GetLatestRegisterBinary(PlatformID platformId)
    {
        var registerVersions = await _repository.GetEntities();
        var versionsOrdered = registerVersions
            .Where(rv => rv.PlatformID == platformId)
            .OrderBy(rv => rv.Version);
        var lastVersion = versionsOrdered.Last();
        if (lastVersion == null)
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);

        var fileName = $"dsRegister-v{lastVersion.Version}" + (platformId == PlatformID.Win32NT ? ".exe" : "");

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
}