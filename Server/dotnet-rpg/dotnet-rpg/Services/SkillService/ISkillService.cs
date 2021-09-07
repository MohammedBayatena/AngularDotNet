using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.SkillService
{
    public interface ISkillService
    {

        Task<ServiceResponse<GetSkillDto>> AddSkill(AddSkillDto newSkill);
        Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills();



    }
}
