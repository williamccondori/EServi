using System.Collections.Generic;

namespace EServi.Microservices.Auth.Infrastructure.Jwt.Builders
{
    public interface IJwtBuilder
    {
        string GenerateToken(Dictionary<string, string> values);
        string ValidateAuthToken(string token);
    }
}