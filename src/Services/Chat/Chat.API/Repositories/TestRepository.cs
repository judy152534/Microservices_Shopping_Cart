using Chat.API.Data;
using Chat.API.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Repositories
{
    public class TestRepository : ITestRepository
    {

        private readonly IChatContext _chatContext;
        public TestRepository(IChatContext chatContext)
        {
            _chatContext = chatContext;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _chatContext.Users.Find(p => true).ToListAsync();
        }
    }
}
