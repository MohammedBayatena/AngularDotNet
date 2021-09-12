using dotnet_rpg.Contracts;
using dotnet_rpg.Manager;
using dotnet_rpg.Models.Weapon;
using dotnet_rpg.Resource.Character;
using dotnet_rpg.Resource.User;
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
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponManager _weaponManager;

        public WeaponController(IWeaponManager weaponManager)
        {
            _weaponManager = weaponManager;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<UserResource>>>> GetAllWeapons()
        {
            return Ok(await _weaponManager.GetAllWeapons());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<CharacterResource>>> addWeapon(WeaponModel weapon)
        {
            try
            {
                return Ok(await _weaponManager.AddWeapon(weapon));
            }
            catch (Exception e)
            {
                return NotFound($"Couldn't Add Weapon: {e}");
            }
        }
    }
}