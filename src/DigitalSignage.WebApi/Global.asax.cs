﻿using System;
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
    }
  }
}