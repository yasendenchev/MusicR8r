using MusicR8r.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services.Contracts
{
    public interface IUserService : IService
    {
        User GetUserById(Guid userId);

        void Update(Guid userId, string email, string username, string firstName, string lastName, string bio);
    }
}
