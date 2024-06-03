using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using loginDemo.Models;

namespace loginDemo.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Payment> Payments { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Rooms");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name).HasMaxLength(255);
                entity.Property(e => e.Description).HasMaxLength(255);
                entity.Property(e => e.Capacity).HasColumnType("int");
                entity.Property(e => e.PhotoPath).HasMaxLength(255);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.ToTable("Reservations");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.RoomId).HasColumnName("RoomId");
                entity.Property(e => e.UserId).HasMaxLength(450);
                entity.Property(e => e.StartTime).HasColumnType("datetime");
                entity.Property(e => e.EndTime).HasColumnType("datetime");
                entity.Property(e => e.IsCancelled).HasColumnName("IsCancelled");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Reservations_Rooms");
            });
    }
}
