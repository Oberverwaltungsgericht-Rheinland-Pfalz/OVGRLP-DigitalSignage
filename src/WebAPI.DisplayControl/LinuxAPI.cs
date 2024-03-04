using System.Drawing.Imaging;
using System.Drawing;
using System.Text;
using System.Diagnostics;

namespace CLI.Client;

public class LinuxAPI : IBaseAPI
{
    public IResult Restart()
    {
        try
        {
            // TODO: Maybe find a way to not use sudo
            // var result = "echo Benutzer1 | sudo -S reboot".Bash();
            return TypedResults.Ok("");
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
            // TODO: Maybe find a way to not use sudo
            // var result = "echo Benutzer1 | sudo -S shutdown".Bash();
            return TypedResults.Ok("");
        }
        catch (Exception ex)
        {
            return TypedResults.Problem(ex.Message);
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen", Justification = "<Ausstehend>")]
    public async Task Screenshot(HttpContext context)
    {
        try
        {
            ProcessStartInfo psi = new()
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"xdpyinfo | grep -oP 'dimensions:\\s+\\K\\S+'\"",
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false
            };

            Process? process = Process.Start(psi);

            string screenDimensions = process?.StandardOutput.ReadToEnd() ?? "";
            process?.WaitForExit();

            string[] screenDimensionsSplit = screenDimensions.Split("x");

            if (screenDimensionsSplit.Length < 2) 
            {
                return;
            }

            int width = Convert.ToInt32(screenDimensionsSplit[0]);
            int height = Convert.ToInt32(screenDimensionsSplit[1]);

            using Bitmap screenshot = new(width, height, PixelFormat.Format32bppArgb);

            using var capturedGraphic = Graphics.FromImage(screenshot);
            capturedGraphic.CopyFromScreen(0, 0, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

            using MemoryStream memStream = new();
            screenshot.Save(memStream, ImageFormat.Jpeg);

            memStream.Position = 0;
            byte[] byteArray = memStream.ToArray();

            context.Response.ContentType = "image/jpeg";
            await context.Response.Body.WriteAsync(byteArray);
        }
        catch (Exception ex)
        {
            context.Response.ContentType = "image/jpeg";
            context.Response.StatusCode = 500;

            await context.Response.Body.WriteAsync(Encoding.UTF8.GetBytes("Fehlerbei Screenshot Aufnahme " + ex.Message).AsMemory(0, 0));
        }
        finally
        {
            await context.Response.CompleteAsync();
        }
    }

}
