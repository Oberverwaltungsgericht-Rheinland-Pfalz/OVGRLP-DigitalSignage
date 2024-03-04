using Microsoft.EntityFrameworkCore;
using Core.Models;
using System.Net;
using System.Net.NetworkInformation;

namespace Services.Database;

public class DSContext : DbContext
{
    public DbSet<RegisterVersion> RegisterVersionRepository;
    public DbSet<ClientVersion> ClientVersionRepository;
    public DbSet<Department> DepartmentRepository;
    public DbSet<Display> DisplayRepository;
    public DbSet<EventChange> EventChangeRepository;
    public DbSet<Event> EventRepository;
    public DbSet<Filter> FilterRepository;
    public DbSet<Group> GroupRepository;
    public DbSet<Notification> NotificationRepository;
    public DbSet<Person> PersonRepository;
    public DbSet<Room> RoomRepository;
    public DbSet<Schedule> ScheduleRepository;
    public DbSet<Template> TemplateRepository;

    public DSContext(DbContextOptions options) : base(options)
    {
        RegisterVersionRepository = Set<RegisterVersion>();
        ClientVersionRepository = Set<ClientVersion>();
        DepartmentRepository = Set<Department>();
        DisplayRepository = Set<Display>();
        EventChangeRepository = Set<EventChange>(); 
        EventRepository = Set<Event>();
        FilterRepository = Set<Filter>();
        GroupRepository = Set<Group>();
        NotificationRepository = Set<Notification>();
        PersonRepository = Set<Person>();
        RoomRepository = Set<Room>();
        ScheduleRepository = Set<Schedule>();
        TemplateRepository = Set<Template>();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        // optionsBuilder.EnableSensitiveDataLogging(true);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<RegisterVersion>();
        modelBuilder.Entity<ClientVersion>();
        modelBuilder.Entity<Department>()
            .HasIndex(d => d.Name)
                .IsUnique();

        modelBuilder.Entity<Display>()
            .HasOne(d => d.Template)
            .WithMany(t => t.Displays)
            .HasForeignKey(d => d.TemplateId)
            .IsRequired(false);
        modelBuilder.Entity<Display>()
            .HasOne(d => d.Filter)
            .WithMany(f => f.Displays)
            .HasForeignKey(d => d.FilterId)
            .IsRequired(false);
        modelBuilder.Entity<Display>()
            .HasOne(d => d.Group)
            .WithMany(g => g.Displays)
            .HasForeignKey(d => d.GroupId)
            .IsRequired(false);
        modelBuilder.Entity<Display>()
            .Property(d => d.Mac)
            .HasConversion(
                v => v.ToString(),
                v => PhysicalAddress.Parse(v));
        modelBuilder.Entity<Display>()
            .Property(d => d.Ip)
            .HasConversion(
                v => v.ToString(),
                v => IPAddress.Parse(v));
        modelBuilder.Entity<Display>()
            .HasIndex(d => d.Ip)
                .IsUnique();
        modelBuilder.Entity<Display>()
            .HasIndex(d => d.Mac)
                .IsUnique();

        modelBuilder.Entity<Event>()
            .HasOne(e => e.Department)
            .WithMany(d => d.Events)
            .HasForeignKey(e => e.DepartmentId)
            .IsRequired(false);
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Room)
            .WithMany(r => r.Events)
            .HasForeignKey(e => e.RoomId)
            .IsRequired(false);

        modelBuilder.Entity<EventChange>()
            .HasOne(e => e.Department)
            .WithMany(d => d.EventChanges)
            .HasForeignKey(e => e.DepartmentId)
            .IsRequired(false);
        modelBuilder.Entity<EventChange>()
            .HasOne(e => e.Room)
            .WithMany(r => r.EventChanges)
            .HasForeignKey(e => e.RoomId)
            .IsRequired(false);
        modelBuilder.Entity<EventChange>()
            .HasOne(e => e.Event)
            .WithOne(e => e.EventChange)
            .HasForeignKey<Event>(e => e.EventChangeId)
            .IsRequired(false);

        modelBuilder.Entity<Filter>()
            .OwnsMany(f => f.Data, ownedBuilder =>
            {
                ownedBuilder.ToJson();
            })
            .HasMany(f => f.Groups)
            .WithOne(g => g.Filter)
            .HasForeignKey(g => g.FilterId)
            .IsRequired(false);
        modelBuilder.Entity<Filter>()
            .HasIndex(f => f.Name)
                .IsUnique();

        modelBuilder.Entity<Group>()
            .HasIndex(g => g.Name)
                .IsUnique();

        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Filter)
            .WithMany(f => f.Notifications)
            .HasForeignKey(n => n.FilterId)
            .IsRequired(false);
        modelBuilder.Entity<Notification>()
            .HasOne(n => n.Template)
            .WithMany(t => t.Notifications)
            .HasForeignKey(n => n.TemplateId)
            .IsRequired(false);

        modelBuilder.Entity<Person>()
            .HasMany(p => p.Events)
            .WithMany(e => e.Persons)
            .UsingEntity("PersonXEvent");
        modelBuilder.Entity<Person>()
            .HasMany(p => p.EventChanges)
            .WithMany(e => e.Persons)
            .UsingEntity("PersonXEventChanges");

        modelBuilder.Entity<Room>();
        //     .HasIndex(r => r.RoomNumber)
        //         .IsUnique();

        modelBuilder.Entity<Schedule>()
            .OwnsMany(s => s.Data, ownedBuilder =>
            {
                ownedBuilder.ToJson();
            });

        modelBuilder.Entity<Template>()
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
