using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Furniqo.Auth.API.Configurations
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

            // 👇 Update this connection string if needed
            // optionsBuilder.UseSqlServer("Server=localhost;Database=FurniqoAuthDb;User Id=sa;Password=Furniqo!Passw0rd;TrustServerCertificate=True");
            optionsBuilder.UseInMemoryDatabase("FurniqoAuthDb_InMemory");

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}
