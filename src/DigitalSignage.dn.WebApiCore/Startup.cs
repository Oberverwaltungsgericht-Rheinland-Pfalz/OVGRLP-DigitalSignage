// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Data;
using DigitalSignage.dn.WebApiCore.DtoModels;
using DigitalSignage.dn.WebApiCore.Services;
using DigitalSignage.WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Serilog;

namespace DigitalSignage.dn.WebApiCore;

public class Startup
{
    WebApplicationBuilder _builder;
    IServiceCollection _services;
    IWebHostEnvironment _environment;
    public Startup(WebApplicationBuilder builder)
    {
        _services = builder.Services;
        _builder = builder;
        _environment = builder.Environment;
    }

    public void AddDependencyInjection()
    {
        // Add services to the container.
        _services.AddControllers();

        _services.AddDbContext<DigitalSignageDbContext>(options =>
        {
            options.UseSqlServer(
              _builder.Configuration.GetConnectionString("EfContext"),
              sqlServerOptions => sqlServerOptions.CommandTimeout(120));   
        });

        _services.AddScoped<DisplayManagementService>();
        _services.AddScoped<DigitalSignagePersistenceManager>();

        _services.AddSingleton(new Configuration(_builder.Configuration.GetValue<bool>("checkPermissions")));

        _services.AddHealthChecks().AddDbContextCheck<DigitalSignageDbContext>("ef");

        _services.AddSwaggerGen();

        _services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
           .AddNegotiate();

        _services.AddAuthorization(options =>
        {
            options.FallbackPolicy = options.DefaultPolicy;
        });
    }

    public void ConfigureServices(WebApplication app) {
        
        app.UseHealthChecks("/health");

        if (_environment.IsDevelopment() || _builder.Configuration["ShowExceptionPage"] == "true")    
          app.UseDeveloperExceptionPage();

        if (_environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DigitalSignage.WebApiCore v1"));
        }

        app.UseCors(options =>
        {
            options
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithOrigins("http://localhost:4200", "http://localhost:4201", "http://localhost:4202", "http://localhost:4203");
        });

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseDefaultFiles(); //UseStaticFiles();

        app.UseSerilogRequestLogging();
        app.UseRouting();
        app.UseAuthorization();
        app.UseSystemWebAdapters();

        app.MapDefaultControllerRoute();
    }
}
