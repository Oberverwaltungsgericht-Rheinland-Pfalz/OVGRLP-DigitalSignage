// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.NetworkInformation;

namespace DigitalSignage.Data.DbV3Models;
using T = Guid;

public class ApplicationDbContext : DbContext
{
    public DbSet<ClientVersion<T>> ClientVersionRepository;
    public DbSet<Department<T>> DepartmentRepository;
    public DbSet<Display<T>> DisplayRepository;
    public DbSet<EventChange<T>> EventChangeRepository;
    public DbSet<Event<T>> EventRepository;
    public DbSet<Filter<T>> FilterRepository;
    public DbSet<Group<T>> GroupRepository;
    public DbSet<Notification<T>> NotificationRepository;
    public DbSet<Person<T>> PersonRepository;
    public DbSet<Room<T>> RoomRepository;
    public DbSet<Schedule<T>> ScheduleRepository;
    public DbSet<Template<T>> TemplateRepository;


    public ApplicationDbContext()
    {
        ClientVersionRepository = Set<ClientVersion<T>>();
        DepartmentRepository = Set<Department<T>>();
        DisplayRepository = Set<Display<T>>();
        EventChangeRepository = Set<EventChange<T>>();
        EventRepository = Set<Event<T>>();
        FilterRepository = Set<Filter<T>>();
        GroupRepository = Set<Group<T>>();
        NotificationRepository = Set<Notification<T>>();
        PersonRepository = Set<Person<T>>();
        RoomRepository = Set<Room<T>>();
        ScheduleRepository = Set<Schedule<T>>();
        TemplateRepository = Set<Template<T>>();
    }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        ClientVersionRepository = Set<ClientVersion<T>>();
        DepartmentRepository = Set<Department<T>>();
        DisplayRepository = Set<Display<T>>();
        EventChangeRepository = Set<EventChange<T>>(); 
        EventRepository = Set<Event<T>>();
        FilterRepository = Set<Filter<T>>();
        GroupRepository = Set<Group<T>>();
        NotificationRepository = Set<Notification<T>>();
        PersonRepository = Set<Person<T>>();
        RoomRepository = Set<Room<T>>();
        ScheduleRepository = Set<Schedule<T>>();
        TemplateRepository = Set<Template<T>>();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientVersion<T>>();
        modelBuilder.Entity<Department<T>>();

        modelBuilder.Entity<Display<T>>()
            .HasOne(d => d.Template)
            .WithMany(t => t.Displays)
            .HasForeignKey(d => d.TemplateId)
            .IsRequired(false);
        modelBuilder.Entity<Display<T>>()
            .HasOne(d => d.Filter)
            .WithMany(f => f.Displays)
            .HasForeignKey(d => d.FilterId)
            .IsRequired(false);
        modelBuilder.Entity<Display<T>>()
            .HasOne(d => d.Group)
            .WithMany(g => g.Displays)
            .HasForeignKey(d => d.GroupId)
            .IsRequired(false);
        modelBuilder.Entity<Display<T>>()
            .Property(d => d.Mac)
            .HasConversion(
                v => v.ToString(),
                v => PhysicalAddress.Parse(v));
        modelBuilder.Entity<Display<T>>()
            .Property(d => d.Ip)
            .HasConversion(
                v => v.ToString(),
                v => IPAddress.Parse(v));

        modelBuilder.Entity<Event<T>>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Events)
            .HasForeignKey(e => e.DepartmentId)
            .IsRequired(false);
        modelBuilder.Entity<Event<T>>()
            .HasOne(e => e.Room)
            .WithMany(r => r.Events)
            .HasForeignKey(e => e.RoomId)
            .IsRequired(false);

        modelBuilder.Entity<EventChange<T>>()
            .HasOne(e => e.Department)
            .WithMany(d => d.EventChanges)
            .HasForeignKey(e => e.DepartmentId)
            .IsRequired(false);
        modelBuilder.Entity<EventChange<T>>()
            .HasOne(e => e.Room)
            .WithMany(r => r.EventChanges)
            .HasForeignKey(e => e.RoomId)
            .IsRequired(false);
        modelBuilder.Entity<EventChange<T>>()
            .HasOne(e => e.Event)
            .WithOne(e => e.EventChange)
            .HasForeignKey<Event<T>>(e => e.EventChangeId)
            .IsRequired(false);

        modelBuilder.Entity<Filter<T>>()
            .OwnsMany(f => f.Data, ownedBuilder =>
            {
                ownedBuilder.ToJson();
            })
            .HasMany(f => f.Groups)
            .WithOne(g => g.Filter)
            .HasForeignKey(g => g.FilterId)
            .IsRequired(false);
        // modelBuilder.Entity<Filter<T>>()
        //     .HasIndex(f => f.Name)
        //         .IsUnique();

        modelBuilder.Entity<Notification<T>>()
            .HasOne(n => n.Filter)
            .WithMany(f => f.Notifications)
            .HasForeignKey(n => n.FilterId)
            .IsRequired(false);
        modelBuilder.Entity<Notification<T>>()
            .HasOne(n => n.Template)
            .WithMany(t => t.Notifications)
            .HasForeignKey(n => n.TemplateId)
            .IsRequired(false);

        modelBuilder.Entity<Person<T>>()
            .HasMany(p => p.Events)
            .WithMany(e => e.Persons)
            .UsingEntity("PersonXEvent");
        modelBuilder.Entity<Person<T>>()
            .HasMany(p => p.EventChanges)
            .WithMany(e => e.Persons)
            .UsingEntity("PersonXEventChanges");

        // modelBuilder.Entity<Room<T>>()
        //     .HasIndex(r => r.RoomNumber)
        //         .IsUnique();

        modelBuilder.Entity<Schedule<T>>()
            .OwnsMany(s => s.Data, ownedBuilder =>
            {
                ownedBuilder.ToJson();
            });

        modelBuilder.Entity<Template<T>>()
            .OwnsMany(t => t.Html, ownedBuilder =>
            {
                ownedBuilder.ToJson();
            })
            .OwnsMany(t => t.Css, ownedBuilder =>
            {
                ownedBuilder.ToJson();
            });
    }
}
