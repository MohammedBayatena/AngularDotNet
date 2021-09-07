using dotnet_rpg.Dtos.Skill;
using dotnet_rpg.Models;
using dotnet_rpg.Services.SkillService;
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
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetSkillDto>>>> GetAllSkills()
        {
            return Ok(await _skillService.GetAllSkills());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetSkillDto>>> AddSkill(AddSkillDto newSkill)
        {
            return Ok(await _skillService.AddSkill(newSkill));
        }
    }
}