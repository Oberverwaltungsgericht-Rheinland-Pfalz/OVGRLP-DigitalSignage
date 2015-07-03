using DigitalSignage.DisplayControl.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

    [Route("shutdown")]
    [HttpGet]
    public IHttpActionResult Shutdown()
    {
      try
      {
        Process.Start("shutdown", "/s /t 10");
        return Ok();
      }
      catch
      {
        return InternalServerError();
      }
      
    }

    [Route("restart")]
    [HttpGet]
    public IHttpActionResult Restart()
    {
      try
      {
        Process.Start("shutdown", "/r /t 10");
        return Ok();
      }
      catch
      {
        return InternalServerError();
      }
    }
  }
}
