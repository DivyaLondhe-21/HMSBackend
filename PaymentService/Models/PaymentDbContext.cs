using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace PaymentService.Models;

public partial class PaymentDbContext : DbContext
{
    public PaymentDbContext()
    {
    }

    public PaymentDbContext(DbContextOptions<PaymentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LIN-5CG4464LZR\\SQLEXPRESS;Initial Catalog=HotelManagementSystemDb;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasIndex(e => e.PaymentId, "IX_Bills_PaymentId");

            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Service).HasMaxLength(100);
            entity.Property(e => e.Taxes).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Unit).HasMaxLength(50);

            entity.HasOne(d => d.Payment).WithMany(p => p.Bills).HasForeignKey(d => d.PaymentId);
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasIndex(e => e.ReservationId, "IX_Payments_ReservationId");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
