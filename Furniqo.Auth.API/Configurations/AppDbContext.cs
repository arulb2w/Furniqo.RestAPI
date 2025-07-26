using Furniqo.Auth.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Furniqo.Auth.API.Configurations
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users => Set<User>();
        public DbSet<OtpSession> OtpSessions => Set<OtpSession>();

    }
}
