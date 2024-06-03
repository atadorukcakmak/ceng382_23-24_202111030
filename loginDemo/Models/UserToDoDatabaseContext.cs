using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;

namespace loginDemo.Models;

public partial class UserToDoDatabaseContext : DbContext
{
    public UserToDoDatabaseContext()
    {
    }

    public UserToDoDatabaseContext(DbContextOptions<UserToDoDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblTodo> TblTodos { get; set; }
    public virtual DbSet<Room> Rooms { get; set; }
    public virtual DbSet<Reservation> Reservations { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblTodo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_todo__3213E83F531957F3");

            entity.ToTable("tbl_todo");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Category)
                .HasMaxLength(10)
                .IsFixedLength()
                .HasColumnName("category");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .IsFixedLength()
                .HasColumnName("description");
            entity.Property(e => e.EndDate)
                .HasColumnType("datetime")
                .HasColumnName("endDate");
            entity.Property(e => e.IsDeleted).HasColumnName("isDeleted");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsFixedLength()
                .HasColumnName("title");
            entity.Property(e => e.UserId)
                .HasMaxLength(450)
                .HasColumnName("userId");
        });


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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();

        var connectionString = builder.GetConnectionString("MyConnection");

        optionsBuilder.UseSqlServer(connectionString);
    }

}
