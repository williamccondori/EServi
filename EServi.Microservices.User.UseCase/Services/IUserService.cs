using System.Threading.Tasks;
using EServi.Microservices.User.UseCase.Models;

namespace EServi.Microservices.User.UseCase.Services
{
    public interface IUserService
    {
        Task<UserInfo> GetInfoById(string id);
        Task<UserProfile> UpdateProfile(string id, UserProfile userProfile);
        Task<UserRegister> Register(UserRegister userRegister);
    }
}