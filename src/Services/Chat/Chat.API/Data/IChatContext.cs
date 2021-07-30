using Chat.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Data
{
    public interface IChatContext
    {
        IMongoCollection<User> Users { get; }
        IMongoCollection<Room> Rooms { get; }
        IMongoCollection<Message> Messages{ get; }
    }
}
