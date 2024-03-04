using Core.Extensions;
using Core.Models;

namespace CLI.Register.Services;

/// <summary>
/// Class for handling communication with APIs
/// </summary>
public class APIService
{
    /// <summary>
    /// Creates an Display Entity in the Database
    /// </summary>
    /// <param name="httpClient">HttpClient to run the Request from</param>
    /// <param name="url">The Endpoint to call</param>
    /// <param name="devicedata">The Data for the Display</param>
    /// <returns>The Display Entity</returns>
    public static Display? RegisterDisplay(HttpClient httpClient, string? url, NetworkDeviceData devicedata)
    {
        // Insert the Device into the Database...
        var displayTask = HttpExtensions.HttpPostSingleAsync<Display>(httpClient, url, new Display
        {
            Name = devicedata.Hostname,
            IpStr = devicedata.IpAddr,
            MacStr = devicedata.MacAddr,
            Status = DisplayStatus.Registered,
        });
        displayTask.Wait();

        if (!displayTask.IsCompletedSuccessfully)
            return null;

        // Return Display object
        return displayTask.Result;
    }

    /// <summary>
    /// Deletes an Display from the Database
    /// </summary>
    /// <param name="httpClient">HttpClient to run the Request from</param>
    /// <param name="url">The Endpoint to call</param>
    /// <returns>bool on success; false otherwise</returns>
    public static bool DeleteDisplay(HttpClient httpClient, string? url)
    {
        // Delete the Display if the Registry-Value could not be set...
        var deleteTask = HttpExtensions.HttpDeleteAsync(httpClient, url);
        deleteTask.Wait();

        if (!deleteTask.IsCompletedSuccessfully)
            return false;

        return deleteTask.Result;
    }

    /// <summary>
    /// Download the client binary from an Endpoint and write it to disk
    /// </summary>
    /// <param name="httpClient">HttpClient to run the Request from</param>
    /// <param name="url">The Endpoint to call</param>
    /// <param name="targetFolder">The Folder to write the File to</param>
    /// <param name="os">The target os</param>
    /// <returns>A HttpFile struct</returns>
    public static HttpFile? DownloadClientBinary(HttpClient httpClient, string? url, string targetFolder)
    {
        var clientTask = HttpExtensions.HttpGetFileSingleAsync(httpClient, url);
        clientTask.Wait();

        if (!clientTask.IsCompletedSuccessfully)
            return null;

        // Write File to Disk
        var fileName = Path.Combine(targetFolder, clientTask.Result?.FileName ?? "");

        // Delete the File if it already exists
        if (File.Exists(fileName))
            File.Delete(fileName);

        using (var f = File.Create(fileName))
        {
            f.Write(clientTask.Result?.Data ?? [], 0, clientTask.Result?.Data.Length ?? 0);
        }

        return clientTask.Result;
    }
}
