using AutoMapper;
using DigitalSignage.WebApi.Services;
using System;
using System.Web.Http;
using System.Web.Mvc;

namespace DigitalSignage.WebApi
{
  public class Global : System.Web.HttpApplication
  {
    protected void Application_Start(object sender, EventArgs e)
    {
      AreaRegistration.RegisterAllAreas();
      GlobalConfiguration.Configure(WebApiConfig.Register);

      Mapper.Initialize(cfg =>
      {
        cfg.CreateMap<Infrastructure.Models.Settings.Display, Controllers.Settings.DisplayDto>();
      });
    }

    protected void Application_AuthorizeRequest(object sender, EventArgs e)
    {
      if (Properties.Settings.Default.checkPermissions && null != Request.LogonUserIdentity)
      {
        var permService = new PermissionService(Request.LogonUserIdentity);
        if (!permService.checkPermission(Request.Path, Request.HttpMethod))
        {
          Context.Response.StatusCode = 403;
          Context.Response.StatusDescription = string.Format("User '{0}' hat keine Berechtigung für Ressource '{1}'", Request.LogonUserIdentity.Name, Request.Path);
          Context.Response.End();
        }
      }
    }
  }
}