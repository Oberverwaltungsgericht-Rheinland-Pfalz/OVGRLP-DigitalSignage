// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2

using DigitalSignage.dn.WebApiCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
builder.Services.AddSystemWebAdapters();
// builder.Services.AddHttpForwarder();


var startup = new Startup(builder);
startup.AddDependencyInjection();
var app = builder.Build();

startup.ConfigureServices(app);
//app.MapForwarder("/{**catch-all}", app.Configuration["ProxyTo"]).Add(static builder => ((RouteEndpointBuilder)builder).Order = int.MaxValue);

app.Run();
