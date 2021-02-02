using System.Threading.Tasks;
using EServi.Microservices.Auth.Domain.Entities;
using EServi.Microservices.Auth.Domain.Repositories;
using EServi.Microservices.Auth.Infrastructure.MongoDb.Contexts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EServi.Microservices.Auth.Infrastructure.MongoDb.Repositories
{
    public class CodeRepository : ICodeRepository
    {
        private readonly MongoDbContext _context;

        public CodeRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<Code> Create(Code code)
        {
            await _context.Code.InsertOneAsync(code);

            return code;
        }

        public Task<Code> UpdateStatus(bool isActive)
        {
            throw new System.NotImplementedException();
        }
    }
}