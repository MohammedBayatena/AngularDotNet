using dotnet_rpg.Entities;
using dotnet_rpg.Models.Skill;
using dotnet_rpg.Resource.Skill;

namespace dotnet_rpg.Extensions
{
    public static class SkillMapping
    {
        public static SkillResource MapSkillEntitytoResource(this Skill skill)
        {
            return new SkillResource
            {
                Name = skill.Name,
                Damage = skill.Damage,
                Id = skill.Id,
            };
        }

        public static Skill MapSkillModelToEntity(this SkillModel addSkill)
        {
            return new Skill
            {
                Name = addSkill.Name,
                Damage = addSkill.Damage,
            };
        }
    }
}