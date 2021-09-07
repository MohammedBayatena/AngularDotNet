using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using System.Collections.Generic;

namespace dotnet_rpg.Dtos.Character
{
    public class AddCharacterDto
    {
        public string Name { get; set; } = "Character";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public GetWeaponDto Weapon { get; set; }
        public List<AddSkillDto> Skills { get; set; }
        public RpgClass Type { get; set; } = RpgClass.Knight;
    }
}