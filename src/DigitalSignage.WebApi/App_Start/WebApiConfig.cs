using Newtonsoft.Json.Serialization;
using System.Web.Http;

namespace DigitalSignage.WebApi
{
  public static class WebApiConfig
  {
    public static void Register(HttpConfiguration config)
    {
      var formatter = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
      formatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
      formatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

      config.MapHttpAttributeRoutes();
    }
  }
}