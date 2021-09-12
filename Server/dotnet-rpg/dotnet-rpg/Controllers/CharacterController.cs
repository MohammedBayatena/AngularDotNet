using dotnet_rpg.Contracts;
using dotnet_rpg.Manager;
using dotnet_rpg.Models.Character;
using dotnet_rpg.Resource.Character;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterManager _characterManager;

        public CharacterController(ICharacterManager characterManager)
        {
            this._characterManager = characterManager;
        }

        [HttpPost("skill")]
        public async Task<ActionResult<CharacterResource>> AddCharacterSkill(List<CharacterModelWithSkill> newCharacterSkillList)
        {
            return Ok(await _characterManager.AddCharacterSkill(newCharacterSkillList));
        }

        [HttpGet("GetUserCharacters")]
        public async Task<ActionResult<List<CharacterResource>>> GetUserCharacters()
        {
            return Ok(await _characterManager.GetUserCharacters());
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<CharacterWithUserResource>>>> GetAll()
        {
            return Ok(await _characterManager.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CharacterResource>> GetSingle(int id)
        {
            return Ok(await _characterManager.GetSingle(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<CharacterResource>>> AddCharacter(CharacterModel character)
        {
            return Ok(await _characterManager.AddCharacter(character));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<CharacterResource>>> UpdateCharacter(int id, CharacterModel newCharacter)
        {
            try
            {
                return Ok(await _characterManager.UpdateCharacter(id, newCharacter));
            }
            catch (Exception e)
            {
                return NotFound("Update Failed Character Not Found in DataBase!!"+e.Message);
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<CharacterResource>> DeleteCharacter(int id)
        {
            try
            {
                return Ok(await _characterManager.DeleteCharacter(id));
            }
            catch
            {
                return NotFound("Character Not Found in DataBase!!");
            }
        }
    }
}