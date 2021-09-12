using dotnet_rpg.Data;
using dotnet_rpg.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Repositories
{
    public interface IWeaponRepository
    {
        Task<List<Weapon>> GetAllAsync();

        Task<Character> CreateAsync(Weapon newWeapon);
    }

    public class WeaponRepository : IWeaponRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeaponRepository(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<Character> CreateAsync(Weapon newWeapon)
        {
            var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
            if (character == null) { return null; }
            await _context.SaveChangesAsync();
            return character;
        }

        public async Task<List<Weapon>> GetAllAsync()
        {
            var weapons = await _context.Weapons.ToListAsync();
            return weapons;
        }
    }
}