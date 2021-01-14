using GenericApp.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericApp.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CountryEntity> Countries { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CountryEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();
        }
    }
}