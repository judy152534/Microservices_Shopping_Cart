using Chat.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Data
{
    public class ChatContextSeed
    {
        public static void SeedData(IMongoCollection<Message> messageCollection)
        {
            var existProduct = messageCollection.Find(p => true).Any();
            if (!existProduct)
            {
                messageCollection.InsertManyAsync(GetPreconfiguredMessages());
            }
        }
        public static void SeedData(IMongoCollection<Room> roomCollection)
        {
            var existProduct = roomCollection.Find(p => true).Any();
            if (!existProduct)
            {
                roomCollection.InsertManyAsync(GetPreconfiguredRooms());
            }
        }
        public static void SeedData(IMongoCollection<User> userCollection)
        {
            var existProduct = userCollection.Find(p => true).Any();
            if (!existProduct)
            {
                userCollection.InsertManyAsync(GetPreconfiguredUsers());
            }
        }

        private static IEnumerable<User> GetPreconfiguredUsers()
        {
            return new List<User>()
            {
                new User()
                {
                    Id = "1",
                    Name = "PIG",
                    Email = "pig@gmail.com",
                    CellPhone = "0987654321",
                    Photos = new Photo { PhotoId = "1", IsMain = true, Url = "https://fashionpig.com" },
                    Friends = new Friend { FriendUserId = "2"}
                },
                new User()
                {
                    Id = "2",
                    Name = "PeG",
                    Email = "peg@gmail.com",
                    CellPhone = "0987654321",
                    Photos = new Photo { PhotoId = "1", IsMain = true, Url = "https://fashionpeg.com" },
                    Friends = new Friend { FriendUserId = "1"}
                }
            };
        }

        private static IEnumerable<Room> GetPreconfiguredRooms()
        {
            return new List<Room>()
            {
                new Room()
                {
                    Id = "1",
                    RoomName = "Heee",
                    ManagerId = 2,
                    Members = new Member{ UserId =2,IsLocked = false},
                    photos = new Photo { PhotoId = "1", IsMain = true, Url = "https://peg.com" }

                }
            };
        }

        private static IEnumerable<Message> GetPreconfiguredMessages()
        {
            return new List<Message>()
            {
                new Message()
                {
                    Id ="1",
                    RoomId = 8888,
                    UserId = 5,
                    EventId = 1,
                    Content="su3safggg",
                    IsDeleted = false,
                    LastEditTime = 1625564903

                }
            };
        }
    }
}
