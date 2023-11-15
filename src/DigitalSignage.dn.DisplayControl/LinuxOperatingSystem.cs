// SPDX-FileCopyrightText: © 2023 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

namespace DigitalSignage.dn.DisplayControl;

internal class LinuxOperatingSystem : IOperatingSystem
{
    public IResult Restart()
    {
        try
        {
            var result = "echo Benutzer1 | sudo -S reboot".Bash();
            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            return TypedResults.Conflict(ex.Message);
        }
    }

    public IResult Shutdown()
    {
        try
        {
            var result = "echo Benutzer1 | sudo -S shutdown".Bash();
            return TypedResults.Ok(result);
        }
        catch (Exception ex)
        {
            return TypedResults.Conflict(ex.Message);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen", Justification = "<Ausstehend>")]
    public async Task Screenshot(HttpContext context)
    {
        try
        {
            string screenDimensions = "xdpyinfo  | grep -oP 'dimensions:\\s+\\K\\S+'".Bash();
            string[] screenDimensionsSplit = screenDimensions.Split("x");

            int width = Convert.ToInt32(screenDimensionsSplit[0]);
            int height = Convert.ToInt32(screenDimensionsSplit[1]);

            using Bitmap screenshot = new Bitmap(width, height, PixelFormat.Format32bppArgb);

            using var capturedGraphic = Graphics.FromImage(screenshot);
            capturedGraphic.CopyFromScreen(0, 0, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

            using MemoryStream memStream = new MemoryStream();
            screenshot.Save(memStream, ImageFormat.Jpeg);

            memStream.Position = 0;
            byte[] byteArray = memStream.ToArray();

            context.Response.ContentType = "image/jpeg";
            await context.Response.Body.WriteAsync(byteArray, 0, byteArray.Length);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "image/jpeg";
            context.Response.StatusCode = 500;

            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Fehlerbei Screenshot Aufnahme " + ex.Message), 0, 0);
        }
        finally
        {
            await context.Response.CompleteAsync();
        }
    }
}