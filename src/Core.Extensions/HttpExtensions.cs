using System.Net.Http.Json;

namespace Core.Extensions;


public struct HttpFile
{
    public byte[] Data;
    public string FileName;
    public string ContentType;
}

/// <summary>
/// Collection of Functions for Http-Methods
/// </summary>
public class HttpExtensions
{
    /// <summary>
    /// Calls a Http-Get-Endpoint and retrieves a single entity
    /// </summary>
    /// <typeparam name="TEntity">The expected return Type</typeparam>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <returns>The Entity on success; null otherwise</returns>
    public async static Task<TEntity?> HttpGetSingleAsync<TEntity>(HttpClient httpClient, string? url)
    {
        var httpTask = await httpClient.GetAsync(url);
        if (!httpTask.IsSuccessStatusCode)
            return default;

        var entity = await httpTask.Content.ReadFromJsonAsync<TEntity>();

        return entity;
    }

    /// <summary>
    /// Calls a Http-Get-Endpoint and retrieves an File/byte-Array
    /// </summary>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <returns>A HttpFile-Object with FileType, FileData and FileName</returns>
    public async static Task<HttpFile?> HttpGetFileSingleAsync(HttpClient httpClient, string? url)
    {
        var httpTask = await httpClient.GetAsync(url);
        if (!httpTask.IsSuccessStatusCode)
            return null;

        if (httpTask.StatusCode == System.Net.HttpStatusCode.NoContent)
            return null;

        var result = await httpTask.Content.ReadAsStreamAsync();

        var file = new HttpFile
        {
            FileName = httpTask.Content.Headers.ContentDisposition?.FileName ?? "",
            ContentType = httpTask.Content.Headers.ContentType?.MediaType ?? ""
        };

        using MemoryStream ms = new();
        result.CopyTo(ms);
        file.Data = ms.ToArray();

        return file;
    }

    /// <summary>
    /// Calls a Http-Get-Endpoint and retrieves multiple entities
    /// </summary>
    /// <typeparam name="TEntity">The expected return Type</typeparam>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <returns>A List of Entities on success; an empty list otherwise</returns>
    public async static Task<ICollection<TEntity>> HttpGetMultipleAsync<TEntity>(HttpClient httpClient, string? url)
    {
        var httpTask = await httpClient.GetAsync(url);
        if (!httpTask.IsSuccessStatusCode)
            return [];

        var entity = await httpTask.Content.ReadFromJsonAsync<ICollection<TEntity>>();
        if (entity == null)
            return [];

        return entity;
    }

    /// <summary>
    /// Calls a Http-Post-Endpoint and retrieves a single entity
    /// </summary>
    /// <typeparam name="TEntity">The expected return Type</typeparam>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <param name="data">The Data to Post</param>
    /// <returns>The Entity on success; null otherwise</returns>
    public async static Task<TEntity?> HttpPostSingleAsync<TEntity>(HttpClient httpClient, string? url, object data)
    {
        var httpTask = await httpClient.PostAsJsonAsync(url, data);
        if (!httpTask.IsSuccessStatusCode)
            return default;

        var entity = await httpTask.Content.ReadFromJsonAsync<TEntity>();
        if (entity == null)
            return default;

        return entity;
    }

    /// <summary>
    /// Calls a Http-Post-Endpoint and retrieves multiple Entities
    /// </summary>
    /// <typeparam name="TEntity">The expected return Type</typeparam>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <param name="data">The Data to Post</param>
    /// <returns>A List of Entities on success; an empty List otherwise</returns>
    public async static Task<ICollection<TEntity>> HttpPostMultipleAsync<TEntity>(HttpClient httpClient, string? url, object data)
    {
        var httpTask = await httpClient.PostAsJsonAsync(url, data);
        if (!httpTask.IsSuccessStatusCode)
            return [];

        var entity = await httpTask.Content.ReadFromJsonAsync<ICollection<TEntity>>();
        if (entity == null)
            return [];

        return entity;
    }

    /// <summary>
    /// Calls a Http-Delete-Endpoint and retrieves a single Entity
    /// </summary>
    /// <typeparam name="TEntity">The expected return Type</typeparam>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <returns>The Entity on success; null otherwise</returns>
    public async static Task<TEntity?> HttpDeleteSingleAsync<TEntity>(HttpClient httpClient, string? url)
    {
        var httpTask = await httpClient.DeleteAsync(url);
        if (!httpTask.IsSuccessStatusCode)
            return default;

        var entity = await httpTask.Content.ReadFromJsonAsync<TEntity>();
        if (entity == null)
            return default;

        return entity;
    }

    /// <summary>
    /// Calls a Http-Put-Endpoint and retrieves a single Entity
    /// </summary>
    /// <typeparam name="TEntity">The expected return Type</typeparam>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <param name="data">The Entity</param>
    /// <returns>The Entity on success; null otherwise</returns>
    public async static Task<TEntity?> HttpPutSingleAsync<TEntity>(HttpClient httpClient, string? url, object data)
    {
        var httpTask = await httpClient.PutAsJsonAsync(url, data);
        if (!httpTask.IsSuccessStatusCode)
            return default;

        if (httpTask.StatusCode == System.Net.HttpStatusCode.NoContent)
            return (TEntity)data;

        var entity = await httpTask.Content.ReadFromJsonAsync<TEntity>();
        if (entity == null)
            return default;

        return entity;
    }

    /// <summary>
    /// Calls a Http-Delete-Endpoint
    /// </summary>
    /// <param name="httpClient">A Http Client instance</param>
    /// <param name="url">The Url</param>
    /// <returns>The Entity on success; null otherwise</returns>
    public async static Task<bool> HttpDeleteAsync(HttpClient httpClient, string? url)
    {
        var httpTask = await httpClient.DeleteAsync(url);
        if (!httpTask.IsSuccessStatusCode)
            return false;

        return true;
    }
}
