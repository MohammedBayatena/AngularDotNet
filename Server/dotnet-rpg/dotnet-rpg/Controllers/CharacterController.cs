using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
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
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        [HttpPost("skill")]
        public async Task<ActionResult<GetCharacterDto>> AddCharacterSkill( List<AddCharacterSkillDto> newCharacterSkillList)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkillList));
        }

        [HttpGet("GetUserCharacters")]
        public async Task<ActionResult<List<GetCharacterDto>>> GetUserCharacters()
        {
            return Ok(await _characterService.GetUserCharacters());
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterWithUserDto>>>> GetAll()
        {
            return Ok(await _characterService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetCharacterDto>> GetSingle(int id)
        {
            return Ok(await _characterService.GetSingle(id));
        }

        [HttpPost]
        public async Task<ActionResult<List<GetCharacterDto>>> AddCharacter(AddCharacterDto character)
        {
            return Ok(await _characterService.AddCharacter(character));
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<GetCharacterDto>>> UpdateCharacter(int id, AddCharacterDto newCharacter)
        {
            try
            {
                return Ok(await _characterService.UpdateCharacter(id, newCharacter));
            }
            catch (Exception e)
            {
                return NotFound("Update Failed Character Not Found in DataBase!!" + e);
            }
        }

        [HttpDelete("id")]
        public async Task<ActionResult<GetCharacterDto>> DeleteCharacter(int id)
        {
            try
            {
                return Ok(await _characterService.DeleteCharacter(id));
            }
            catch
            {
                return NotFound("Character Not Found in DataBase!!");
            }
        }
    }
}