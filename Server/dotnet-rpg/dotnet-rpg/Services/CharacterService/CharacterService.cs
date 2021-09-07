using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public CharacterService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<List<GetCharacterWithUserDto>>> GetAll()
        {
            var ServiceResponse = new ServiceResponse<List<GetCharacterWithUserDto>>();
            //change every item to ad Get DTO then return that as a list
            var Characters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Include(c => c.User)
                .ToListAsync();
            ServiceResponse.Data = Characters.Select(c => _mapper.Map<GetCharacterWithUserDto>(c)).ToList();
            ServiceResponse.Message = "All Characters Read Successfully";
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetUserCharacters()
        {
            var userId = GetUserId();
            var response = new ServiceResponse<List<GetCharacterDto>>();
            var userCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User.Id == userId).ToListAsync();
            response.Data = userCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            response.Message = $"All Characters of User {userId} Successfully";
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetSingle(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            //change the return type to DTO from character
            var dbCharacter = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            try
            {
                ServiceResponse.Data = _mapper.Map<GetCharacterDto>(dbCharacter);
                ServiceResponse.Message = ServiceResponse.Data != null ? "Character have been Found! Success!" : "Character Was not Found!";
                ServiceResponse.success = ServiceResponse.Data != null ? true : false;
            }
            catch (Exception e)
            {
                ServiceResponse.success = false;
                ServiceResponse.Message = e.Message;
            }
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterWithUserDto>> AddCharacter(AddCharacterDto character)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterWithUserDto>();
            //change to character from add character
            Character newCharacter = _mapper.Map<Character>(character);
            newCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(newCharacter);
            await _context.SaveChangesAsync();
            ServiceResponse.Data = _mapper.Map<GetCharacterWithUserDto>(newCharacter);
            ServiceResponse.Message = "Character was added Successfully!";
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character characterToBeDelete = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
                _context.Characters.Remove(characterToBeDelete);
                await _context.SaveChangesAsync();
                ServiceResponse.Data = _mapper.Map<GetCharacterDto>(characterToBeDelete);
            }
            catch (Exception e)
            {
                ServiceResponse.success = false;
                ServiceResponse.Message = e.Message;
            }
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int id, AddCharacterDto newCharacter)
        {
            var ServiceResponse = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character oldCharacter = await _context.Characters
                    .Include(c => c.User)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == id);
                if (oldCharacter.User.Id == GetUserId())
                {
                    //oldcharacter = _mapper.map<character>(newcharacter); // This Does the same as below but using auto mapper
                    oldCharacter.Name = newCharacter.Name;
                    oldCharacter.HitPoints = newCharacter.HitPoints;
                    oldCharacter.Intelligence = newCharacter.Intelligence;
                    oldCharacter.Defence = newCharacter.Defence;
                    oldCharacter.Type = newCharacter.Type;
                    oldCharacter.Strength = newCharacter.Strength;
                    //oldCharacter.Skills.Clear();
                    //oldCharacter.Skills = newCharacter.Skills.Select(s => _mapper.Map<Skill>(s)).ToList();
                    ServiceResponse.Data = _mapper.Map<GetCharacterDto>(oldCharacter);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    ServiceResponse.success = false;
                    ServiceResponse.Message = "Cant Update Character! Wrong privileges or character not found";
                }
            }
            catch (Exception e)
            {
                ServiceResponse.success = false;
                ServiceResponse.Message = e.Message;
            }
            return ServiceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(List<AddCharacterSkillDto> newCharacterSkillList)
        {
            var response = new ServiceResponse<GetCharacterDto>();

            try
            {
                foreach (AddCharacterSkillDto skil in newCharacterSkillList)
                {
                    var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == skil.CharacterId && c.User.Id == GetUserId());
                    if (character == null)
                    {
                        response.success = false;
                        response.Message = "Character not Found";
                        return response;
                    }
                    character.Skills.Clear();
                }

                foreach (AddCharacterSkillDto skil in newCharacterSkillList)
                {
                    var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == skil.CharacterId && c.User.Id == GetUserId());

                    if (character == null)
                    {
                        response.success = false;
                        response.Message = "Character not Found";
                        return response;
                    }

                    var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == skil.SkillId);

                    if (skill == null)
                    {
                        response.success = false;
                        response.Message = "Skill not Found";
                        return response;
                    }
                    character.Skills.Add(skill);
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetCharacterDto>(character);
                }
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Adding character skill failed :  {e.Message}";
            }
            return response;
        }
    }
}