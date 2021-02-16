using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace OrderSystem
{
    public partial class OrderSystemDatabaseContext : DbContext
    {
        public OrderSystemDatabaseContext()
        {
        }

        public OrderSystemDatabaseContext(DbContextOptions<OrderSystemDatabaseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adress> Adresses { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Courier> Couriers { get; set; }
        public virtual DbSet<FamilyCard> FamilyCards { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductOrderPair> ProductOrderPairs { get; set; }
        public virtual DbSet<Shop> Shops { get; set; }
        public virtual DbSet<Street> Streets { get; set; }
        public virtual DbSet<Сity> Сities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server= LAPTOP-P4IHNG64; Database=OrderSystemDatabase; Trusted_Connection=True; ");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Cyrillic_General_CI_AS");

            modelBuilder.Entity<Adress>(entity =>
            {
                entity.HasKey(e => e.AddressId);

                entity.ToTable("Adress");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.AddressNotes).HasMaxLength(128);

                entity.Property(e => e.StreetId).HasColumnName("StreetID");

                entity.HasOne(d => d.Street)
                    .WithMany(p => p.Adresses)
                    .HasForeignKey(d => d.StreetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Adress_Street");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("Client");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.ClientEmail)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.ClientName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.ClientSurname)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.FamilyCardId).HasColumnName("FamilyCardID");

                entity.HasOne(d => d.FamilyCard)
                    .WithMany(p => p.Clients)
                    .HasForeignKey(d => d.FamilyCardId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Client_FamilyCard");
            });

            modelBuilder.Entity<Courier>(entity =>
            {
                entity.ToTable("Courier");

                entity.Property(e => e.CourierId).HasColumnName("CourierID");

                entity.Property(e => e.CourierName)
                    .IsRequired()
                    .HasMaxLength(32);

                entity.Property(e => e.CourierSurname)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Couriers)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Courier_Shop");
            });

            modelBuilder.Entity<FamilyCard>(entity =>
            {
                entity.ToTable("FamilyCard");

                entity.Property(e => e.FamilyCardId).HasColumnName("FamilyCardID");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.ClientId).HasColumnName("ClientID");

                entity.Property(e => e.CourierId).HasColumnName("CourierID");

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Adress");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.ClientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Client");

                entity.HasOne(d => d.Courier)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CourierId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Courier");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.HasOne(d => d.Shop)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ShopId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Shop");
            });

            modelBuilder.Entity<ProductOrderPair>(entity =>
            {
                entity.HasKey(e => e.PairId);

                entity.ToTable("ProductOrderPair");

                entity.Property(e => e.PairId).HasColumnName("PairID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.ProductOrderPairs)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrderPair_Order");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductOrderPairs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductOrderPair_Product");
            });

            modelBuilder.Entity<Shop>(entity =>
            {
                entity.ToTable("Shop");

                entity.Property(e => e.ShopId).HasColumnName("ShopID");

                entity.Property(e => e.AddressId).HasColumnName("AddressID");

                entity.Property(e => e.ShopName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.Address)
                    .WithMany(p => p.Shops)
                    .HasForeignKey(d => d.AddressId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Shop_Adress");
            });

            modelBuilder.Entity<Street>(entity =>
            {
                entity.ToTable("Street");

                entity.Property(e => e.StreetId).HasColumnName("StreetID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.StreetName)
                    .IsRequired()
                    .HasMaxLength(64);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Streets)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_City_Street");
            });

            modelBuilder.Entity<Сity>(entity =>
            {
                entity.HasKey(e => e.CityId);

                entity.ToTable("Сity");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(64);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
