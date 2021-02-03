using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EServi.Microservices.User.Domain.Repositories;
using EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Models;
using EServi.Microservices.User.Infrastructure.RabbitMq.Publishers.Auth.Publishers;
using EServi.Microservices.User.UseCase.Models;
using EServi.Shared.Helpers;

namespace EServi.Microservices.User.UseCase.Services
{
    public class UserService : IUserService
    {
        private readonly IAuthPublisher _authPublisher;
        private readonly IUserRepository _userRepository;

        public UserService(IAuthPublisher authPublisher, IUserRepository userRepository)
        {
            _authPublisher = authPublisher;
            _userRepository = userRepository;
        }

        public async Task<UserInfo> GetInfoById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<UserProfile> UpdateProfile(string id, UserProfile userProfile)
        {
            throw new NotImplementedException();
        }

        public async Task<UserRegister> Register(UserRegister userRegister)
        {
            var existingUser = await _userRepository.GetByEmail(userRegister.Email);

            if (existingUser != null)
            {
                throw new ValidationException("Ya se ha registrado con usuario con el correo electrónico");
            }

            var user = Domain.Entities.User.Create(userRegister.Name, userRegister.LastName, userRegister.Phone,
                userRegister.Email);

            var secretKey = Encryptor.GetSecretKey();
            var passwordEncrypted = Encryptor.GetHash(userRegister.Password, secretKey);

            await _userRepository.Create(user);

            var identityRegister = new AuthRegister
            {
                UserId = user.Id,
                Email = user.Email,
                Password = (passwordEncrypted, secretKey),
                FullName = $"{user.Name} {user.LastName}",
            };

            _authPublisher.RegisterAuth(identityRegister);

            return new UserRegister
            {
                Name = user.Name,
                Email = user.Email,
                LastName = user.LastName
            };
        }
    }
}