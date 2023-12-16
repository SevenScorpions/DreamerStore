using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DreamerStore2.Models;

public partial class SonungvienContext : DbContext
{
    public SonungvienContext()
    {
    }

    public SonungvienContext(DbContextOptions<SonungvienContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillProduct> BillProducts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DetailedProduct> DetailedProducts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountUse> DiscountUses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<TermOfPayment> TermOfPayments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__Account__349DA586DC9EB0D3");

            entity.ToTable("Account");

            entity.Property(e => e.AccountId).HasColumnName("AccountID");
            entity.Property(e => e.Password).HasMaxLength(64);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bill__11F2FC4ADDFF20F6");

            entity.ToTable("Bill");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.BillCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.BillDiscountAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BillEmail).HasMaxLength(100);
            entity.Property(e => e.BillFinalPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BillFirstName).HasMaxLength(100);
            entity.Property(e => e.BillLastName).HasMaxLength(100);
            entity.Property(e => e.BillOldPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BillPhoneNumber).HasMaxLength(100);
            entity.Property(e => e.BillPostcode).HasMaxLength(100);
            entity.Property(e => e.BillPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BillProvince).HasMaxLength(100);
            entity.Property(e => e.BillStt).HasMaxLength(200);
            entity.Property(e => e.BillTaxAmount).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.BillUpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.BillWard).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.BillTermOfPaymentNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.BillTermOfPayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__BillTermOf__4316F928");
        });

        modelBuilder.Entity<BillProduct>(entity =>
        {
            entity.HasKey(e => new { e.BillId, e.DetailedProductId }).HasName("PK__BillProd__0F1F604D8BFDA3DD");

            entity.ToTable("BillProduct");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.DetailedProductId).HasColumnName("DetailedProductID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Bill).WithMany(p => p.BillProducts)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillProdu__BillI__45F365D3");

            entity.HasOne(d => d.DetailedProduct).WithMany(p => p.BillProducts)
                .HasForeignKey(d => d.DetailedProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillProdu__Detai__46E78A0C");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B5F0C719E");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DetailedProduct>(entity =>
        {
            entity.HasKey(e => e.DetailedProductId).HasName("PK__Detailed__EED9C0702FAF0908");

            entity.ToTable("DetailedProduct");

            entity.Property(e => e.DetailedProductId).HasColumnName("DetailedProductID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DetailedProductName).HasMaxLength(100);
            entity.Property(e => e.DetailedProductPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.DetailedProducts)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DetailedP__Produ__3E52440B");
        });

        modelBuilder.Entity<Discount>(entity =>
        {
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF608E71238");

            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DiscountAvailableFrom).HasColumnType("datetime");
            entity.Property(e => e.DiscountAvailableUntil).HasColumnType("datetime");
            entity.Property(e => e.DiscountCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.DiscountName).HasMaxLength(100);
            entity.Property(e => e.DiscountRemark).HasMaxLength(100);
            entity.Property(e => e.DiscountValue).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiscountUse>(entity =>
        {
            entity.HasKey(e => new { e.DiscountId, e.BillId }).HasName("PK__Discount__5520423230628B1C");

            entity.ToTable("DiscountUse");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.UsedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Bill).WithMany(p => p.DiscountUses)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiscountU__BillI__4AB81AF0");

            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountUses)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiscountU__Disco__49C3F6B7");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED3AD602AA");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.ProductPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__398D8EEE");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PK__ProductI__07B2B1D8FAA1730B");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ProductImageId).HasColumnName("ProductImageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductImageLink)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Produ__4D94879B");
        });

        modelBuilder.Entity<TermOfPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__TermOfPa__9B556A586E23763B");

            entity.ToTable("TermOfPayment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PaymentName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
