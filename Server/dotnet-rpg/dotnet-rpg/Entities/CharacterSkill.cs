using System;

namespace dotnet_rpg.Entities
{
    public class CharacterSkill
    {
        public DateTime CreatedAt { get; set; }

        public int CharactersId { get; set; }
        public Character Character { get; set; }

        public int SkillsId { get; set; }
        public Skill Skill { get; set; }
    }
}