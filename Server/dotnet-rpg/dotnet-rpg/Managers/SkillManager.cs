using dotnet_rpg.Contracts;
using dotnet_rpg.Extensions;
using dotnet_rpg.Models.Skill;
using dotnet_rpg.Repositories;
using dotnet_rpg.Resource.Skill;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Manager
{
    public interface ISkillManager
    {
        Task<ServiceResponse<List<SkillResource>>> GetAll();

        Task<ServiceResponse<SkillResource>> AddSkill(SkillModel character);
    }

    public class SkillManager : ISkillManager
    {
        private readonly ISkillRepository _skillRepository;

        public SkillManager(ISkillRepository skillRepository)
        {
            _skillRepository = skillRepository;
        }

        public async Task<ServiceResponse<SkillResource>> AddSkill(SkillModel newSkill)
        {
            var response = new ServiceResponse<SkillResource>();
            try
            {
                var skillEntity = newSkill.MapSkillModelToEntity();
                var skill = await _skillRepository.CreateAsync(skillEntity);
                var skillResource = skill.MapSkillEntitytoResource();
                response.Data = skillResource;
                response.Message = "Added Skill Successfully";
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Error Adding Skill {e.Message}";
            }
            return response;
        }

        public async Task<ServiceResponse<List<SkillResource>>> GetAll()
        {
            var response = new ServiceResponse<List<SkillResource>>();
            try
            {
                var skills = await _skillRepository.GetAllAsync();
                response.Data = skills.Select(s => s.MapSkillEntitytoResource()).ToList();
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