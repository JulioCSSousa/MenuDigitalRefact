
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
    public DbSet<Additional> Additionals => Set<Additional>();
    public DbSet<StoreModel> StoreModels => Set<StoreModel>();
    public DbSet<WorkSchedule> WorkSchedules => Set<WorkSchedule>();
    public DbSet<AddressModel> Addresses => Set<AddressModel>();
    public DbSet<StorePayments> StorePayments => Set<StorePayments>();
    public DbSet<OrderList> OrderList => Set<OrderList>();
    public DbSet<Category> Categories => Set<Category>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        // Store → Colors & Images como owned simples
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.Colors);
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.Images);
        modelBuilder.Entity<StoreModel>().OwnsOne(s => s.Contacts);
        modelBuilder.Entity<StoreModel>().OwnsMany(s => s.SocialMedias, sm =>
        {
            sm.Property(x => x.Name).HasColumnName("SocialMedia_Name");
            sm.Property(x => x.Url).HasColumnName("SocialMedia_Url");
            sm.ToTable("StoreSocialMedias");
        });
        modelBuilder.Entity<WorkSchedule>()
        .HasOne<StoreModel>()
        .WithMany(s => s.WorkSchedule)
        .HasForeignKey("StoreId")
        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<StorePayments>()
            .HasOne<StoreModel>()
            .WithMany(s => s.StorePayments)
            .HasForeignKey("StoreId") 
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<AddressModel>()
            .HasOne(a => a.Store)
            .WithMany(s => s.Address)
            .HasForeignKey(a => a.StoreId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<ProductModel>()
       .HasOne<StoreModel>()
       .WithMany(s => s.Products)
       .HasForeignKey("StoreId")
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ProductModel>()
       .HasMany<Additional>()
       .WithOne(s => s.Product)
       .HasForeignKey("ProductId")
       .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Additional>()
        .HasOne(a => a.Product)
        .WithMany(p => p.Additional)
        .HasForeignKey(a => a.ProductId)
        .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StoreModel>()
        .OwnsMany(s => s.SocialMedias, sm =>
        {
            sm.WithOwner().HasForeignKey("StoreId"); 
            sm.Property(x => x.Name).HasColumnName("SocialMedia_Name");
            sm.Property(x => x.Url).HasColumnName("SocialMedia_Url");
            sm.ToTable("StoreSocialMedias");
        });

        base.OnModelCreating(modelBuilder);

        
    }

}
