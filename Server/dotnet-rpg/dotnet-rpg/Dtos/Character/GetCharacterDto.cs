﻿using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Dtos.User;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using System.Collections.Generic;

namespace dotnet_rpg.Dtos.Character
{
    public class GetCharacterDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "Character";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defence { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }
        public RpgClass Type { get; set; } = RpgClass.Knight;
    }
}