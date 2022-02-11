using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAPI.DB
{
    public partial class DrinkSaleContext : DbContext
    {
        public DrinkSaleContext()
        {
        }

        public DrinkSaleContext(DbContextOptions<DrinkSaleContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Coin> Coins { get; set; } = null!;
        public virtual DbSet<Drink> Drinks { get; set; } = null!;
        public virtual DbSet<VendingMachine> VendingMachines { get; set; } = null!;
        public virtual DbSet<VendingMachineCoin> VendingMachineCoins { get; set; } = null!;
        public virtual DbSet<VendingMachineDrink> VendingMachineDrinks { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-C9BVD3K\\SQLEXPRESS;Database=DrinkSale;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Coin>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<Drink>(entity =>
            {
                entity.Property(e => e.Cost).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<VendingMachineCoin>(entity =>
            {
                entity.Property(e => e.IsActive).HasColumnName("isActive");

                entity.HasOne(d => d.Coins)
                    .WithMany(p => p.VendingMachineCoins)
                    .HasForeignKey(d => d.CoinsId)
                    .HasConstraintName("FK__VendingMa__Coins__412EB0B6");

                entity.HasOne(d => d.VendingMachines)
                    .WithMany(p => p.VendingMachineCoins)
                    .HasForeignKey(d => d.VendingMachinesId)
                    .HasConstraintName("FK__VendingMa__Vendi__403A8C7D");
            });

            modelBuilder.Entity<VendingMachineDrink>(entity =>
            {
                entity.HasOne(d => d.Drinks)
                    .WithMany(p => p.VendingMachineDrinks)
                    .HasForeignKey(d => d.DrinksId)
                    .HasConstraintName("FK__VendingMa__Drink__3B75D760");

                entity.HasOne(d => d.VendingMachines)
                    .WithMany(p => p.VendingMachineDrinks)
                    .HasForeignKey(d => d.VendingMachinesId)
                    .HasConstraintName("FK__VendingMa__Vendi__3A81B327");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
