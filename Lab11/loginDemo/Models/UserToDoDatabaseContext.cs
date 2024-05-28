using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

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

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var builder = WebApplication.CreateBuilder();
        var connectionString = builder.Configuration.GetConnectionString ("MyConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblTodo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__tbl_todo__3213E83FFD5BD6AE");

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

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
