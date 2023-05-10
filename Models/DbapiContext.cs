using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API.Models;

public partial class DbapiContext : DbContext
{
    public DbapiContext()
    {
    }

    public DbapiContext(DbContextOptions<DbapiContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
#warning 
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCategory).HasName("PK__CATEGORY__CBD74706B47F3619");

            entity.ToTable("CATEGORY");

            entity.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProduct).HasName("PK__PRODUCT__2E8946D49F110218");

            entity.ToTable("PRODUCT");

            entity.Property(e => e.BarCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.IdCategory).HasColumnName("idCategory");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ProdDescription)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prod_description");
            entity.Property(e => e.ProdLabel)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("prod_label");

            entity.HasOne(d => d.oCategory).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCategory)
                .HasConstraintName("FK_IDCATEGORY");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
