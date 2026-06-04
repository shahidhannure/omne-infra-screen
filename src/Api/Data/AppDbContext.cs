using Microsoft.EntityFrameworkCore;
using Omne.Screen.Api.Models;

namespace Omne.Screen.Api.Data;

public sealed class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Widget> Widgets => Set<Widget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Widget>(b =>
        {
            b.HasKey(w => w.Id);
            b.Property(w => w.Name).HasMaxLength(200).IsRequired();
        });

        // Seed a couple of rows so the endpoint returns something on a fresh DB.
        modelBuilder.Entity<Widget>().HasData(
            new Widget { Id = 1, Name = "Sprocket", Quantity = 42 },
            new Widget { Id = 2, Name = "Flange", Quantity = 7 });
    }
}
