using dotnet_rpg.Entities;
using dotnet_rpg.Resource.User;

namespace dotnet_rpg.Extensions
{
    public static class UserMapping
    {
        public static UserResource MapUserEntitytoResource(this User user)
        {
            return new UserResource
            {
                Username = user.Username,
            };
        }
    }
}