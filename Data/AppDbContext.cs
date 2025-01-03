using Back_End.Models;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<MaterialModel> Material { get; set; }

        public DbSet<SupplierModel> Supplier { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MaterialModel>().ToTable("Material");
            modelBuilder.Entity<SupplierModel>().ToTable("Supplier");
        }


    }
}
