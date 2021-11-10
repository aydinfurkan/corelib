using CoreLib.Mongo;
using MongoDB.Driver;

namespace CoreLib.Helpers.Descriptor.Managers
{
    public class SortByDescriptorManager<T> : DescriptorManager<string> where T : Document
    {
        public readonly SortByDescriptor<T> CreatedAtAsc = new SortByDescriptor<T>("createdAtAsc", Builders<T>.Sort.Ascending(x => x.CreatedAt));
        public readonly SortByDescriptor<T> CreatedAtDesc = new SortByDescriptor<T>("createdAtDesc", Builders<T>.Sort.Descending(x => x.CreatedAt));
        
        
        protected override void AddAll()
        {
            _descriptors.Add(CreatedAtAsc);
            _descriptors.Add(CreatedAtDesc);
        }

        public SortDefinition<T> GetSortDefinition(string key)
        {
            return ((SortByDescriptor<T>) Find(key)).SortDefinition;
        }
    }
}