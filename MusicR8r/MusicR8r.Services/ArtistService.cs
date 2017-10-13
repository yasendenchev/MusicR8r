using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services
{
    public class ArtistService : IArtistService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Artist> artistRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;


        public ArtistService(IEfRepository<Artist> artistRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider)
        {
            if (artistRepository == null)
            {
                throw new ArgumentNullException(String.Format("ArtistRepository" + argNullMessage));
            }

            if (saveContext == null)
            {
                throw new ArgumentNullException(String.Format("SaveContext" + argNullMessage));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(String.Format("DateTimeProvider" + argNullMessage));
            }

            this.artistRepository = artistRepository;
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IQueryable<Artist> GetAll()
        {
            IQueryable<Artist> artists = this.artistRepository.All;
            return artists;
        }

        //public IQueryable<Artist> GetAllAndDeleted()
        //{
        //    IQueryable<Artist> artists = this.artistRepository.AllAndDeleted;
        //    return artists;
        //}

        public void Add(Artist artist)
        {
            this.artistRepository.Add(artist);
            this.saveContext.Commit();
        }

        public Artist GetById(Guid artistId)
        {
            return this.artistRepository.GetById(artistId);
        }

        public void Update(Guid artistId, string artistName, string artistCountry, string artistBio)
        {
            var artist = this.artistRepository.GetById(artistId);

            artist.Name = artistName;
            artist.CountryOfOrigin = artistCountry;
            artist.Bio = artistBio;

            this.artistRepository.Update(artist);
            this.saveContext.Commit();
        }

        public void DeleteById(Guid artistId)
        {
            var artist = this.artistRepository.GetById(artistId);
            var dateDeleted = this.dateTimeProvider.Now();

            if (artist != null)
            {
                artist.IsDeleted = true;
                artist.DeletedOn = this.dateTimeProvider.Now();

                this.artistRepository.Update(artist);
                this.saveContext.Commit();
            }
        }
    }
}
