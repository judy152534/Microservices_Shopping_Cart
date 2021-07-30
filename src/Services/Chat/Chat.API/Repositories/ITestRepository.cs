using Chat.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.API.Repositories
{
    public interface ITestRepository
    {
        Task<IEnumerable<User>> GetUsers();
        
    }
}
