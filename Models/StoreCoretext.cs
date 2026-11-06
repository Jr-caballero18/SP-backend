using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public class StoreContext : DbContext
{
    public StoreContext(DbContextOptions<StoreContext> options) : base(options)
    {
    }

    public DbSet<Beer> Beers { get; set; }
    public DbSet<Brand> Brands { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Reto 1: Índice único en la base de datos para Brand.Name
        modelBuilder.Entity<Brand>()
            .HasIndex(b => b.Name)
            .IsUnique();
    }
}