using System;
using System.Threading.Tasks;
using EServi.Microservices.User.UseCase.Models;

namespace EServi.Microservices.User.UseCase.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetInfoById(Guid id);

        Task<UserProfile> UpdateProfile(Guid id, UserProfile userProfile);
    }
}