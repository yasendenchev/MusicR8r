using MusicR8r.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services.Providers;

namespace MusicR8r.Services
{
    public class UserService : IUserService
    {
        private readonly IEfRepository<User> userRepository;
        private readonly IEfRepository<Album> albumRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;

        public UserService(IEfRepository<User> userRepository, IEfRepository<Album> albumRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            if (userRepository == null)
            {
                throw new ArgumentNullException("User repository cannot be null.");
            }

            if (albumRepository == null)
            {
                throw new ArgumentNullException("User repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("Date provider cannot be null.");
            }

            this.userRepository = userRepository;
            this.albumRepository = albumRepository;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
        }
        public User GetUserById(Guid userId)
        {
            return this.userRepository.GetById(userId.ToString());
        }

        public void Update(Guid userId, string email, string username, string firstName, string lastName, string bio)
        {
            var user = this.userRepository.GetById(userId.ToString());

            if (user != null)
            {
                user.Email = email;
                user.UserName = username;
                user.FirstName = firstName;
                user.LastName = lastName;
                user.Bio = bio;

                this.userRepository.Update(user);
                this.unitOfWork.Commit();
            }
        }
    }
}
