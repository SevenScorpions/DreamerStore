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

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<Bill> Bills { get; set; }

    public virtual DbSet<BillProduct> BillProducts { get; set; }

    public virtual DbSet<BillStt> BillStts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<DetailedProduct> DetailedProducts { get; set; }

    public virtual DbSet<Discount> Discounts { get; set; }

    public virtual DbSet<DiscountUse> DiscountUses { get; set; }

    public virtual DbSet<Footer> Footers { get; set; }

    public virtual DbSet<Logo> Logos { get; set; }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<TermOfPayment> TermOfPayments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.BannerId).HasName("PK__Banner__32E86A31889E81FB");

            entity.ToTable("Banner");

            entity.Property(e => e.BannerId).HasColumnName("BannerID");
            entity.Property(e => e.BannerDescription).HasMaxLength(100);
            entity.Property(e => e.BannerMessage).HasMaxLength(100);
            entity.Property(e => e.BannerName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image)
                .HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.BillId).HasName("PK__Bill__11F2FC4A210569AC");

            entity.ToTable("Bill");

            entity.Property(e => e.BillId).HasColumnName("BillID");
            entity.Property(e => e.BillAddress).HasMaxLength(100);
            entity.Property(e => e.BillCreatedAt).HasColumnType("datetime");
            entity.Property(e => e.BillEmail).HasMaxLength(100);
            entity.Property(e => e.BillFirstName).HasMaxLength(100);
            entity.Property(e => e.BillLastName).HasMaxLength(100);
            entity.Property(e => e.BillNote).HasMaxLength(100);
            entity.Property(e => e.BillPhoneNumber).HasMaxLength(100);
            entity.Property(e => e.BillPostcode).HasMaxLength(100);
            entity.Property(e => e.BillProvince).HasMaxLength(100);
            entity.Property(e => e.BillSttId).HasColumnName("BillSttID");
            entity.Property(e => e.BillUpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.BillWard).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.BillStt).WithMany(p => p.Bills)
                .HasForeignKey(d => d.BillSttId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__BillSttID__44FF419A");

            entity.HasOne(d => d.BillTermOfPaymentNavigation).WithMany(p => p.Bills)
                .HasForeignKey(d => d.BillTermOfPayment)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Bill__BillTermOf__45F365D3");
        });

        modelBuilder.Entity<BillProduct>(entity =>
        {
            entity.HasKey(e => new { e.BillId, e.DetailedProductId }).HasName("PK__BillProd__0F1F604DCA02D6D7");

            entity.ToTable("BillProduct");

            entity.Property(e => e.BillId)
                .ValueGeneratedOnAdd()
                .HasColumnName("BillID");
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
            entity.HasKey(e => e.BillSttId).HasName("PK__BillStt__1BCF8416A992D457");

            entity.ToTable("BillStt");

            entity.Property(e => e.BillSttId).HasColumnName("BillSttID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Category__19093A2BFEEA07ED");

            entity.ToTable("Category");

            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CategoryName).HasMaxLength(100);
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image)
                .HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DetailedProduct>(entity =>
        {
            entity.HasKey(e => e.DetailedProductId).HasName("PK__Detailed__EED9C070A2BAB153");

            entity.ToTable("DetailedProduct");

            entity.Property(e => e.DetailedProductId).HasColumnName("DetailedProductID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DetailedProductName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.DetailedProductPrice).HasColumnName("DetailedProductPRICE");
            entity.Property(e => e.DetailedProductQuantity).HasColumnName("DetailedProductQUANTITY");
            entity.Property(e => e.Image)
                .HasMaxLength(100);
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
            entity.HasKey(e => e.DiscountId).HasName("PK__Discount__E43F6DF614E00C76");

            entity.ToTable("Discount");

            entity.Property(e => e.DiscountId).HasColumnName("DiscountID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.DiscountAvailableFrom).HasColumnType("datetime");
            entity.Property(e => e.DiscountAvailableUntil).HasColumnType("datetime");
            entity.Property(e => e.DiscountCalUnit).HasMaxLength(100);
            entity.Property(e => e.DiscountName).HasMaxLength(100);
            entity.Property(e => e.DiscountRemark).HasMaxLength(100);
            entity.Property(e => e.Image)
                .HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<DiscountUse>(entity =>
        {
            entity.HasKey(e => new { e.DiscountId, e.BillId }).HasName("PK__Discount__55204232614688BE");

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

        modelBuilder.Entity<Footer>(entity =>
        {
            entity.HasKey(e => e.FooterId).HasName("PK__Footer__838347B869048B71");

            entity.ToTable("Footer");

            entity.Property(e => e.FooterId).HasColumnName("FooterID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.FooterDescription).HasMaxLength(100);
            entity.Property(e => e.FooterName).HasMaxLength(100);
            entity.Property(e => e.Image)
                .HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Logo>(entity =>
        {
            entity.HasKey(e => e.LogoId).HasName("PK__Logo__C62015ADEF0CA379");

            entity.ToTable("Logo");

            entity.Property(e => e.LogoId).HasColumnName("LogoID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image)
                .HasMaxLength(100);
            entity.Property(e => e.LogoName).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.MenuId).HasName("PK__Menu__C99ED25050534B70");

            entity.ToTable("Menu");

            entity.Property(e => e.MenuId).HasColumnName("MenuID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image)
                .HasMaxLength(100);
            entity.Property(e => e.MenuName).HasMaxLength(100);
            entity.Property(e => e.Meta)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Product__B40CC6EDB368D8CF");

            entity.ToTable("Product");

            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Meta)
                .HasMaxLength(100);
            entity.Property(e => e.ProductDescription).HasMaxLength(100);
            entity.Property(e => e.ProductName).HasMaxLength(100);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Product__Categor__398D8EEE");
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.ProductImageId).HasName("PK__ProductI__07B2B1D8AAB1D26F");

            entity.ToTable("ProductImage");

            entity.Property(e => e.ProductImageId).HasColumnName("ProductImageID");
            entity.Property(e => e.ProductId).HasColumnName("ProductID");
            entity.Property(e => e.ProductImageLink)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Product).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Produ__5812160E");
        });

        modelBuilder.Entity<TermOfPayment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__TermOfPa__9B556A58F9E638A4");

            entity.ToTable("TermOfPayment");

            entity.Property(e => e.PaymentId).HasColumnName("PaymentID");
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Image)
                .HasMaxLength(100);
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
