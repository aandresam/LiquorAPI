using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using liquorApi.Context.Entities;

namespace liquorApi.Context;

public partial class LicoresDbContext : DbContext
{
    public LicoresDbContext()
    {
    }

    public LicoresDbContext(DbContextOptions<LicoresDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersProduct> UsersProducts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Products__3214EC07FEFB0C02");

            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC079CA1A86C");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534A5CFF2B7").IsUnique();

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<UsersProduct>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersPro__3214EC07AC8CA718");

            entity.Property(e => e.RegDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.UsersProducts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__UsersProd__Produ__5165187F");

            entity.HasOne(d => d.User).WithMany(p => p.UsersProducts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UsersProd__UserI__5070F446");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
