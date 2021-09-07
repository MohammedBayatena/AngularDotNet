using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; } // name of the table based on the model Character
        public DbSet<User> Users { get; set; } // name of the table based on the model User
        public DbSet<Weapon> Weapons { get; set; } // name of the table based on the model Weapon
        public DbSet<Skill> Skills { get; set; } // name of the table based on the model Skill

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>()
                .HasData(
                new Skill { Id = 1, Name = "Great Slash", Damage = 10 },
                new Skill { Id = 2, Name = "Fire Ball", Damage = 15 },
                new Skill { Id = 3, Name = "Heal", Damage = 50 }
            );
        }
    }
}