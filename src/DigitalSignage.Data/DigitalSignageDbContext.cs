// SPDX-FileCopyrightText: © 2014 Oberverwaltungsgericht Rheinland-Pfalz <poststelle@ovg.jm.rlp.de>
// SPDX-License-Identifier: EUPL-1.2
using DigitalSignage.Infrastructure.Models.EurekaFach;
using DigitalSignage.Infrastructure.Models.Settings;
using Microsoft.EntityFrameworkCore;

namespace DigitalSignage.Data;

public class DigitalSignageDbContext : DbContext
{

    public DigitalSignageDbContext(DbContextOptions<DigitalSignageDbContext> options) : base(options) { }

    /*
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (_connectionString != null)
            optionsBuilder
                .UseLazyLoadingProxies(false)
                .UseSqlServer(_connectionString);
        else
            optionsBuilder.UseLazyLoadingProxies();
    }
    */
    protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  #region Basics

  modelBuilder.Entity<Basics>()
      .ToTable("Basics")
      .HasKey(t => t.Nummer);

  #endregion Basics

  #region Stammdaten

  modelBuilder.Entity<Stammdaten>().ToTable("Stammdaten").HasKey(s => s.StammdatenId);
  modelBuilder.Entity<Stammdaten>().Property(s => s.Gerichtsname).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Stammdaten>().Property(s => s.Datum).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Stammdaten>().HasMany<Verfahren>(s => s.Verfahren).WithOne(v => v.Stammdaten).IsRequired().HasForeignKey(v => v.StammdatenId);

  #endregion Stammdaten

  #region Verfahren

  modelBuilder.Entity<Verfahren>().ToTable("Verfahren").HasKey(v => v.VerfahrensId);
  modelBuilder.Entity<Verfahren>().Property(v => v.Lfdnr).IsRequired();
  modelBuilder.Entity<Verfahren>().Property(v => v.Kammer).IsRequired();
  modelBuilder.Entity<Verfahren>().Property(v => v.Sitzungssaal).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.UhrzeitPlan).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.UhrzeitAktuell).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Status).HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Oeffentlich).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Az).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Gegenstand).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Bemerkung1).HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Bemerkung2).HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().Property(v => v.Art).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Verfahren>().HasMany<Besetzung>(v => v.Besetzung).WithOne().HasForeignKey(b => b.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ParteienAktiv>(v => v.ParteienAktiv).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ParteienPassiv>(v => v.ParteienPassiv).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ParteienBeigeladen>(v => v.ParteienBeigeladen).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ParteienSV>(v => v.ParteienSV).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ParteienZeugen>(v => v.ParteienZeugen).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ProzBevAktiv>(v => v.ProzBevAktiv).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ProzBevPassiv>(v => v.ProzBevPassiv).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ProzBevBeigeladen>(v => v.ProzBevBeigeladen).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<ParteienBeteiligt>(v => v.ParteienBeteiligt).WithOne().HasForeignKey(p => p.VerfahrensId);
  modelBuilder.Entity<Verfahren>().HasMany<Objekte>(v => v.Objekte).WithOne().HasForeignKey(o => o.VerfahrensId);

  #endregion Verfahren

  #region Besetzung

  modelBuilder.Entity<Besetzung>().ToTable("Besetzung").HasKey(b => b.BesetzungsId);
  modelBuilder.Entity<Besetzung>().Property(b => b.Richter).IsRequired().HasMaxLength(255);

  #endregion Besetzung

  #region ParteienAktiv

  modelBuilder.Entity<ParteienAktiv>().ToTable("ParteienAktiv").HasKey(p => p.ParteiId);
  modelBuilder.Entity<ParteienAktiv>().Property(p => p.Partei).HasMaxLength(255);

  #endregion ParteienAktiv

  #region ParteienPassiv

  modelBuilder.Entity<ParteienPassiv>().ToTable("ParteienPassiv").HasKey(p => p.ParteiId);
  modelBuilder.Entity<ParteienPassiv>().Property(p => p.Partei).HasMaxLength(255);

  #endregion ParteienPassiv

  #region ParteienBeigeladen

  modelBuilder.Entity<ParteienBeigeladen>().ToTable("ParteienBeigeladen").HasKey(p => p.ParteiId);
  modelBuilder.Entity<ParteienBeigeladen>().Property(p => p.Partei).HasMaxLength(255);

  #endregion ParteienBeigeladen

  #region ParteienSV

  modelBuilder.Entity<ParteienSV>().ToTable("ParteienSV").HasKey(p => p.ParteiId);
  modelBuilder.Entity<ParteienSV>().Property(p => p.Partei).HasMaxLength(255);

  #endregion ParteienSV

  #region ParteienZeugen

  modelBuilder.Entity<ParteienZeugen>().ToTable("ParteienZeugen").HasKey(p => p.ParteiId);
  modelBuilder.Entity<ParteienZeugen>().Property(p => p.Partei).HasMaxLength(255);

  #endregion ParteienZeugen

  #region ParteienBeteiligt

  modelBuilder.Entity<ParteienBeteiligt>().ToTable("ParteienBeteiligt").HasKey(p => p.ParteiId);
  modelBuilder.Entity<ParteienBeteiligt>().Property(p => p.Partei).HasMaxLength(255);

  #endregion ParteienBeteiligt

  #region ProzBevAktiv

  modelBuilder.Entity<ProzBevAktiv>().ToTable("ProzBevAktiv").HasKey(p => p.ProzBevId);
  modelBuilder.Entity<ProzBevAktiv>().Property(p => p.ProzBevId).HasColumnName("ProzBevAktivID");
  modelBuilder.Entity<ProzBevAktiv>().Property(p => p.PB).HasMaxLength(255);

  #endregion ProzBevAktiv

  #region ProzBevPassiv

  modelBuilder.Entity<ProzBevPassiv>().ToTable("ProzBevPassiv").HasKey(p => p.ProzBevId);
  modelBuilder.Entity<ProzBevPassiv>().Property(p => p.ProzBevId).HasColumnName("ProzBevAktivID");
  modelBuilder.Entity<ProzBevPassiv>().Property(p => p.PB).HasMaxLength(255);

  #endregion ProzBevPassiv

  #region ProzBevBeigeladen

  modelBuilder.Entity<ProzBevBeigeladen>().ToTable("ProzBevBeigeladen").HasKey(p => p.ProzBevId);
  modelBuilder.Entity<ProzBevBeigeladen>().Property(p => p.ProzBevId).HasColumnName("ProzBevAktivID");
  modelBuilder.Entity<ProzBevBeigeladen>().Property(p => p.PB).HasMaxLength(255);

  #endregion ProzBevBeigeladen

  #region Objekte

  modelBuilder.Entity<Objekte>().ToTable("Objekte").HasKey(o => o.ObjektId);
  modelBuilder.Entity<Objekte>().Property(o => o.Objektart).IsRequired().HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Gemarkung).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Flur).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Wirtschaftsart).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Anschrift).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Groesse).HasMaxLength(50);
  modelBuilder.Entity<Objekte>().Property(o => o.Objekt).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Blatt).HasMaxLength(50);
  modelBuilder.Entity<Objekte>().Property(o => o.Grundbuchamt).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Zusatz).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Eigentumsart).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Nutzungsrecht).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Kurzname).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Grundbuchbezirk).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.Eigentumsanteil).HasMaxLength(50);
  modelBuilder.Entity<Objekte>().Property(o => o.Schiffsregisterart).HasMaxLength(50);
  modelBuilder.Entity<Objekte>().Property(o => o.Schiffsname).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.IMONR).HasMaxLength(255);
  modelBuilder.Entity<Objekte>().Property(o => o.RegisterGericht).HasMaxLength(255);

  #endregion Objekte

  base.OnModelCreating(modelBuilder);
}

#region Sitzungsdaten

public DbSet<Stammdaten> Stammdaten { get; set; }
public DbSet<Verfahren> Verfahren { get; set; }
public DbSet<Besetzung> Besetzung { get; set; }
public DbSet<ParteienAktiv> ParteienAktiv { get; set; }
public DbSet<ParteienPassiv> ParteienPassiv { get; set; }
public DbSet<ParteienBeigeladen> ParteienBeigeladen { get; set; }
public DbSet<ParteienSV> ParteienSV { get; set; }
public DbSet<ParteienZeugen> ParteienZeugen { get; set; }
public DbSet<ProzBevAktiv> ProzBevAktiv { get; set; }
public DbSet<ProzBevPassiv> ProzBevPassiv { get; set; }
public DbSet<ProzBevBeigeladen> ProzBevBeigeladen { get; set; }
public DbSet<ParteienBeteiligt> ParteienBeteiligt { get; set; }
public DbSet<Objekte> Objekte { get; set; }

#endregion Sitzungsdaten

#region settings

public DbSet<Basics> Basics { get; set; }
public DbSet<Display> Displays { get; set; }
public DbSet<Note> Notes { get; set; }
public DbSet<NoteAssignment> NoteAssignments { get; set; }
public DbSet<Permission> Permissions { get; set; }

#endregion settings
}