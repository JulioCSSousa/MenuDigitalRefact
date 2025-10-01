
using MenuDigital.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MenuDigital.Domain.Models.Entities;
using MenuDigital.Domain.Entities.MenuModels;

namespace MenuDigital.Infrastructure.Persistence.MySQLContext;

public class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> opts) : base(opts) { }

    public DbSet<MenuModel> Menu => Set<MenuModel>();
    public DbSet<ProductModel> Products => Set<ProductModel>();
    public DbSet<CombinedProduct> CombinedProducts => Set<CombinedProduct>();
    public DbSet<StoreModel> StoreModels => Set<StoreModel>();
    public DbSet<WorkSchedule> WorkSchedules => Set<WorkSchedule>();
    public DbSet<AddressModel> Addresses => Set<AddressModel>();
    public DbSet<StorePayments> StorePayments => Set<StorePayments>();
    public DbSet<Category> Categories => Set<Category>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Product → Prices
        modelBuilder.Entity<ProductModel>().OwnsMany(p => p.Prices);

        // Product → PreviewPrices
        modelBuilder.Entity<ProductModel>().OwnsMany(p => p.PreviewPrices);

        // Store → Colors & Images como owned simples
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.Colors);
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.Images);
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.Contacts);
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.SocialMedias);
        // Store → OpeningHours com Turns

        base.OnModelCreating(modelBuilder);
    }

}
