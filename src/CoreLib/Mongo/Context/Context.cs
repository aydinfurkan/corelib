using MongoDB.Driver;

namespace CoreLib.Mongo.Context
{
    public class Context : IContext
    {
        private readonly IMongoDatabase _mongoDatabase;

        public Context(MongoClient mongoClient, string databaseName)
        {
            _mongoDatabase = mongoClient.GetDatabase(databaseName);
        }

        public IMongoCollection<T> DbSet<T>(string collection) where T : Document
        {

            return _mongoDatabase.GetCollection<T>(collection);
        }
    }
}
