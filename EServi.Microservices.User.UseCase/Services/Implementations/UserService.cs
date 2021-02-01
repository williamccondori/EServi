using System;
using System.Threading.Tasks;
using EServi.Microservices.User.UseCase.Models;

namespace EServi.Microservices.User.UseCase.Services.Implementations
{
    public class UserService : IUserService
    {
        public async Task<UserInfo> GetInfoById(Guid id)
        {
            UserInfo userInfo = null;

            await Task.Run(() =>
            {
                userInfo = new UserInfo
                {
                    Id = new Guid(),
                    Name = "William"
                };
            });

            return userInfo;
        }

        public async Task<UserProfile> UpdateProfile(Guid id, UserProfile userProfile)
        {
            return userProfile;
        }
    }
}