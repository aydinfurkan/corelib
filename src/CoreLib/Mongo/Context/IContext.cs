using MongoDB.Driver;

namespace CoreLib.Mongo.Context
{
    public interface IContext
    {
        IMongoCollection<T> DbSet<T>(string collection) where T : Document;
    }
}
