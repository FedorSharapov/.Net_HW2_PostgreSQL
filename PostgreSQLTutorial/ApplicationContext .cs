using Microsoft.EntityFrameworkCore;
using PostgreSQLTutorial.Entities;

namespace PostgreSQLTutorial
{
    class ApplicationContext : DbContext
    {
        string _connectionString;

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SubwayStation> SubwayStations { get; set; }

        public ApplicationContext(string connectionString)
        {
            _connectionString = connectionString;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }
    }
}
