using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Models;

public partial class InventoryDbContext : DbContext
{
    public InventoryDbContext()
    {
    }

    public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Inventory> Inventories { get; set; }
    public virtual DbSet<Department> Departments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LIN-5CG4464LZR\\SQLEXPRESS;Initial Catalog=HotelManagementSystemDb;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });
        modelBuilder.Entity<Inventory>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Inventories_DepartmentId");

            entity.Property(e => e.ItemName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
