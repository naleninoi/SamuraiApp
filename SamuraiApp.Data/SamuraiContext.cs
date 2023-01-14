using Microsoft.EntityFrameworkCore;
using SamuraiApp.Domain;

namespace SamuraiApp.Data;

public class SamuraiContext: DbContext
{
    public DbSet<Samurai> Samurais { get; set; } = null!;
    public DbSet<Quote> Quotes { get; set; } = null!;
    public DbSet<Battle> Battles { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=samurai_ch4;Username=postgres;Password=sa");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Samurai>()
            .HasMany(s => s.Battles)
            .WithMany(b => b.Samurais)
            .UsingEntity<BattleSamurai>
            (bs => bs.HasOne<Battle>().WithMany(),
                bs => bs.HasOne<Samurai>().WithMany())
            .Property(bs => bs.DateJoined)
            .HasDefaultValueSql("now()");
    }
}