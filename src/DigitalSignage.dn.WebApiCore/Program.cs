// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2

using DigitalSignage.dn.WebApiCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
// builder.Services.AddHttpForwarder();

// Add services to the container.
builder.Services.AddControllers();

var startup = new Startup(builder).AddDependencyInjection();
var app = builder.Build();

startup.ConfigureServices(app);
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseDefaultFiles(); //UseStaticFiles();

app.UseRouting();
app.UseAuthorization();
app.UseSystemWebAdapters();

app.MapDefaultControllerRoute();
//app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();
