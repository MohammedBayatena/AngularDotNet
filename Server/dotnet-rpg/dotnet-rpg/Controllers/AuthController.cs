using dotnet_rpg.Contracts;
using dotnet_rpg.Data;
using dotnet_rpg.Entities;
using dotnet_rpg.Models.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        //@Route /Register
        //@Desc  Allows User To Register a new account
        //@Request Username , Password
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterModel request)
        {
            var response = await _authRepository.Register(
                new User { Username = request.Username }, request.Password
            );
            return response.success ? Ok(response) : BadRequest(response);
        }

        //@Route /Login
        //@Desc  Allows User To Login
        //@Request Username , Password
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<AuthDispatchDto>>> Login(UserLoginModel request)
        {
            var response = await _authRepository.Login(request.Username, request.Password);
            return response.success ? Ok(response) : BadRequest(response);
        }
    }
}