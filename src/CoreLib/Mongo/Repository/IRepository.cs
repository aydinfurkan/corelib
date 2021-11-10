using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace CoreLib.Mongo.Repository
{
    public interface IRepository<T> where T : class
    {
        List<T> FindAll();
        Task<List<T>> FindAllAsync();
        T FindOne(Expression<Func<T, bool>> expression);
        Task<T> FindOneAsync(Expression<Func<T, bool>> expression);
        List<T> FindMany(Expression<Func<T, bool>> expression);
        Task<List<T>> FindManyAsync(Expression<Func<T, bool>> expression);
        Task<Tuple<List<T>, long>> FindManyAsync(Expression<Func<T, bool>> expression, int from, int size);
        Task<Tuple<List<T>, long>> FindManyAsync(Expression<Func<T, bool>> expression, int from, int size, SortDefinition<T> sortDefinition);
        object InsertOne(T document);
        Task<object> InsertOneAsync(T document);
        List<object> InsertMany(List<T> documents);
        Task<List<object>> InsertManyAsync(List<T> documents);
        long UpdateOne(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties);
        Task<long> UpdateOneAsync(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties);
        long UpdateMany(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties);
        Task<long> UpdateManyAsync(Expression<Func<T, bool>> expression,
            params (Expression<Func<T, object>>, object)[] updatedProperties);
        long ReplaceOne(Expression<Func<T, bool>> expression, T document);
        Task<long> ReplaceOneAsync(Expression<Func<T, bool>> expression, T document);
        long ReplaceMany(Expression<Func<T, bool>> expression, List<T> documents);
        Task<long> ReplaceManyAsync(Expression<Func<T, bool>> expression, List<T> documents);
        long DeleteOne(Expression<Func<T, bool>> expression);
        Task<long> DeleteOneAsync(Expression<Func<T, bool>> expression);
        long DeleteMany(Expression<Func<T, bool>> expression);
        Task<long> DeleteManyAsync(Expression<Func<T, bool>> expression);
    }
}
