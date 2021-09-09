using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Extensions;
using dotnet_rpg.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.SkillService
{
    public class SkillService : ISkillService
    {
        private readonly DataContext _context;

        public SkillService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<GetSkillDto>> AddSkill(AddSkillDto newSkill)
        {
            var response = new ServiceResponse<GetSkillDto>();
            try
            {
                var skill = newSkill.MapAddSkillDtoToSkill();
                _context.Skills.Add(skill);
                await _context.SaveChangesAsync();
                response.Data = skill.MapSkilltoGetDto();
                response.Message = "Added Skill Successfully";
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Error Adding Skill {e.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetSkillDto>>> GetAllSkills()
        {
            var response = new ServiceResponse<List<GetSkillDto>>();
            try
            {
                var skills = await _context.Skills.ToListAsync();
                response.Data = skills.Select(s => s.MapSkilltoGetDto()).ToList();
                response.Message = "All Skills Retrieved Successfully";
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Error Getting Skills from database {e.Message}";
            }
            return response;
        }
    }
}