using Newtonsoft.Json.Serialization;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DigitalSignage.WebApi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
      formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      formatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

      // enable cross-origin resource sharing
      var cors = new EnableCorsAttribute(origins: "http://localhost:4202", headers: "*", methods: "*");
      config.EnableCors(cors);

      config.MapHttpAttributeRoutes();
    }
  }
}