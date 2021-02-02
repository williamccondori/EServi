using System.Threading.Tasks;
using EServi.Microservices.Auth.Domain.Entities;

namespace EServi.Microservices.Auth.Domain.Repositories
{
    public interface ICodeRepository
    {
        Task<Code> Create(Code code);
        Task<Code> UpdateStatus(bool isActive);
    }
}