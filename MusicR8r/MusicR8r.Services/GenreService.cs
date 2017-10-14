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
using System.Data.Entity;

namespace MusicR8r.Services
{
    public class GenreService : IGenreService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Genre> genreRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;


        public GenreService(IEfRepository<Genre> genreRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider)
        {
            if (genreRepository == null)
            {
                throw new ArgumentNullException(String.Format("GenreRepository" + argNullMessage));
            }

            if (saveContext == null)
            {
                throw new ArgumentNullException(String.Format("SaveContext" + argNullMessage));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(String.Format("DateTimeProvider" + argNullMessage));
            }

            this.genreRepository = genreRepository;
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IQueryable<Genre> GetAll()
        {
            IQueryable<Genre> genres = this.genreRepository.All;
            return genres;
        }

        //public IQueryable<Genre> GetAllAndDeleted()
        //{
        //    IQueryable<Genre> genres = this.genreRepository.AllAndDeleted;
        //    return genres;
        //}

        public void AddGenre(string name)
        {
            var genre = new Genre(name);
            this.genreRepository.Add(genre);
            this.saveContext.Commit();
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
                this.saveContext.Commit();
            }
        }
    }
}
