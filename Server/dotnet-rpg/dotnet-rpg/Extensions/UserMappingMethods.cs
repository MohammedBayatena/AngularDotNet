using dotnet_rpg.Dtos.User;
using dotnet_rpg.Models;

namespace dotnet_rpg.Extensions
{
    public static class UserMappingMethods
    {
        public static UserGetDto MapUsertoGetDto(this User user)
        {
            return new UserGetDto
            {
                Username = user.Username,
            };
        }
    }
}
