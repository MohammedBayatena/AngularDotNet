using dotnet_rpg.Dtos.Character;
using dotnet_rpg.Dtos.Weapon;
using dotnet_rpg.Models;
using dotnet_rpg.Services.WeaponService;
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
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceResponse<List<GetWeaponDto>>>> GetAllWeapons()
        {
            return Ok(await _weaponService.GetAllWeapons());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> addWeapon(AddWeaponDto weapon)
        {
            try
            {
                return Ok(await _weaponService.AddWeapon(weapon));
            }
            catch (Exception e)
            {
                return NotFound($"Couldn't Add Weapon: {e}");
            }
        }
    }
}