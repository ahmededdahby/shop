using GestionDeMagazin.Models;
using GestionDeMagazin.Models.Purchases;
using GestionDeMagazin.Models.Sales;
using Microsoft.EntityFrameworkCore;

namespace GestionDeMagazin.Context;

public class AppDbContext:DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Sale>()
            .HasKey(s => s.SaleId);

        modelBuilder.Entity<SaleItem>()
            .HasKey(si => si.SaleItemId);

        modelBuilder.Entity<SaleItem>()
            .HasOne(si => si.Sale)
            .WithMany(s => s.SaleItems)
            .HasForeignKey(si => si.SaleId)
            .IsRequired();


        modelBuilder.Entity<Purchase>()
            .HasKey(p => p.PurchaseId);

        modelBuilder.Entity<PurchaseItem>()
            .HasKey(pi => pi.PurchaseItemId);

        modelBuilder.Entity<PurchaseItem>()
            .HasOne(pi => pi.Purchase)
            .WithMany(p => p.PurchaseItems)
            .HasForeignKey(pi => pi.PurchaseId)
            .IsRequired();
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Sale> Sales { get; set; }
    public DbSet<SaleItem> SalesItem { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseItem> PurchaseItems { get; set; }

}
