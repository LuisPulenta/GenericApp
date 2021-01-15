using GenericApp.Web.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace GenericApp.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<CityEntity> Cities { get; set; }
        public DbSet<CountryEntity> Countries { get; set; }
        public DbSet<DepartmentEntity> Departments { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CountryEntity>()
                .HasIndex(t => t.Name)
                .IsUnique();

            modelBuilder.Entity<DepartmentEntity>(dep =>
            {
                dep.HasIndex("Name", "CountryId").IsUnique();
                dep.HasOne(d => d.Country).WithMany(c => c.Departments).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<CityEntity>(cit =>
            {
                cit.HasIndex("Name", "DepartmentId").IsUnique();
                cit.HasOne(c => c.Department).WithMany(d => d.Cities).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<TeamEntity>(dep =>
            {
                dep.HasIndex("Name", "CountryId").IsUnique();
                dep.HasOne(d => d.Country).WithMany(c => c.Teams).OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}