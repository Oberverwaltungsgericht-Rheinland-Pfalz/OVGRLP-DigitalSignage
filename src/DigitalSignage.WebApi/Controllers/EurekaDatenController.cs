using Breeze.ContextProvider;
using Breeze.ContextProvider.EF6;
using Breeze.WebApi2;
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.WebApi.Data;
using DigitalSignage.WebApi.Services;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;

namespace DigitalSignage.WebApi.Controllers
{
  [BreezeController]
  [RoutePrefix("breeze/EurekaDaten")]
  public class EurekaDatenController : ApiController
  {
    private readonly EFContextProvider<DigitalSignageDbContext> contextProvider = new EFContextProvider<DigitalSignageDbContext>();
    private readonly DisplayManagementService displayManagementService = new DisplayManagementService();

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

    // ~/breeze/EurekaDaten/VerfahrenList
    [HttpGet]
    public IQueryable<object> VerfahrenList()
    {
      var query = from v in contextProvider.Context.Verfahren
                    .Include("Stammdaten")
                    .Include("ParteienAktiv")
                    .Include("ParteienPassiv")
                  select new
                  {
                    VerfahrensId = v.VerfahrensId,
                    Az = v.Az,
                    Status = v.Status,
                    UhrzeitPlan = v.UhrzeitPlan,
                    UhrzeitAktuell = v.UhrzeitAktuell,
                    Gericht = v.Stammdaten.Gerichtsname,
                    Datum = v.Stammdaten.Datum,
                    Sitzungssaal = v.Sitzungssaal,
                    ParteienAktiv = v.ParteienAktiv,
                    ParteienPassiv = v.ParteienPassiv
                  };

      return query.AsQueryable();
    }

    // ~/breeze/EurekaDaten/Displays
    [HttpGet]
    public IQueryable<object> Displays()
    {
      return contextProvider.Context.Displays;
    }

    // ~/breeze/EurekaDaten/DisplayStatus
    [HttpGet]
    [Route("Display/{id}/Status")]
    public async Task<IHttpActionResult> DisplayStatus(int id)
    {
      var display = await contextProvider.Context.Displays.FindAsync(id);

      if (display == null)
        return NotFound();

      return Ok((int)displayManagementService.GetDisplayStatus(display));
    }

    // ~/breeze/EurekaDaten/SaveChanges
    [HttpPost]
    public SaveResult SaveChanges(JObject saveBundle)
    {
      return contextProvider.SaveChanges(saveBundle);
    }
  }
}