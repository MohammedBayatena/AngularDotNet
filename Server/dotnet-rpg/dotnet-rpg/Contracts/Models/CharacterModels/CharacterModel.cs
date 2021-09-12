using dotnet_rpg.Models.Skill;
using dotnet_rpg.Models.Weapon;
using dotnet_rpg.Entities;
using System.Collections.Generic;
using dotnet_rpg.Enums;

namespace dotnet_rpg.Models.Character
{
    public class CharacterModel
    {
        public string Name { get; set; } = "Character";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public WeaponModel Weapon { get; set; }
        public List<SkillModel> Skills { get; set; }
        public RpgClass Type { get; set; } = RpgClass.Knight;
    }
}