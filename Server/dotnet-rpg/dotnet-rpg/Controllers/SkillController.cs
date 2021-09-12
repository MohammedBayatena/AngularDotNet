using dotnet_rpg.Contracts;
using dotnet_rpg.Manager;
using dotnet_rpg.Models.Skill;
using dotnet_rpg.Resource.Skill;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class SkillController : ControllerBase
    {
        private readonly ISkillManager _skillManager;

        public SkillController(ISkillManager skillManager)
        {
            _skillManager = skillManager;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<SkillResource>>>> GetAllSkills()
        {
            return Ok(await _skillManager.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<SkillResource>>> AddSkill(SkillModel newSkill)
        {
            return Ok(await _skillManager.AddSkill(newSkill));
        }
    }
}