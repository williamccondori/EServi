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
using EServi.Shared.Helpers;

namespace EServi.Microservices.Auth.UseCase.Services
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

        public async Task Register(AuthRegister authRegister)
        {
            var identity = Identity.Create(authRegister.UserId, authRegister.Email,
                authRegister.Password, authRegister.PasswordKey);

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

            _emailPublisher.SendActivationCode(activationCodeEmail);
        }

        public async Task<AuthToken> Authenticate(Login login)
        {
            var identity = await _identityRepository.GetByEmail(login.Email);

            if (identity == null)
            {
                throw new ValidationException("Su correo electónico no está registrado en la plataforma");
            }

            if (!identity.IsEnabled)
            {
                throw new ValidationException("Se ha desabilitado su acceso a la plataforma");
            }

            var passwordEncrypted = Encryptor.GetHash(login.Password, identity.SecretKey);

            var isValid = identity.Password.Equals(passwordEncrypted);

            if (!isValid)
            {
                throw new ValidationException("Las credenciales brindadas son incorrectas");
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