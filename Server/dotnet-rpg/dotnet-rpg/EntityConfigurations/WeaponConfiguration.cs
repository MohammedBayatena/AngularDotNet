using dotnet_rpg.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dotnet_rpg.EntityConfigurations
{
    public class WeaponConfiguration : IEntityTypeConfiguration<Weapon>
    {
        public void Configure(EntityTypeBuilder<Weapon> builder)
        {
            builder.ToTable("Weapons");

            builder.HasKey(c => c.Id);

            builder.Property(w => w.Damage)
                .IsRequired();

            builder.Property(w => w.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .HasOne(c => c.Character)
                .WithOne(c => c.Weapon);
        }
    }
}