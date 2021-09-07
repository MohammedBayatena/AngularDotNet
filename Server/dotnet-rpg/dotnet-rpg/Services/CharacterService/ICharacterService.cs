using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterWithUserDto>>> GetAll();

        Task<ServiceResponse<GetCharacterDto>> GetSingle(int id);

        Task<ServiceResponse<List<GetCharacterDto>>> GetUserCharacters();

        Task<ServiceResponse<GetCharacterWithUserDto>> AddCharacter(AddCharacterDto character);

        Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(int id, AddCharacterDto newCharacter);

        Task<ServiceResponse<GetCharacterDto>> DeleteCharacter(int id);

        Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill( List<AddCharacterSkillDto> newCharacterSkillList);


    }
}