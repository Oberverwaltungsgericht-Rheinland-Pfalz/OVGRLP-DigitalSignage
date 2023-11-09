// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.Web;
using System.Web.Http;

[assembly: PreApplicationStartMethod(
    typeof(DigitalSignage.WebApi.App_Start.BreezeWebApiConfig), "RegisterBreezePreStart")]
namespace DigitalSignage.WebApi.App_Start {
  ///<summary>
  /// Inserts the Breeze Web API controller route at the front of all Web API routes
  ///</summary>
  ///<remarks>
  /// The [PreApplicationStartMethod] attribute above causes this class to be discovered and run during startup.
  /// Alternatively, you may remove the attribute and call this directly from Application_Start in Global.asax.
  ///</remarks>
  public static class BreezeWebApiConfig {

    public static void RegisterBreezePreStart() {
      GlobalConfiguration.Configuration.Routes.MapHttpRoute(
          name: "BreezeApi",
          routeTemplate: "breeze/{controller}/{action}"
      );
    }
  }
}