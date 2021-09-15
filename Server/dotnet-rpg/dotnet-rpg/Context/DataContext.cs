using dotnet_rpg.Entities;
using dotnet_rpg.EntityConfigurations;
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
            modelBuilder.ApplyConfiguration(new WeaponConfiguration());

            modelBuilder.Entity<User>()
                .HasMany(u => u.Characters)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);

            modelBuilder.Entity<Character>()
            .HasMany(c => c.Skills)
            .WithMany(c => c.Characters)
            .UsingEntity<CharacterSkill>(
                j => j
                    .HasOne(cs => cs.Skill)
                    .WithMany(s => s.CharacterSkill)
                    .HasForeignKey(cs => cs.SkillsId),
                j => j
                    .HasOne(cs => cs.Character)
                    .WithMany(c => c.CharacterSkill)
                    .HasForeignKey(cs => cs.CharactersId),
                j =>
                {
                    j.Property(pt => pt.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                    j.HasKey(t => new { t.CharactersId, t.SkillsId });
                });

            modelBuilder.Entity<Skill>()
                .HasData(
                new Skill { Id = 1, Name = "Great Slash", Damage = 10 },
                new Skill { Id = 2, Name = "Fire Ball", Damage = 15 },
                new Skill { Id = 3, Name = "Heal", Damage = 50 }
            );
        }
    }
}