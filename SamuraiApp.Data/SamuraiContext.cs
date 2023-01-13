using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data;

public class SamuraiContext: DbContext
{
    public DbSet<Samurai> Samurais { get; set; } = null!;
    public DbSet<Quote> Quotes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5433;Database=samurai_ch2;Username=postgres;Password=sa");
    }
}