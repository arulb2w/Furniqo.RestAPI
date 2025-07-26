using Furniqo.Product.API.Models;
using Microsoft.EntityFrameworkCore;
namespace Furniqo.Product.API.Configurations;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<ProductEntity> ProductEntity { get; set; }

}
