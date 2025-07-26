using Microsoft.EntityFrameworkCore;
using Furniqo.MasterData.API.Models;

namespace Furniqo.MasterData.API.Configurations;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Category> Categories => Set<Category>();
    public DbSet<SubCategory> SubCategories => Set<SubCategory>();
    public DbSet<Brand> Brands => Set<Brand>();
    public DbSet<Material> Materials => Set<Material>();
    public DbSet<Color> Colors => Set<Color>();
    public DbSet<Size> Sizes => Set<Size>();
    public DbSet<Unit> Units => Set<Unit>();
    public DbSet<Tax> Taxes => Set<Tax>();
    public DbSet<Warranty> Warranties => Set<Warranty>();
    public DbSet<Country> Countries => Set<Country>();
    public DbSet<OfferTag> OfferTags => Set<OfferTag>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: Apply configurations or constraints if needed
        modelBuilder.Entity<Category>().HasMany(c => c.SubCategories).WithOne(sc => sc.Category).HasForeignKey(sc => sc.CategoryId);
    }
}
