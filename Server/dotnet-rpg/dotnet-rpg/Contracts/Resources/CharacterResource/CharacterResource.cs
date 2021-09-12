using dotnet_rpg.Entities;
using dotnet_rpg.Enums;
using dotnet_rpg.Resource.Skill;
using dotnet_rpg.Resource.Weapon;
using System.Collections.Generic;

namespace dotnet_rpg.Resource.Character
{
    public class CharacterResource
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Character";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public WeaponResource Weapon { get; set; }
        public List<SkillResource> Skills { get; set; }
        public RpgClass Type { get; set; } = RpgClass.Knight;
    }
}