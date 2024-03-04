using Microsoft.AspNetCore.Authentication.Negotiate;
using System.Runtime.InteropServices;
using CLI.Client;
using Core.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var app = builder.Build();

var httpClient = new HttpClient();
var client = await HttpExtensions.HttpGetFileSingleAsync(httpClient, app.Configuration["Services:ClientVersions"]);
// Newer Version was Downloaded...
if (client != null)
{
    // var fileName = Path.Combine(Assembly.GetExecutingAssembly().Location, client.Value.FileName);

    // using var f = File.Create(fileName);
    // f.Write(client.Value.Data, 0, client.Value.Data.Length);

    // TODO: restart new assembly
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

IBaseAPI api = DetectOS();

app.MapGet("/v1/shutdown", () => api.Shutdown()).WithOpenApi();
app.MapGet("/v1/restart", () => api.Restart()).WithOpenApi();
app.MapGet("/v1/screenshot", api.Screenshot).WithOpenApi();

app.UseHealthChecks("/health");
app.Run();

static IBaseAPI DetectOS()
{
    if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return new WindowsAPI();

    // TODO: Implement
    //if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
    //    return new LinuxAPI();

    // Fr den Fall, dass das Betriebssystem nicht gefunden werden kann
    Console.Error.WriteLine("Unbekanntes Betriebssystem. Untersttzt werden nur Windows und Linux");
    throw new Exception();
}
