using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicR8r.Data;
using MusicR8r.Data.Model;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Contracts;
using MusicR8r.Services.Providers;

namespace MusicR8r.Services
{
    public class GenreService : IGenreService
    {
        private const string nullRefMessage = "cannot be null.";
        private readonly IEfRepository<Genre> genreRepository;
        private readonly ISaveContext unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;


        public GenreService(IEfRepository<Genre> genreRepository, ISaveContext unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            if (genreRepository == null)
            {
                throw new ArgumentNullException("Genre repository cannot be null.");
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException("Unit of work cannot be null.");
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException("Provider cannot be null.");
            }

            this.genreRepository = genreRepository;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IQueryable<Genre> GetAll()
        {
            IQueryable<Genre> genres = this.genreRepository.All;
            return genres;
        }

        public IQueryable<Genre> GetAllAndDeleted()
        {
            IQueryable<Genre> genres = this.genreRepository.AllAndDeleted;
            return genres;
        }

        public void AddGenre(string name)
        {
            this.genreRepository.Add(new Genre(name));
            this.unitOfWork.Commit();
        }

        public Genre GetById(Guid genreId)
        {
            return this.genreRepository.GetById(genreId);
        }

        public void DeleteById(Guid genreId)
        {
            var genre = this.genreRepository.GetById(genreId);
            var dateDeleted = this.dateTimeProvider.Now();

            if (genre != null)
            {
                genre.IsDeleted = true;
                genre.DeletedOn = this.dateTimeProvider.Now();

                this.genreRepository.Update(genre);
                this.unitOfWork.Commit();
            }
        }
    }
}
