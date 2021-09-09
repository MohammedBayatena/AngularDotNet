using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;

namespace dotnet_rpg.Extensions
{
    public static class SkillMappingMethods
    {
        public static GetSkillDto MapSkilltoGetDto(this Skill skill)
        {
            return new GetSkillDto
            {
                Name = skill.Name,
                Damage = skill.Damage,
                Id = skill.Id,
            };
        }
        public static Skill MapAddSkillDtoToSkill(this AddSkillDto addSkill)
        {
            return new Skill
            {
                Name = addSkill.Name,
                Damage = addSkill.Damage,
            };
        }
    }
}