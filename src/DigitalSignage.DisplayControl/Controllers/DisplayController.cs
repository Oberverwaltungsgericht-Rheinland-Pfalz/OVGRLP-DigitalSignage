// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using System.Windows.Forms;

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

    [Route("screenshot")]
    [HttpGet]
    public HttpResponseMessage Screenshot()
    {
      try
      {
        var result = new HttpResponseMessage(HttpStatusCode.OK);

        Bitmap bitmap = new Bitmap(
          Screen.PrimaryScreen.Bounds.Width,
          Screen.PrimaryScreen.Bounds.Height, 
          PixelFormat.Format32bppArgb);
        Graphics graphics = Graphics.FromImage(bitmap);

        graphics.CopyFromScreen(
          0, 0, 0, 0, 
          new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height), 
          CopyPixelOperation.SourceCopy);

        MemoryStream memStream = new MemoryStream();
        bitmap.Save(memStream, ImageFormat.Jpeg);
        memStream.Position = 0;
        result.Content = new ByteArrayContent(memStream.ToArray());
        result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");

        return result;
      }
      catch
      {
        return new HttpResponseMessage(HttpStatusCode.InternalServerError);
      }
    }
  }
}
