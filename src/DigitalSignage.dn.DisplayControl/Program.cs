// SPDX-FileCopyrightText: © 2023 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.dn.DisplayControl;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

IOperatingSystem os = DetectOS();

app.UseFileServer();

app.MapGet("/api/shutdown", () => os.Shutdown);
app.MapGet("/api/restart", () => os.Restart);
app.MapGet("/api/screenshot", (HttpContext context) => os.Screenshot);

app.UseHealthChecks("/health");
app.Run();

IOperatingSystem DetectOS()
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return new WindowsOperatingSystem();

    if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        return new LinuxOperatingSystem();

    if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        throw new NotImplementedException("MacOS ist zurzeit nicht implementiert!");

    // Für den Fall, dass das Betriebssystem nicht gefunden werden kann
    Console.Error.WriteLine("Unbekanntes Betriebssystem. Unterstützt werden nur Windows und Linux");
    throw new NotImplementedException("MacOS ist zurzeit nicht implementiert!");
}