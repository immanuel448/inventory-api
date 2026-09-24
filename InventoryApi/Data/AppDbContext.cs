using InventoryApi.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(150);

            entity.Property(p => p.Description)
                .HasMaxLength(500);

            entity.Property(p => p.Price)
                .HasPrecision(18, 2);

            entity.Property(p => p.Stock)
                .IsRequired();

            entity.Property(p => p.IsActive)
                .IsRequired();

            entity.Property(p => p.CreatedAt)
                .IsRequired();

            entity.Property(p => p.UpdatedAt)
                .IsRequired(false);
        });
    }
}