using dotnet_rpg.Data;
using dotnet_rpg.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using TrackableEntities.Common.Core;
using TrackableEntities.EF.Core;

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
            //var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId()); 
            // In Trackable we can remove this and just do load related then flag as added then apply changes and save
            await _context.LoadRelatedEntitiesAsync(newWeapon);
            if (newWeapon == null || newWeapon.Character.User.Id != GetUserId() ) { return null; }
            newWeapon.TrackingState = TrackingState.Added;
            _context.ApplyChanges(newWeapon);
            await _context.SaveChangesAsync();
            _context.AcceptChanges(newWeapon);
            return newWeapon.Character;
        }

        public async Task<List<Weapon>> GetAllAsync()
        {
            var weapons = await _context.Weapons.ToListAsync();
            return weapons;
        }
    }
}