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

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillProduct> BillProducts { get; set; }

    public virtual DbSet<BillStt> BillStts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DetailedProduct> DetailedProducts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountUse> DiscountUses { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<TermOfPayment> TermOfPayments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=.\\;Initial Catalog=SONUNGVIEN;Integrated Security=True;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bill__11F2FC4A35A9D817");

            entity.ToTable("Bill");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.BillAddress).HasMaxLength(100);
            entity.Property(e => e.BillCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.BillEmail).HasMaxLength(100);
            entity.Property(e => e.BillFirstName).HasMaxLength(100);
            entity.Property(e => e.BillLastName).HasMaxLength(100);
            entity.Property(e => e.BillPhoneNumber).HasMaxLength(100);
            entity.Property(e => e.BillPostcode).HasMaxLength(100);
            entity.Property(e => e.BillProvince).HasMaxLength(100);
            entity.Property(e => e.BillUpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.BillWard).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.BillSttNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.BillStt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__BillStt__44FF419A");

            entity.HasOne(d => d.BillTermOfPaymentNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.BillTermOfPayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__BillTermOf__45F365D3");
        });

        modelBuilder.Entity<BillProduct>(entity =>
        {
            entity.HasKey(e => new { e.BillId, e.DetailedProductId }).HasName("PK__BillProd__0F1F604DC94E1B77");

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
                .HasConstraintName("FK__BillProdu__BillI__48CFD27E");

            entity.HasOne(d => d.DetailedProduct).WithMany(p => p.BillProducts)
                .HasForeignKey(d => d.DetailedProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BillProdu__Detai__49C3F6B7");
        });

        modelBuilder.Entity<BillStt>(entity =>
        {
            entity.HasKey(e => e.BillSttId).HasName("PK__BillStt__1BCF841667AD0F64");

            entity.ToTable("BillStt");

            entity.Property(e => e.BillSttId).HasColumnName("BillSttID");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2B8C6591DE");

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
            entity.HasKey(e => e.DetailedProductId).HasName("PK__Detailed__EED9C070DE9DF81E");

            entity.ToTable("DetailedProduct");

            entity.Property(e => e.DetailedProductId).HasColumnName("DetailedProductID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DetailedProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
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
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF6DD5A0F89");

            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DiscountAvailableFrom).HasColumnType("datetime");
            entity.Property(e => e.DiscountAvailableUntil).HasColumnType("datetime");
            entity.Property(e => e.DiscountName).HasMaxLength(100);
            entity.Property(e => e.DiscountRemark).HasMaxLength(100);
            entity.Property(e => e.Image).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiscountUse>(entity =>
        {
            entity.HasKey(e => new { e.DiscountId, e.BillId }).HasName("PK__Discount__5520423240C48B2E");

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
                .HasConstraintName("FK__DiscountU__BillI__4D94879B");

            entity.HasOne(d => d.Discount).WithMany(p => p.DiscountUses)
                .HasForeignKey(d => d.DiscountId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DiscountU__Disco__4CA06362");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6ED96BDBBAD");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__398D8EEE");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PK__ProductI__07B2B1D8028B2601");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ProductImageId).HasColumnName("ProductImageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductImageLink)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Produ__5070F446");
        });

        modelBuilder.Entity<TermOfPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__TermOfPa__9B556A5826C5CDF5");

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
