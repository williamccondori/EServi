using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EServi.Microservices.Auth.UseCase.Models;

namespace EServi.Microservices.Auth.UseCase.Services.Implementations
{
    public class AuthService : IAuthService
    {
        public async Task<AuthToken> Authenticate(Login login)
        {
            if (login.Email == "williamccondori@gmail.com")
                throw new ValidationException("User not found");

            AuthToken authToken = null;

            await Task.Run(() =>
            {
                authToken = new AuthToken()
                {
                    Token = "xsxsxs"
                };
            });

            return authToken;
        }
    }
}