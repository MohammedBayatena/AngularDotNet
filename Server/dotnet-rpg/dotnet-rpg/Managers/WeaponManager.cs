using dotnet_rpg.Contracts;
using dotnet_rpg.Extensions;
using dotnet_rpg.Models.Weapon;
using dotnet_rpg.Repositories;
using dotnet_rpg.Resource.Character;
using dotnet_rpg.Resource.Weapon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Manager
{
    public interface IWeaponManager
    {
        Task<ServiceResponse<CharacterResource>> AddWeapon(WeaponModel newWeapon);

        Task<ServiceResponse<List<WeaponResource>>> GetAllWeapons();
    }

    public class WeaponManager : IWeaponManager
    {
        private readonly IWeaponRepository _weaponRepository;

        public WeaponManager(IWeaponRepository weaponRepository)
        {
            _weaponRepository = weaponRepository;
        }

        public async Task<ServiceResponse<CharacterResource>> AddWeapon(WeaponModel newWeapon)
        {
            var response = new ServiceResponse<CharacterResource>();
            try
            {
                var weapon = newWeapon.MapWeaponModelToEntity();
                var character = await _weaponRepository.CreateAsync(weapon);
                if (character == null) { response.success = false; response.Message = "Character Not Found!"; return response; }
                character.Weapon = newWeapon.MapWeaponModelToEntity();
                response.Data = character.MapCharacterEntitytoResource();
            }
            catch (Exception e)
            {
                response.success = false;
                response.Message = $"Cant Add Weapon : {e.Message} ";
            }
            return response;
        }

        public async Task<ServiceResponse<List<WeaponResource>>> GetAllWeapons()
        {
            var response = new ServiceResponse<List<WeaponResource>>();
            try
            {
                var weapons = await _weaponRepository.GetAllAsync();
                response.Data = weapons.Select(w => w.MapWeaponEntitytoResource()).ToList();
                response.Message = "All Weapons Retrieved Successfully";
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