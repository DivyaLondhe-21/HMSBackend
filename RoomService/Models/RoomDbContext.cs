using Microsoft.EntityFrameworkCore;

namespace RoomService.Models
{
    public partial class RoomDbContext : DbContext
    {
        public RoomDbContext() { }

        public RoomDbContext(DbContextOptions<RoomDbContext> options)
            : base(options) { }

        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=LIN-5CG4464LZR\\SQLEXPRESS;Initial Catalog=HotelManagementSystemDb;Integrated Security=True;Trust Server Certificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Rate>(entity =>
            {
                entity.HasIndex(e => e.RoomId, "IX_Rates_RoomId");

                entity.Property(e => e.FirstNightPrice).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.ExtensionPrice).HasColumnType("decimal(18, 2)");

                entity.HasOne(e => e.Room)
                      .WithMany(r => r.Rates)
                      .HasForeignKey(e => e.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.RoomID).HasColumnName("RoomID");
                entity.Property(e => e.RoomType).HasMaxLength(50);
                entity.Property(e => e.Period).HasMaxLength(20);
                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
