using dotnet_rpg.Data;
using dotnet_rpg.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dotnet_rpg.Repositories
{
    public interface ISkillRepository
    {
        Task<List<Skill>> GetAllAsync();

        Task<Skill> CreateAsync(Skill newSkill);

        //Task<SkillResource> GetAsync(int id);
        //Task DeleteAsync(Skill skill);
        //Task<Skill> UpdateAsync(int Id, Skill skill);
    }

    public class SkillRepository : ISkillRepository
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly DataContext _context;

        public SkillRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }

        public async Task<Skill> CreateAsync(Skill newSkill)
        {
            _context.Skills.Add(newSkill);
            await _context.SaveChangesAsync();
            return newSkill;
        }

        public async Task<List<Skill>> GetAllAsync()
        {
            var skills = await _context.Skills.ToListAsync();
            return skills;
        }
    }
}