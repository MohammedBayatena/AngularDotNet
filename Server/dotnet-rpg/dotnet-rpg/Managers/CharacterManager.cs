using dotnet_rpg.Contracts;
using dotnet_rpg.Entities;
using dotnet_rpg.Extensions;
using dotnet_rpg.Models.Character;
using dotnet_rpg.Repositories;
using dotnet_rpg.Resource.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Manager
{
    public interface ICharacterManager
    {
        Task<ServiceResponse<List<CharacterWithUserResource>>> GetAll();

        Task<ServiceResponse<CharacterResource>> GetSingle(int id);

        Task<ServiceResponse<List<CharacterResource>>> GetUserCharacters();

        Task<ServiceResponse<CharacterWithUserResource>> AddCharacter(CharacterModel character);

        Task<ServiceResponse<CharacterResource>> UpdateCharacter(int id, CharacterModel newCharacter);

        Task<ServiceResponse<CharacterResource>> DeleteCharacter(int id);

        Task<ServiceResponse<string>> AddCharacterSkill(List<CharacterModelWithSkill> newCharacterSkillList);
    }

    public class CharacterManager : ICharacterManager
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterManager(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        public async Task<ServiceResponse<CharacterWithUserResource>> AddCharacter(CharacterModel character)
        {
            var ServiceResponse = new ServiceResponse<CharacterWithUserResource>();
            Character characterEntity = character.MapCharacterModelToEntity();
            Character addedCharacter = await _characterRepository.CreateAsync(characterEntity);
            var characterResource = addedCharacter.MapCharacterEntityToResourceWithUser();
            ServiceResponse.Data = characterResource;
            ServiceResponse.Message = "Character was added Successfully!";
            return ServiceResponse;
        }

        public async Task<ServiceResponse<string>> AddCharacterSkill(List<CharacterModelWithSkill> newCharacterSkillList)
        {
            var response = new ServiceResponse<string>();

            try { await _characterRepository.AddCharacterSkillAsync(newCharacterSkillList); }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Error ${e.Message}";
                return response;
            }
            response.Data = "Adding all skills successfully;";
            return response;
        }

        public async Task<ServiceResponse<CharacterResource>> DeleteCharacter(int id)
        {
            var ServiceResponse = new ServiceResponse<CharacterResource>();
            Character characterToBeDelete = await _characterRepository.GetAsync(id);
            if (characterToBeDelete == null)
            {
                ServiceResponse.success = false;
                ServiceResponse.Message = "The Character you are trying to delete either doesn't exist or not enough permissions";
                return ServiceResponse;
            }
            ServiceResponse.Data = characterToBeDelete.MapCharacterEntitytoResource();
            await _characterRepository.DeleteAsync(characterToBeDelete);
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<CharacterWithUserResource>>> GetAll()
        {
            var ServiceResponse = new ServiceResponse<List<CharacterWithUserResource>>();
            var Characters = await _characterRepository.GetAllAsync();
            var characterslist = Characters.Select(c => c.MapCharacterEntityToResourceWithUser()).ToList();
            ServiceResponse.Data = characterslist;
            ServiceResponse.Message = "All Characters Read Successfully";
            return ServiceResponse;
        }

        public async Task<ServiceResponse<CharacterResource>> GetSingle(int id)
        {
            var ServiceResponse = new ServiceResponse<CharacterResource>();
            Character characterEntity = await _characterRepository.GetAsync(id);
            if (characterEntity == null)
            {
                ServiceResponse.success = false;
                ServiceResponse.Message = "Character not Found";
                return ServiceResponse;
            }
            CharacterResource characterResource = characterEntity.MapCharacterEntitytoResource();
            ServiceResponse.Data = characterResource;
            ServiceResponse.Message = ServiceResponse.Data != null ? "Character have been Found! Success!" : "Character Was not Found!";
            ServiceResponse.success = ServiceResponse.Data != null ? true : false;
            return ServiceResponse;
        }

        public async Task<ServiceResponse<List<CharacterResource>>> GetUserCharacters()
        {
            var response = new ServiceResponse<List<CharacterResource>>();
            var userCharacters = await _characterRepository.GetUserCharactersAsync();
            response.Data = userCharacters.Select(c => c.MapCharacterEntitytoResource()).ToList();
            response.Message = $"All Characters of User was obtained sucessfully";
            return response;
        }

        public async Task<ServiceResponse<CharacterResource>> UpdateCharacter(int id, CharacterModel updatedCharacter)
        {
            var ServiceResponse = new ServiceResponse<CharacterResource>();
            Character characterEntity = updatedCharacter.MapCharacterModelToEntity();
            Character updatedEntity = await _characterRepository.UpdateAsync(id, characterEntity);
            CharacterResource CharacterResource = updatedEntity.MapCharacterEntitytoResource();
            ServiceResponse.Data = CharacterResource;
            return ServiceResponse;
        }
    }
}