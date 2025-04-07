using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReservationService.Models;

public partial class ReservationDbContext : DbContext
{
    public ReservationDbContext()
    {
    }

    public ReservationDbContext(DbContextOptions<ReservationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Guest> Guests { get; set; }

    public virtual DbSet<Reservation> Reservations { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Rate> Rates { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=LIN-5CG4464LZR\\SQLEXPRESS;Initial Catalog=HotelManagementSystemDb;Integrated Security=True;Trust Server Certificate=True;");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Guest>(entity =>
        {
            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.Company).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Reservation>(entity =>
        {
            entity.HasIndex(e => e.GuestId, "IX_Reservations_GuestId");

            entity.HasIndex(e => e.RoomId, "IX_Reservations_RoomId");

            entity.HasOne(d => d.Guest).WithMany(p => p.Reservations).HasForeignKey(d => d.GuestId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
