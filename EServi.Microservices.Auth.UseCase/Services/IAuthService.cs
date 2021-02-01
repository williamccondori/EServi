using System.Threading.Tasks;
using EServi.Microservices.Auth.UseCase.Models;

namespace EServi.Microservices.Auth.UseCase.Services
{
    public interface IAuthService
    {
        Task<AuthToken> Authenticate(Login login);
    }
}