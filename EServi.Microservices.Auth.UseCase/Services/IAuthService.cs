using System.Threading.Tasks;
using EServi.Microservices.Auth.UseCase.Models;

namespace EServi.Microservices.Auth.UseCase.Services
{
    public interface IAuthService
    {
        Task Register(IdentityRegister authRegister);
        Task<AuthToken> Authenticate(Login login);
        Task<bool> Validate(string token);
    }
}