using System.Collections.Generic;
using CoreLib.Mongo;
using MongoDB.Driver;

namespace CoreLib.Helpers.Descriptor
{
    public class SortByDescriptor<T> : CustomDescriptor<string> where T : Document
    {
        public  SortDefinition<T> SortDefinition { get; }

        public SortByDescriptor(string key, SortDefinition<T> sortDefinition) : base(key)
        {
            SortDefinition = sortDefinition;
        }
    }
}