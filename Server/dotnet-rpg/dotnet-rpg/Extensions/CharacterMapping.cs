using dotnet_rpg.Entities;
using dotnet_rpg.Models.Character;
using dotnet_rpg.Resource.Character;
using System.Linq;

namespace dotnet_rpg.Extensions
{
    public static class CharacterMapping
    {
        public static CharacterResource MapCharacterEntitytoResource(this Character character)
        {
            return new CharacterResource
            {
                Id = character.Id,
                Name = character.Name,
                Intelligence = character.Intelligence,
                Defence = character.Defence,
                HitPoints = character.HitPoints,
                Skills = (character.Skills != null) ? character.Skills.Select(s => s.MapSkillEntitytoResource()).ToList() : null,
                Strength = character.Strength,
                Weapon = (character.Weapon != null) ? character.Weapon.MapWeaponEntitytoResource() : null,
                Type = character.Type,
            };
        }

        public static CharacterWithUserResource MapCharacterEntityToResourceWithUser(this Character character)
        {
            return new CharacterWithUserResource
            {
                Id = character.Id,
                Name = character.Name,
                Intelligence = character.Intelligence,
                Defence = character.Defence,
                HitPoints = character.HitPoints,
                Skills = (character.Skills != null) ? character.Skills.Select(s => s.MapSkillEntitytoResource()).ToList() : null,
                Strength = character.Strength,
                Weapon = (character.Weapon != null) ? character.Weapon.MapWeaponEntitytoResource() : null,
                Type = character.Type,
                User = character.User.MapUserEntitytoResource(),
            };
        }

        public static Character MapCharacterModelToEntity(this CharacterModel addCharacter)
        {
            return new Character
            {
                Name = addCharacter.Name,
                Intelligence = addCharacter.Intelligence,
                Defence = addCharacter.Defence,
                HitPoints = addCharacter.HitPoints,
                Skills = (addCharacter.Skills != null) ? addCharacter.Skills.Select(s => s.MapSkillModelToEntity()).ToList() : null,
                Strength = addCharacter.Strength,
                Weapon = (addCharacter.Weapon != null) ? addCharacter.Weapon.MapWeaponModelToEntity() : null,
                Type = addCharacter.Type,
            };
        }
    }
}