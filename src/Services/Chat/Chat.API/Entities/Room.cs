using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Entities
{
    public class Room
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string RoomName { get; set; }
        public int ManagerId { get; set; }
        public Member Members { get; set; }
        public Photo photos { get; set; }

    }

    public class Member
    {
        public int UserId { get; set; }
        public bool IsLocked { get; set; }

    }
}
