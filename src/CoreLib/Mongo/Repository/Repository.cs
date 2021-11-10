using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CoreLib.Mongo.Context;
using MongoDB.Driver;

namespace CoreLib.Mongo.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Document
    {
        private readonly IMongoCollection<T> _collection;

        protected Repository(IContext context, string collectionName = "")
        {
            if (string.IsNullOrEmpty(collectionName))
            {
                collectionName = typeof(T).Name;
            }
            collectionName = collectionName.First().ToString().ToLower() + collectionName.Substring(1);
            _collection = context.DbSet<T>(collectionName);
        }

        public List<T> FindAll()
        {
            return _collection.AsQueryable().ToList();
        }

        public Task<List<T>> FindAllAsync()
        {
            return Task.FromResult(FindAll());
        }

        public T FindOne(Expression<Func<T, bool>> expression)
        {
            return _collection.AsQueryable().FirstOrDefault(expression);
        }

        public Task<T> FindOneAsync(Expression<Func<T, bool>> expression)
        {
            return Task.FromResult(FindOne(expression));
        }
        
        public List<T> FindMany(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).ToList();
        }

        public async Task<List<T>> FindManyAsync(Expression<Func<T, bool>> expression)
        {
            var documents = await _collection.FindAsync(expression);
            return documents.ToList();
        }
        
        public async Task<Tuple<List<T>,long>> FindManyAsync(Expression<Func<T, bool>> expression, int from, int size)
        {
            var query = _collection.Find(expression);
            var totalItemCount = await query.CountDocumentsAsync();
            var currentItems = await query.Skip(from).Limit(size).ToListAsync();
            return new Tuple<List<T>, long>(currentItems, totalItemCount);
        }
        
        public async Task<Tuple<List<T>,long>> FindManyAsync(Expression<Func<T, bool>> expression, int from, int size, SortDefinition<T> sortDefinition)
        {
            var query = _collection.Find(expression).Sort(sortDefinition);
            var totalItemCount = await query.CountDocumentsAsync();
            var currentItems = await query.Skip(from).Limit(size).ToListAsync();
            return new Tuple<List<T>, long>(currentItems, totalItemCount);
        }

        public object InsertOne(T document)
        {
            if (document.Id == null)
            {
                document.Id = Guid.NewGuid();
            }
            document.CreatedAt = DateTime.Now;
            document.UpdatedAt = DateTime.Now;
            _collection.InsertOne(document);
            return document.Id;
        }

        public virtual async Task<object> InsertOneAsync(T document)
        {
            if (document.Id == null)
            {
                document.Id = Guid.NewGuid();
            }
            document.CreatedAt = DateTime.Now;
            document.UpdatedAt = DateTime.Now;
            await _collection.InsertOneAsync(document);
            return document.Id;
        }

        public List<object> InsertMany(List<T> documents)
        {
            foreach (var document in documents)
            {
                if (document.Id == null)
                {
                    document.Id = Guid.NewGuid();
                }

                document.CreatedAt = DateTime.Now;
                document.UpdatedAt = DateTime.Now;
            }

            try
            {
                _collection.InsertMany(documents, new InsertManyOptions { IsOrdered = false});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return documents.Select(d => d.Id).ToList();
        }

        public async Task<List<object>> InsertManyAsync(List<T> documents)
        {
            foreach (var document in documents)
            {
                if (document.Id == null)
                {
                    document.Id = Guid.NewGuid();
                }
                document.CreatedAt = DateTime.Now;
                document.UpdatedAt = DateTime.Now;
            }

            try
            {
                await _collection.InsertManyAsync(documents, new InsertManyOptions { IsOrdered = false});
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return documents.Select(d => d.Id).ToList();
        }

        public long UpdateOne(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties)
        {
            var update = Builders<T>.Update.Set(b => b.UpdatedAt, DateTime.Now);
            foreach (var (key, value) in updatedProperties)
            {
                update = update.Set(key, value);
            }
            var result = _collection.UpdateOne(expression, update);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateOneAsync(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties)
        {
            var update = Builders<T>.Update.Set(b => b.UpdatedAt, DateTime.Now);
            foreach (var (key, value) in updatedProperties)
            {
                update = update.Set(key, value);
            }
            var result = await _collection.UpdateOneAsync(expression, update);
            return result.ModifiedCount;
        }

        public long UpdateMany(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties)
        {
            var update = Builders<T>.Update.Set(b => b.UpdatedAt, DateTime.Now);
            foreach (var (key, value) in updatedProperties)
            {
                update = update.Set(key, value);
            }
            var result = _collection.UpdateMany(expression, update);
            return result.ModifiedCount;
        }

        public async Task<long> UpdateManyAsync(Expression<Func<T, bool>> expression, params (Expression<Func<T, object>>, object)[] updatedProperties)
        {
            var update = Builders<T>.Update.Set(b => b.UpdatedAt, DateTime.Now);
            foreach (var (key, value) in updatedProperties)
            {
                update = update.Set(key, value);
            }
            var result = await _collection.UpdateManyAsync(expression, update);
            return result.ModifiedCount;
        }

        public long ReplaceOne(Expression<Func<T, bool>> expression, T document)
        {
            document.UpdatedAt = DateTime.Now;
            var result = _collection.ReplaceOne(expression, document);
            return result.ModifiedCount;
        }
        
        public async Task<long> ReplaceOneAsync(Expression<Func<T, bool>> expression, T document)
        {
            document.UpdatedAt = DateTime.Now;
            var result = await _collection.ReplaceOneAsync(expression, document);
            return result.ModifiedCount;
        }
        
        public long ReplaceMany(Expression<Func<T, bool>> expression, List<T> documents)
        {
            var updates = new List<WriteModel<T>>();
            foreach (var doc in documents)
            {
                doc.UpdatedAt = DateTime.Now;
                updates.Add(new ReplaceOneModel<T>(expression, doc));
            }
            var result = _collection.BulkWrite(updates, new BulkWriteOptions() { IsOrdered = false });
            return result.ModifiedCount;
        }
        
        public async Task<long> ReplaceManyAsync(Expression<Func<T, bool>> expression, List<T> documents)
        {
            var updates = new List<WriteModel<T>>();
            foreach (var doc in documents)
            {
                doc.UpdatedAt = DateTime.Now;
                updates.Add(new ReplaceOneModel<T>(expression, doc));
            }
            var result = await _collection.BulkWriteAsync(updates, new BulkWriteOptions() { IsOrdered = false });
            return result.ModifiedCount;
        }

        public long DeleteOne(Expression<Func<T, bool>> expression)
        {
            var result = _collection.DeleteOne(expression);
            return result.DeletedCount;
        }

        public async Task<long> DeleteOneAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _collection.DeleteOneAsync(expression);
            return result.DeletedCount;
        }

        public long DeleteMany(Expression<Func<T, bool>> expression)
        {
            var result = _collection.DeleteMany(expression);
            return result.DeletedCount;
        }

        public async Task<long> DeleteManyAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _collection.DeleteManyAsync(expression);
            return result.DeletedCount;
        }
    }
}
