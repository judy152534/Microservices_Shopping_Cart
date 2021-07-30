using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]  
        public string Id { get; set; }

        [BsonElement("Name")] 
        public string Name { get; set; }
        public string Email { get; set; }
        public string CellPhone { get; set; }
        public Photo Photos { get; set; }
        public Friend Friends { get; set; }
    }

    public class Friend
    {
        public string FriendUserId { get; set; }

    }

    public class Photo
    {
        public string PhotoId { get; set; }
        public string Url { get; set; }
        public bool IsMain { get; set; }
    }
}
