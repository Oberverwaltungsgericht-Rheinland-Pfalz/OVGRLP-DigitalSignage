using System.Collections.Generic;
using System.Web.Http;

namespace DigitalSignage.DisplayControl.Controllers
{
  [RoutePrefix("api")]
  public class DisplayController : ApiController
  {
    [Route("")]
    public string Get()
    {
      return "Hallo Welt";
    }
  }
}
