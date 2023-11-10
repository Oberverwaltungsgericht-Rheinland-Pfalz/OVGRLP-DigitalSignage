var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseFileServer();

app.MapGet("/a", () => "Hello World!");

app.Run();
