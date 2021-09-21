using dotnet_rpg.AsyncDataServices;
using dotnet_rpg.Contracts;
using dotnet_rpg.Contracts.Models.SkillModels;
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
        //private readonly IMessageBusClient _messageBusClient;
        //public SkillController(ISkillManager skillManager, IMessageBusClient messageBusClient)

        public SkillController(ISkillManager skillManager)
        {
            _skillManager = skillManager;
            //_messageBusClient = messageBusClient;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<SkillResource>>>> GetAllSkills()
        {
            return Ok(await _skillManager.GetAll());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<SkillResource>>> AddSkill(SkillModel newSkill)
        {
            ServiceResponse<SkillResource> addedskill = await _skillManager.AddSkill(newSkill);
            try
            {
                SkillPublishModel skillpuplished = new SkillPublishModel();
                skillpuplished.Name = addedskill.Data.Name;
                skillpuplished.Id = addedskill.Data.Id;
                skillpuplished.Event = "Skill_Published";
                //_messageBusClient.PublishNewSkill(skillpuplished);
            }
            catch (System.Exception)
            {
                throw;
            }
            return Ok(addedskill);
        }
    }
}