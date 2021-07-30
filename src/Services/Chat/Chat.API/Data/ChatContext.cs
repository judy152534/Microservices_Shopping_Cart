using Chat.API.Entities;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Data
{
    public class ChatContext : IChatContext
    {
        public ChatContext(IConfiguration configuraton)
        {
            var client = new MongoClient(configuraton.GetValue<string>("DatabaseSettings:ConnectionString"));
            var databse = client.GetDatabase(configuraton.GetValue<string>("DatabaseSettings:DatabaseName"));
            Users = databse.GetCollection<User>("DatabaseSettings:UsersCollectionName");
            Rooms = databse.GetCollection<Room>("DatabaseSettings:RoomsCollectionName");
            Messages = databse.GetCollection<Message>("DatabaseSettings:MessagesCollectionName");
            ChatContextSeed.SeedData(Users);
            ChatContextSeed.SeedData(Rooms);
            ChatContextSeed.SeedData(Messages);
        }

        public IMongoCollection<User> Users { get; }
        public IMongoCollection<Room> Rooms { get; }
        public IMongoCollection<Message> Messages { get; }
    }
}
