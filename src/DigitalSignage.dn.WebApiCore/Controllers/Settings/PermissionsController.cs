// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.Web;
using DigitalSignage.WebApi.Services;
using System.Security.Principal;
using DigitalSignage.Infrastructure.Models.Settings;
using DigitalSignage.Data;
using Microsoft.AspNetCore.Mvc;

namespace DigitalSignage.WebApi.Controllers.Settings
{
  [Route("settings/permissions")]
  public class PermissionsController : Controller
  {
    private readonly DigitalSignageDbContext context = new DigitalSignageDbContext();

    [Route("")]
    [HttpGet]
    public IEnumerable<Permission> GetAllPermissions()
    {
      return context.Permissions;
    }

    [Route("CurrentUserMembers")]
    [HttpGet]
    public IEnumerable<string> GetCurrentUserMembers()
    {
      var perm = new BasicPermissions();
      WindowsIdentity wid = HttpContext.Current.Request.LogonUserIdentity;     //new WindowsIdentity(HttpContext.Current.User.Identity.Name);
      var ps = new PermissionService(wid);

      return ps.SecurityMembers;
    }

    [Route("BasicPermissions")]
    [HttpGet]
    public ActionResult<BasicPermissions> GetBasicPermissions()
    {
      var perm = new BasicPermissions();
      WindowsIdentity wid = HttpContext.Current.Request.LogonUserIdentity;     //new WindowsIdentity(HttpContext.Current.User.Identity.Name);
      var ps = new PermissionService(wid);

      if (Properties.Settings.Default.checkPermissions)
      {
        perm.AllowDisplays = ps.checkPermission("settings/displays", "GET");
        perm.AllowDisplaysControl = (ps.checkPermission("settings/displays", "GET") && ps.checkPermission("settings/displays/*/start", "GET"));

        perm.AllowTermine = Restriction.forbidden;
        if (ps.checkPermission("daten/verfahren", "GET"))
        {
          perm.AllowTermine = Restriction.read;
          if (ps.checkPermission("breeze/EurekaDaten", "POST"))
            perm.AllowTermine = Restriction.write;
        }

        perm.AllowNotes = Restriction.forbidden;
        if (ps.checkPermission("breeze/EurekaDaten/Notes", "GET"))
        {
          perm.AllowNotes = Restriction.read;
          if (ps.checkPermission("breeze/EurekaDaten/Notes", "POST"))
            perm.AllowNotes = Restriction.write;
        }
      }
      else
      {
        perm.AllowDisplays = true;
        perm.AllowDisplaysControl = true;
        perm.AllowNotes = Restriction.write;
        perm.AllowTermine = Restriction.write;
      }

      return Ok(perm);
    }

    [Route("GetPermission")]
    [HttpGet]
    public ActionResult<bool> GetPermission(string urlPath)
    {
      string name = GetUPNUserFormat(HttpContext.Current.User.Identity.Name);
      var wid = new WindowsIdentity(name);
      return Ok(PermissionService.CheckPermissionForIdentity(wid, urlPath, "GET"));
    }

    [Route("PutPermission")]
    [HttpGet]
    public ActionResult PutPermission(string urlPath)
    {
      string name = GetUPNUserFormat(HttpContext.Current.User.Identity.Name);
      var wid = new WindowsIdentity(name);
      return Ok(PermissionService.CheckPermissionForIdentity(wid, urlPath, "PUT"));
    }

    [Route("PostPermission")]
    [HttpGet]
    public ActionResult PostPermission(string urlPath)
    {
      string name = GetUPNUserFormat(User.Identity.Name);
      var wid = new WindowsIdentity(name);
      return Ok(PermissionService.CheckPermissionForIdentity(wid, urlPath, "POST"));
    }

    [Route("DeletePermission")]
    [HttpGet]
    public ActionResult DeletePermission(string urlPath)
    {
      string name = GetUPNUserFormat(HttpContext.Current.User.Identity.Name);
      var wid = new WindowsIdentity(name);
      return Ok(PermissionService.CheckPermissionForIdentity(wid, urlPath, "DELETE"));
    }

    // User Principal Name format
    // Übergabeformat: domäne\user
    // Rückgabeformat: user@domäne
    private string GetUPNUserFormat(string name)
    {
      char del = '\\';
      if (name.Contains(del))
        name = name.Split(del).ToArray()[1] + "@" + name.Split(del).ToArray()[0];
      return name;
    }
  }

  public class BasicPermissions
  {
    public bool AllowDisplays;
    public bool AllowDisplaysControl;
    public Restriction AllowTermine;
    public Restriction AllowNotes;
  }

  public enum Restriction { forbidden = 0, read = 1, write = 2 };
}