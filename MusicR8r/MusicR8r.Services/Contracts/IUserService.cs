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

        User GetByUsername(string username);

        IQueryable<User> GetAll();

        void Update(Guid userId, string email, string username, string firstName, string lastName, string bio);

        //void DeleteById(Guid userId);

        //void RestoreUser(Guid userId);

        //IQueryable<User> GetAllAndDeleted();

        IQueryable<Album> GetUserAlbums(Guid userId);

        void DeleteAlbum(Guid userId, Guid albumId);

        void AddAlbum(Guid userId, Guid albumId);
    }
}
