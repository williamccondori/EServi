using System.Linq;
using System.Threading.Tasks;
using EServi.Microservices.Auth.Domain.Entities;
using EServi.Microservices.Auth.Domain.Repositories;
using EServi.Microservices.Auth.Infrastructure.MongoDb.Contexts;
using MongoDB.Driver;

namespace EServi.Microservices.Auth.Infrastructure.MongoDb.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly MongoDbContext _context;

        public IdentityRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Identity> GetByEmail(string email)
        {
            var identities = await _context.Identity.FindAsync(x => x.IsActive && x.Email == email);

            return identities.Current.FirstOrDefault();
        }

        public async Task<Identity> Create(Identity identity)
        {
            await _context.Identity.InsertOneAsync(identity);

            return identity;
        }
    }
}