using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data;

public class SamuraiContext: DbContext
{
    public DbSet<Samurai> Samurais { get; set; } = null!;
    public DbSet<Quote> Quotes { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=samurai_ch3;Username=postgres;Password=sa");
    }
}