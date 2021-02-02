using System;
using EServi.Microservices.Auth.Domain.Entities.Base;

namespace EServi.Microservices.Auth.Domain.Entities
{
    public class Code : IDocument
    {
        public string Id { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string UserCreated { get; set; }
        public string UserUpdated { get; set; }
        public string UserId { get; set; }
        public CodeType Type { get; set; }
        public string Value { get; set; }
        public bool IsEnabled { get; set; }

        public static Code Create(CodeType codeType, string value, string userId)
        {
            return new Code
            {
                UserId = userId,
                Value = value,
                Type = codeType,
                IsEnabled = true,
                IsActive = true,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                UserCreated = userId,
                UserUpdated = userId
            };
        }

        public enum CodeType
        {
            ActivationCode
        }
    }
}