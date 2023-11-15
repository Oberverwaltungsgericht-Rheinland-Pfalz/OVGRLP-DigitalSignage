// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.dn.WebApiCore.DtoModels;
using DigitalSignage.dn.WebApiCore.Services;
using DigitalSignage.WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignage.dn.WebApiCore;

public class Startup
{
    WebApplicationBuilder _builder;
    IServiceCollection _services;
    WebApplication _app;
    public Startup(WebApplicationBuilder builder, WebApplication app)
    {
        _services = builder.Services;
        _app = app;
        _builder = builder;
    }

    public Startup AddDependencyInjection()
    {
        _services.AddDbContext<DigitalSignageDbContext>(options =>
        {
            options.UseSqlServer(
              _builder.Configuration["ConnectionString"],
              sqlServerOptions => sqlServerOptions.CommandTimeout(120));   
        });

        _services.AddScoped<DisplayManagementService>();
        _services.AddScoped<DigitalSignagePersistenceManager>();

        _services.AddSingleton(new Configuration(_builder.Configuration.GetValue<bool>("checkPermissions")));

        return this;
    }

    public Startup ConfigureServices() {

        _app.UseCors(options =>
        {
            options
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:4202", "http://localhost:4203");
        });
        return this;
    }
}
