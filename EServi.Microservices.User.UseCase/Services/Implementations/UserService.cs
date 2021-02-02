using System;
using System.Threading.Tasks;
using EServi.Microservices.User.UseCase.Models;

namespace EServi.Microservices.User.UseCase.Services.Implementations
{
    public class UserService : IUserService
    {
        public Task<UserInfo> GetInfoById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<UserProfile> UpdateProfile(string id, UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public Task<UserRegister> Register(UserRegister userRegister)
        {throw new NotImplementedException();
        }
    }
}