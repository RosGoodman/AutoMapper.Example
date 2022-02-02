using AutoMapperExample.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace AutoMapperExample.DAL.Context
{
    public class DataContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConString"));
        }
    }
}
