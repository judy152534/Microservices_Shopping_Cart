using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Entities
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public int RoomId { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public string Content { get; set; }
        public bool IsDeleted { get; set; }
        public long LastEditTime { get; set; }

    }
}
