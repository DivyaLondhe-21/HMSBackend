using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService.Models;

public partial class EmployeeDbContext : DbContext
{
    public EmployeeDbContext()
    {
    }

    public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Staff> Staffs { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=LIN-5CG4464LZR\\SQLEXPRESS;Initial Catalog=HotelManagementSystemDb;Integrated Security=True;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Department>(entity =>
        {
            entity.Property(e => e.DepartmentName).HasMaxLength(100);
        });

        modelBuilder.Entity<Staff>(entity =>
        {
            entity.HasIndex(e => e.DepartmentId, "IX_Staffs_DepartmentId");

            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.EmployeeAddress).HasMaxLength(200);
            entity.Property(e => e.EmployeeName).HasMaxLength(100);
            entity.Property(e => e.NIC)
                .HasMaxLength(15)
                .HasColumnName("NIC");
            entity.Property(e => e.Occupation).HasMaxLength(50);
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 2)");

            //entity.HasOne(d => d.Department).WithMany(p => p.Staffs).HasForeignKey(d => d.DepartmentId);
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
