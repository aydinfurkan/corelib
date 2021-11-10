using System;
using MongoDB.Bson.Serialization.Attributes;

namespace CoreLib.Mongo
{
    public abstract class Document
    {
        public object Id { get; set; }
        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
