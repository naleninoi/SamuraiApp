using Microsoft.EntityFrameworkCore;
using SamuraiApp.Data;
using SamuraiApp.Domain;

namespace SamuraiApp.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();

        private static void Main(string[] args)
        {
            _context.Database.EnsureCreated();
            // GetSamurais("Before Add:");
            // AddSamurais(new List<string> {"Toyama", "Kawasaki", "Hirowato"});
            // GetSamurais("After Add:");
            // GetSamuraisByName("Toyama");
            // AddBattles();
            // AddNewSamuraiToExistingBattle();
            RemoveSamuraiFromBattle();
        }

        private static void RemoveSamuraiFromBattle()
        {
            var battle = _context.Battles
                .Include(b => b.Samurais.Where(s => s.Id == 5))
                .SingleOrDefault(b => b.Id == 1);
            var samurai = battle.Samurais[0];
            battle.Samurais.Remove(samurai);
            _context.SaveChanges();
        }
        
        private static void AddNewSamuraiToExistingBattle()
        {
            var battle = _context.Battles.FirstOrDefault();
            battle.Samurais.Add(new Samurai {Name = "Maeukemi Tenkai"});
            _context.SaveChanges();
        }

        private static void AddBattles()
        {
            var firstBattle = new Battle()
            {
                Name = "Monte Carlo"
            };
            var secondBattle = new Battle()
            {
                Name = "Monte Negro"
            };
            _context.Battles.Add(firstBattle);
            _context.Battles.Add(secondBattle);
            _context.SaveChanges();
        }

        private static void AddSamuraisByName(List<string> names)
        {
            foreach (var name in names)
            {
                var samurai = new Samurai { Name = name };
                _context.Samurais.Add(samurai);
            }

            _context.SaveChanges();
        }

        private static void GetSamurais(string text)
        {
            var samurais = _context.Samurais.ToList();
            Console.WriteLine($"{text}: Samurai count is {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.Name);
            }
        }

        private static void GetSamuraisByName(string name)
        {
            var samurais = _context.Samurais
                .Where(s => s.Name == name)
                .Include(s => s.Horse)
                .ToList();
            Console.WriteLine($"Found samurais with name {name}: {samurais.Count}");
            foreach (var samurai in samurais)
            {
                Console.WriteLine(samurai.ToString());
            }
        }

        private static Samurai? GetSamuraiByName(string name)
        {
            return _context.Samurais
                .FirstOrDefault(s => s.Name == name);
        }

        private static void UpdateSamuraiName(Samurai samurai, string newName)
        {
            samurai.Name = newName;
            _context.SaveChanges();
            Console.WriteLine(samurai.ToString());
        }
    }
}