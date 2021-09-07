using dotnet_rpg.Dtos.User;
using dotnet_rpg.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_rpg.Data
{
    public interface IAuthRepository
    {
        Task<ServiceResponse<int>> Register(User user, string password);
        Task<ServiceResponse<AuthDispatchDto>> Login(string username, string password);
        Task<bool> UserExists(string username);
    }
}
