using EServi.Microservices.Auth.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace EServi.Microservices.Auth.Infrastructure.MongoDb.Contexts
{
    public class MongoDbContext : MongoClient
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string host, string port, string user, string password, string databaseName)
        {
            var connectionString = $"mongodb://{user}:{password}@{host}:{port}";

            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(databaseName);

            BsonClassMap.RegisterClassMap<Identity>(cm =>
            {
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.MapMember(c => c.IsActive).SetElementName("isActive");
                cm.MapMember(c => c.CreatedAt).SetElementName("createdAt");
                cm.MapMember(c => c.UpdatedAt).SetElementName("updatedAt");
                cm.MapMember(c => c.UserCreated).SetElementName("userCreated");
                cm.MapMember(c => c.UserUpdated).SetElementName("userUpdated");

                cm.MapMember(c => c.UserId).SetElementName("userId");
                cm.MapMember(c => c.Email).SetElementName("email");
                cm.MapMember(c => c.Password).SetElementName("password");
                cm.MapMember(c => c.IsEnabled).SetElementName("isEnabled");
                cm.MapMember(c => c.SecretKey).SetElementName("secretKey");
            });

            BsonClassMap.RegisterClassMap<Code>(cm =>
            {
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.MapMember(c => c.IsActive).SetElementName("isActive");
                cm.MapMember(c => c.CreatedAt).SetElementName("createdAt");
                cm.MapMember(c => c.UpdatedAt).SetElementName("updatedAt");
                cm.MapMember(c => c.UserCreated).SetElementName("userCreated");
                cm.MapMember(c => c.UserUpdated).SetElementName("userUpdated");

                cm.MapMember(c => c.UserId).SetElementName("userId");
                cm.MapMember(c => c.Type).SetElementName("type");
                cm.MapMember(c => c.Value).SetElementName("value");
                cm.MapMember(c => c.IsEnabled).SetElementName("isEnabled");
            });
        }

        public IMongoCollection<Identity> Identity => _database.GetCollection<Identity>("identity");
        public IMongoCollection<Code> Code => _database.GetCollection<Code>("code");
    }
}