using MusicR8r.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Providers;

namespace MusicR8r.Services
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> userRepository;
        private readonly IEfRepository<Album> albumRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;

        public UserService(IEfRepository<User> userRepository, IEfRepository<Album> albumRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("User repository cannot be null.");
            }

            if (albumRepository == null)
            {
                throw new ArgumentNullException("User repository cannot be null.");
            }

            if (saveContext == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("Date provider cannot be null.");
            }

            this.userRepository = userRepository;
            this.albumRepository = albumRepository;
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        public void AddAlbum(Guid userId, Guid albumId)
        {
            var user = this.userRepository.GetById(userId);
            var album = this.albumRepository.GetById(albumId);

            if (!user.Albums.Contains(album))
            {
                user.Albums.Add(album);
            }
            else
            {
                throw new InvalidOperationException("Album is already added");
            }
        }

        public void DeleteAlbum(Guid userId, Guid albumId)
        {
            var user = this.userRepository.GetById(userId);
            var album = this.albumRepository.GetById(albumId);

            user.Albums.Remove(album);
        }

        public void DeleteById(Guid userId)
        {
            var user = this.userRepository.GetById(userId);
            var dateDeleted = dateTimeProvider.Now();

            if (user != null)
            {
                user.IsDeleted = true;
                user.DeletedOn = dateDeleted;

                this.userRepository.Update(user);
                this.saveContext.Commit();
            }
        }

        public IQueryable<User> GetAll()
        {
            return this.userRepository.All;
        }

        public IQueryable<User> GetAllAndDeleted()
        {
            return this.userRepository.AllAndDeleted;
        }

        public User GetByUsername(string username)
        {
            return this.userRepository.All.FirstOrDefault(x => x.UserName.Equals(username));
        }

        public IQueryable<Album> GetUserAlbums(Guid userId)
        {
            return this.userRepository.GetById(userId).Albums.AsQueryable();
        }

        public User GetUserById(Guid userId)
        {
            return this.userRepository.GetById(userId.ToString());
        }

        public void RestoreUser(Guid userId)
        {
            var user = this.userRepository.GetById(userId);

            if (user != null)
            {
                user.IsDeleted = false;
                user.DeletedOn = null;

                this.userRepository.Update(user);
                this.saveContext.Commit();
            }
        }

        public void Update(Guid userId, string email, string username, string firstName, string lastName, string bio)
        {
            var user = this.userRepository.GetById(userId);

            if (user != null)
            {
                user.Email = email;
                user.UserName = username;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Bio = bio;

                this.userRepository.Update(user);
                this.saveContext.Commit();
            }
        }
    }
}
