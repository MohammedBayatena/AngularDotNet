using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace dotnet_rpg.Services.WeaponService
{
    public class WeaponService : IWeaponService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public WeaponService(DataContext context, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            var response = new ServiceResponse<GetCharacterDto>();
            try
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == GetUserId());
                if (character == null) { response.success = false; response.Message = "Character Not Found!"; return response; }
                character.Weapon = _mapper.Map<Weapon>(newWeapon);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Cant Add Weapon : {e.Message} ";
            }
            return response;
        }

        public async Task<ServiceResponse<List<GetWeaponDto>>> GetAllWeapons()
        {
            var response = new ServiceResponse<List<GetWeaponDto>>();
            try
            {
                var weapons = await _context.Weapons.ToListAsync();
                response.Data = weapons.Select(w => _mapper.Map<GetWeaponDto>(w)).ToList();
                response.Message = "All Weapons Read Successfully";
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Error Getting Weapons from database {e.Message}";
            }
            return response;
        }
    }
}