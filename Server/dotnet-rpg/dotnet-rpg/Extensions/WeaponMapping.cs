using dotnet_rpg.Entities;
using dotnet_rpg.Models.Weapon;
using dotnet_rpg.Resource.Weapon;

namespace dotnet_rpg.Extensions
{
    public static class WeaponMapping
    {
        public static WeaponResource MapWeaponEntitytoResource(this Weapon weapon)
        {
            return new WeaponResource
            {
                Name = weapon.Name,
                Damage = weapon.Damage,
            };
        }

        public static Weapon MapWeaponModelToEntity(this WeaponModel addWeapon)
        {
            return new Weapon
            {
                Name = addWeapon.Name,
                Damage = addWeapon.Damage,
                CharacterId = addWeapon.CharacterId,
            };
        }
    }
}