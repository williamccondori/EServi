using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EServi.Microservices.Auth.Domain.Entities;
using EServi.Microservices.Auth.Domain.Repositories;
using EServi.Microservices.Auth.Infrastructure.Jwt.Builders;
using EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Models;
using EServi.Microservices.Auth.Infrastructure.RabbitMq.Publishers.Email.Publishers;
using EServi.Microservices.Auth.UseCase.Models;
using EServi.Shared.Encryptor;
using EServi.Shared.Helpers;

namespace EServi.Microservices.Auth.UseCase.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IJwtBuilder _jwtBuilder;
        private readonly IEmailPublisher _emailPublisher;
        private readonly ICodeRepository _codeRepository;
        private readonly IIdentityRepository _identityRepository;

        public AuthService(IJwtBuilder jwtBuilder, IEmailPublisher emailPublisher, ICodeRepository codeRepository,
            IIdentityRepository identityRepository)
        {
            _jwtBuilder = jwtBuilder;
            _emailPublisher = emailPublisher;
            _codeRepository = codeRepository;
            _identityRepository = identityRepository;
        }

        public async Task Register(IdentityRegister authRegister)
        {
            var existingIdentity = await _identityRepository.GetByEmail(authRegister.Email);

            if (existingIdentity != null)
            {
                throw new ValidationException();
            }

            var secretKey = Encryptor.GetSecretKey();

            var passwordEncrypted = Encryptor.GetHash(authRegister.Password, secretKey);

            var identity = Identity.Create(authRegister.UserId, authRegister.Email, passwordEncrypted, secretKey);

            await _identityRepository.Create(identity);

            var activationCode = ActivationCode.Generate();

            var code = Code.Create(Code.CodeType.ActivationCode, activationCode, identity.Id);

            await _codeRepository.Create(code);

            var activationCodeEmail = new ActivationCodeEmail
            {
                Email = identity.Email,
                ActivationCode = code.Value,
                FullName = authRegister.FullName
            };

            _emailPublisher.SendActivationCodeEmail(activationCodeEmail);
        }

        public async Task<AuthToken> Authenticate(Login login)
        {
            var identity = await _identityRepository.GetByEmail(login.Email);

            if (identity == null)
            {
                throw new ValidationException();
            }

            if (!identity.IsEnabled)
            {
                throw new ValidationException();
            }

            var passwordEncrypted = Encryptor.GetHash(login.Password, identity.SecretKey);

            var isValid = identity.Password.Equals(passwordEncrypted);

            if (!isValid)
            {
                throw new ValidationException();
            }

            var tokenContent = new Dictionary<string, string> {{"userId", identity.UserId}};

            var token = _jwtBuilder.GenerateToken(tokenContent);

            return new AuthToken
            {
                Token = token
            };
        }

        public Task<bool> Validate(string token)
        {
            throw new NotImplementedException();
        }
    }
}