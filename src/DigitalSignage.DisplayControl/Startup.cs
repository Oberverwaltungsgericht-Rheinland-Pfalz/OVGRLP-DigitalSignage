using Owin;
using System.Web.Http;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin;
using System.Reflection;
using System.IO;
using System;

namespace DigitalSignage.DisplayControl
{
  public class Startup
  {
    public void Configuration(IAppBuilder appBuilder)
    {
      HttpConfiguration config = new HttpConfiguration();
      config.MapHttpAttributeRoutes();

      appBuilder.UseWebApi(config);

      appBuilder.UseFileServer(new FileServerOptions()
      {
        EnableDefaultFiles = true,
        EnableDirectoryBrowsing = false,
        RequestPath = new PathString(""),
        FileSystem = new PhysicalFileSystem(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "web"))
      });
    }
  }
}
