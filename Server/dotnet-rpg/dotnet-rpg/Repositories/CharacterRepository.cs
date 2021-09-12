using dotnet_rpg.Data;
using dotnet_rpg.Entities;
using dotnet_rpg.Models.Character;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Repositories
{
    public interface ICharacterRepository
    {
        Task<List<Character>> GetAllAsync();

        Task<Character> GetAsync(int id);

        //Task<Book> GetAsyncWithoutAuthors(int id);
        Task DeleteAsync(Character character);

        Task<Character> UpdateAsync(int Id, Character character);

        Task<Character> CreateAsync(Character character);

        Task<List<Character>> GetUserCharactersAsync();

        Task AddCharacterSkillAsync(List<CharacterModelWithSkill> newCharacterSkillList);
    }

    public class CharacterRepository : ICharacterRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public CharacterRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<Character> CreateAsync(Character character)
        {
            character.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
            _context.Characters.Add(character);
            await saveChanges();
            return character;
        }

        public async Task DeleteAsync(Character character)
        {
            _context.Characters.Remove(character);
            await saveChanges();
        }

        public async Task<List<Character>> GetAllAsync()
        {
            var characters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Include(c => c.User)
                .ToListAsync();
            return characters;
        }

        public async Task<Character> GetAsync(int id)
        {
            var dbCharacter = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == id && c.User.Id == GetUserId());
            return dbCharacter;
        }

        public async Task<Character> UpdateAsync(int Id, Character character)
        {
            Character oldCharacter = await _context.Characters
                    .Include(c => c.User)
                    .Include(c => c.Skills)
                    .Include(c => c.Weapon)
                    .FirstOrDefaultAsync(c => c.Id == Id);
            if (oldCharacter == null)
            {
                throw new Exception("Character Not Found");
            }
            if (oldCharacter.User.Id == GetUserId())
            {
                //oldcharacter = _mapper.map<Character>(newcharacter); // This Does the same as below but using auto mapper
                oldCharacter.Name = character.Name;
                oldCharacter.HitPoints = character.HitPoints;
                oldCharacter.Intelligence = character.Intelligence;
                oldCharacter.Defence = character.Defence;
                oldCharacter.Type = character.Type;
                oldCharacter.Strength = character.Strength;
                //oldCharacter.Skills.Clear(); //Enable if we want the characters to be able to add skills with request
                Weapon updatedWeapon = character.Weapon;
                oldCharacter.Weapon.Name = updatedWeapon.Name;
                oldCharacter.Weapon.Damage = updatedWeapon.Damage;
                //oldCharacter.Skills = newCharacter.Skills.Select(s => _mapper.Map<Skill>(s)).ToList();
                await saveChanges();
            }
            return oldCharacter;
        }

        public async Task<List<Character>> GetUserCharactersAsync()
        {
            var userId = GetUserId();
            var userCharacters = await _context.Characters
               .Include(c => c.Weapon)
               .Include(c => c.Skills)
               .Where(c => c.User.Id == userId).ToListAsync();
            return userCharacters;
        }

        public async Task AddCharacterSkillAsync(List<CharacterModelWithSkill> newCharacterSkillList)
        {
            foreach (CharacterModelWithSkill skil in newCharacterSkillList)
            {
                var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == skil.CharacterId && c.User.Id == GetUserId());
                if (character == null)
                {
                    throw new Exception("Character Not Found Exception");
                }
                character.Skills.Clear();
            }

            foreach (CharacterModelWithSkill skil in newCharacterSkillList)
            {
                var character = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == skil.CharacterId && c.User.Id == GetUserId());

                var skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == skil.SkillId);
                if (skill == null)
                {
                    throw new Exception("Skill Not Found Exception");
                }
                character.Skills.Add(skill);
            }
            await _context.SaveChangesAsync();
        }

        private async Task saveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}