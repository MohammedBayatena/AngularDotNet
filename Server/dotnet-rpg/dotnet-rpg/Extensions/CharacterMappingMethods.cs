using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using System.Linq;

namespace dotnet_rpg.Extensions
{
    public static class CharacterMappingMethods
    {
        public static GetCharacterDto MapCharactertoGetDto(this Character character)
        {
            return new GetCharacterDto
            {
                Id = character.Id,
                Name = character.Name,
                Intelligence = character.Intelligence,
                Defence = character.Defence,
                HitPoints = character.HitPoints,
                Skills = (character.Skills != null) ? character.Skills.Select(s => s.MapSkilltoGetDto()).ToList() : null,
                Strength = character.Strength,
                Weapon = (character.Weapon != null) ? character.Weapon.MapWeapontoGetDto() : null,
                Type = character.Type,
            };
        }

        public static GetCharacterWithUserDto MapCharacterToGetWithUserDto(this Character character)
        {
            return new GetCharacterWithUserDto
            {
                Id = character.Id,
                Name = character.Name,
                Intelligence = character.Intelligence,
                Defence = character.Defence,
                HitPoints = character.HitPoints,
                Skills = (character.Skills != null) ? character.Skills.Select(s => s.MapSkilltoGetDto()).ToList() : null,
                Strength = character.Strength,
                Weapon = (character.Weapon != null) ? character.Weapon.MapWeapontoGetDto() : null,
                Type = character.Type,
                User = character.User.MapUsertoGetDto(),
            };
        }

        public static Character MapAddDtoToCharacter(this AddCharacterDto addCharacter)
        {
            return new Character
            {
                Name = addCharacter.Name,
                Intelligence = addCharacter.Intelligence,
                Defence = addCharacter.Defence,
                HitPoints = addCharacter.HitPoints,
                Skills = (addCharacter.Skills != null) ? addCharacter.Skills.Select(s => s.MapAddSkillDtoToSkill()).ToList() : null,
                Strength = addCharacter.Strength,
                Weapon = (addCharacter.Weapon != null) ? addCharacter.Weapon.AddWeaponDtoToWeapon() : null,
                Type = addCharacter.Type,
            };
        }
    }
}