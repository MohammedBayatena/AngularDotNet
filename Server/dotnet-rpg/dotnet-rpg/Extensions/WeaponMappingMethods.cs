using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg.Extensions
{
    public static class WeaponMappingMethods
    {
        public static GetWeaponDto MapWeapontoGetDto(this Weapon weapon)
        {
            return new GetWeaponDto
            {
                Name = weapon.Name,
                Damage = weapon.Damage,
            };
        }
        public static Weapon AddWeaponDtoToWeapon(this AddWeaponDto addWeapon)
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