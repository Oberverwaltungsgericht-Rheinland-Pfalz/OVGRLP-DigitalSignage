// SPDX-FileCopyrightText: © 2023 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;

namespace DigitalSignage.dn.DisplayControl;

public class WindowsOperatingSystem : IOperatingSystem
{
    public async Task<Ok<string>> Restart()
    {
        Process.Start("shutdown", "/r /t 3");
        return TypedResults.Ok("");
    }

    public async Task<Ok<string>> Shutdown()
    {
        var process = Process.Start("shutdown", "/s /t 3");
        return TypedResults.Ok("");
    }

    //vncserver -localhost no -geometry 1024x768 -depth 24

    public async Task Screenshot(HttpContext context)
    {
        try
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.Arguments = "/c \"wmic path Win32_VideoController get CurrentHorizontalResolution, CurrentVerticalResolution\"";
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();
            cmd.WaitForExit();

            string screenDimension = cmd.StandardOutput.ReadToEnd();

            MatchCollection matches = Regex.Matches(screenDimension, @"[0-9]{3,}");

            int width = 0;
            int height = 0;

            if (matches.Count >= 2)
            {
                width = Convert.ToInt32(matches[0].Value);
                height = Convert.ToInt32(matches[1].Value);
            }
            else
            {
                throw new Exception("Konnte Bilschirmauflösung nicht ermitteln!");
            }

            Bitmap screenshot = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using var capturedGraphic = Graphics.FromImage(screenshot);
            capturedGraphic.CopyFromScreen(0, 0, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

            using MemoryStream memStream = new MemoryStream();
            screenshot.Save(memStream, ImageFormat.Jpeg);

            memStream.Position = 0;
            var byteArray = memStream.ToArray();

            context.Response.ContentType = "image/jpeg";
            await context.Response.Body.WriteAsync(byteArray, 0, byteArray.Length);
        }
        catch
        {
            context.Response.ContentType = "image/jpeg";
            await context.Response.Body.WriteAsync(null, 0, 0);
            throw new Exception("Konnte keinen Screenshot aufnehmen!");
        }
    }
}