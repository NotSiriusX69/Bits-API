using Bits_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bits_API.Services
{
    public class BitsContext : DbContext
    {
        public BitsContext(DbContextOptions options) : base(options) { }

        public DbSet<User> user { get; set; }
        public DbSet<Project> project { get; set; }
        public DbSet<Status> status { get; set; }
        public DbSet<Models.Entities.Task> task { get; set; }
        public DbSet<Category> category { get; set; }

    }
}
