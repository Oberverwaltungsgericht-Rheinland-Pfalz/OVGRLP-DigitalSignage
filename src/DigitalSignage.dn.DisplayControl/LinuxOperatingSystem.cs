// SPDX-FileCopyrightText: © 2023 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.AspNetCore.Http.HttpResults;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace DigitalSignage.dn.DisplayControl;

internal class LinuxOperatingSystem : IOperatingSystem
{
    public async Task<Ok<string>> Restart()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = @"/bin/bash",
            Arguments = @"echo Benutzer1 | sudo -S reboot",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return TypedResults.Ok(result);
        }
        // "echo Benutzer1 | sudo -S reboot".Bash();
    }

    public async Task<Ok<string>> Shutdown()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = @"/bin/bash",
            Arguments = @"echo Benutzer1 | sudo -S shutdown",
            CreateNoWindow = true,
            RedirectStandardOutput = true,
            UseShellExecute = false
        };

        using (var process = new Process { StartInfo = startInfo })
        {
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return TypedResults.Ok(result);
        }

        // "echo Benutzer1 | sudo -S shutdown".Bash();
    }

    public async Task Screenshot(HttpContext context)
    {
        try
        {
            string screenDimensions = "xdpyinfo  | grep -oP 'dimensions:\\s+\\K\\S+'".Bash();
            string[] screenDimensionsSplit = screenDimensions.Split("x");

            int width = Convert.ToInt32(screenDimensionsSplit[0]);
            int height = Convert.ToInt32(screenDimensionsSplit[1]);

            Bitmap screenshot = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using var capturedGraphic = Graphics.FromImage(screenshot);
            capturedGraphic.CopyFromScreen(0, 0, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

            MemoryStream memStream = new MemoryStream();
            screenshot.Save(memStream, ImageFormat.Jpeg);

            memStream.Position = 0;
            byte[] byteArray = memStream.ToArray();

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
}