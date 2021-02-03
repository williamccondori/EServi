using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace EServi.Microservices.User.Infrastructure.MongoDb.Contexts
{
    public class MongoDbContext : MongoClient
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(string host, string port, string user, string password, string databaseName)
        {
            var connectionString = $"mongodb://{user}:{password}@{host}:{port}";

            var client = new MongoClient(connectionString);

            _database = client.GetDatabase(databaseName);

            BsonClassMap.RegisterClassMap<Domain.Entities.User>(cm =>
            {
                cm.MapIdProperty(c => c.Id)
                    .SetIdGenerator(StringObjectIdGenerator.Instance)
                    .SetSerializer(new StringSerializer(BsonType.ObjectId));

                cm.MapMember(c => c.IsActive).SetElementName("isActive");
                cm.MapMember(c => c.CreatedAt).SetElementName("createdAt");
                cm.MapMember(c => c.UpdatedAt).SetElementName("updatedAt");
                cm.MapMember(c => c.UserCreated).SetElementName("userCreated");
                cm.MapMember(c => c.UserUpdated).SetElementName("userUpdated");

                cm.MapMember(c => c.Name).SetElementName("name");
                cm.MapMember(c => c.LastName).SetElementName("lastName");
                cm.MapMember(c => c.Phone).SetElementName("phone");
                cm.MapMember(c => c.Email).SetElementName("email");
                cm.MapMember(c => c.Photo).SetElementName("photo");
                cm.MapMember(c => c.Description).SetElementName("description");
                cm.MapMember(c => c.Resume).SetElementName("resume");
            });
        }

        public IMongoCollection<Domain.Entities.User> User =>
            _database.GetCollection<Domain.Entities.User>("user");
    }
}