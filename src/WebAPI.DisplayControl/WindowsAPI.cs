using System.Diagnostics;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;

namespace CLI.Client;

public class WindowsAPI : IBaseAPI
{
    public IResult Restart()
    {
        Process.Start("shutdown", "/r /t 3");
        return Results.Ok();
    }

    public IResult Shutdown()
    {
        var process = Process.Start("shutdown", "/s /t 3");
        return Results.Ok("");
    }

    [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
    public static extern IntPtr GetDesktopWindow();

    [DllImport("gdi32.dll")]
    public static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

    private enum DeviceCap
    {
        DESKTOPVERTRES = 117,
        DESKTOPHORZRES = 118
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Plattformkompatibilität überprüfen", Justification = "<Ausstehend>")]
    public async Task Screenshot(HttpContext context)
    {
        try
        {
            IntPtr DesktopHwnd = GetDesktopWindow();
            using Graphics DesktopGr = Graphics.FromHwnd(DesktopHwnd);
            IntPtr DesktopHdc = DesktopGr.GetHdc();
            int XRes = GetDeviceCaps(DesktopHdc, (int)DeviceCap.DESKTOPHORZRES);
            int YRes = GetDeviceCaps(DesktopHdc, (int)DeviceCap.DESKTOPVERTRES);

            using var screenshot = new Bitmap(XRes, YRes, PixelFormat.Format32bppArgb);

            using var capturedGraphic = Graphics.FromImage(screenshot);
            capturedGraphic.CopyFromScreen(0, 0, 0, 0, screenshot.Size, CopyPixelOperation.SourceCopy);

            using MemoryStream memStream = new();
            screenshot.Save(memStream, ImageFormat.Jpeg);

            memStream.Position = 0;
            var byteArray = memStream.ToArray();

            context.Response.ContentType = "image/jpeg";
            context.Response.StatusCode = 200;
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
