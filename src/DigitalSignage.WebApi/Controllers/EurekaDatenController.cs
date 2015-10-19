using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.WebApi.Data;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Web.Http;

namespace DigitalSignage.WebApi.Controllers
{
  [BreezeController]
  public class EurekaDatenController : ApiController
  {
    private readonly EFContextProvider<DigitalSignageDbContext> contextProvider = new EFContextProvider<DigitalSignageDbContext>();

    // ~/breeze/EurekaDaten/Metadata
    [HttpGet]
    public string Metadata()
    {
      return contextProvider.Metadata();
    }

    // ~/breeze/EurekaDaten/Verfahren
    [HttpGet]
    public IQueryable<Verfahren> Verfahren()
    {
      return contextProvider.Context.Verfahren;
    }

    // ~/breeze/EurekaDaten/SaveChanges
    [HttpPost]
    public SaveResult SaveChanges(JObject saveBundle)
    {
      return contextProvider.SaveChanges(saveBundle);
    }
  }
}