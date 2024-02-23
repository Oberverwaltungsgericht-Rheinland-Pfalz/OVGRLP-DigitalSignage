
// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
// using Microsoft.AspNetCore.Authentication;
using DigitalSignage.Data.DbV3Models;
using DigitalSignage.Services.DataServices;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;

namespace DigitalSignage.DataAPI;

using DbC = ApplicationDbContext;

public partial class Program
{
    public static void Main(string[] args)
    {
        var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        logger.Debug("Sartup ...");


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();

        builder.Logging.ClearProviders();
        builder.Host.UseNLog();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        // Hint: Changing the common Id-Type not only here. The Type does not get passed to all the necessary classes
        // The Type also needs to be changed in the Controller Classes, Asp.Net does not allow generic Controllers 
        // without any hacks
        if (builder.Environment.IsDevelopment())
        {
            logger.Debug("Using In-Memory-Database...");
            // Use In-memory-DB for Debugging purposes...
            builder.Services.AddDbContextPool<DbC>(options => 
                options.UseInMemoryDatabase("DigitalSignage"));
        }
        else
        {
            logger.Debug("Using SQL-Server...");
            // Use a production DB for regular use...
            builder.Services.AddDbContextPool<DbC>(options => 
                options.UseSqlServer(builder.Configuration.GetConnectionString("DigitalSignage")));
        }

        builder.Services.AddSingleton<IWorkService<DbC, Guid>, WorkService<DbC, Guid>>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();

        NLog.LogManager.Shutdown();
    }
}
