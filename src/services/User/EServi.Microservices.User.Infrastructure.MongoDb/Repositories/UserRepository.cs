using System.Linq;
using System.Threading.Tasks;
using EServi.Microservices.User.Domain.Repositories;
using EServi.Microservices.User.Infrastructure.MongoDb.Contexts;
using MongoDB.Driver;

namespace EServi.Microservices.User.Infrastructure.MongoDb.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;

        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Domain.Entities.User> GetByEmail(string email)
        {
            var task = await _context.User.FindAsync(x => x.IsActive && x.Email == email);
            var users = await task.ToListAsync();
            return users.FirstOrDefault();
        }

        public async Task<Domain.Entities.User> Create(Domain.Entities.User user)
        {
            await _context.User.InsertOneAsync(user);

            return user;
        }
    }
}