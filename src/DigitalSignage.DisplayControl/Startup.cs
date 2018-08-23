using Owin;
using System.Web.Http;
using Microsoft.Owin.StaticFiles;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin;
using System.Reflection;
using System.IO;
using System;
using System.Web.Http.Cors;

namespace DigitalSignage.DisplayControl
{
  public class Startup
  {
    public void Configuration(IAppBuilder appBuilder)
    {
      HttpConfiguration config = new HttpConfiguration();
      config.MapHttpAttributeRoutes();

      // enable cross-origin resource sharing
      var cors = new EnableCorsAttribute(origins: "*", headers: "*", methods: "*");
      config.EnableCors(cors);

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