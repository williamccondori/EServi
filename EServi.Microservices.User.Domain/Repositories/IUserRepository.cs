using System.Threading.Tasks;

namespace EServi.Microservices.User.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<Entities.User> GetByEmail(string email);
        Task<Entities.User> Create(Entities.User user);
    }
}