// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace DigitalSignage.DisplayControl
{
  public class Program
  {
    static void Main(string[] args)
    {
      string baseAddress = Properties.Settings.Default.BaseAddress;

      using (WebApp.Start<Startup>(url: baseAddress))
      {
        Console.WriteLine(String.Format("WebServer wurde gestartet und ist unter '{0}' erreichbar.", baseAddress));
        Console.ReadLine();
      }
    }
  }
}
