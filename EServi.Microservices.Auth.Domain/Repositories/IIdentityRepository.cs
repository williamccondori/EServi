using System.Threading.Tasks;
using EServi.Microservices.Auth.Domain.Entities;

namespace EServi.Microservices.Auth.Domain.Repositories
{
    public interface IIdentityRepository
    {
        Task<Identity> GetByEmail(string email);
        Task<Identity> Create(Identity identity);
    }
}