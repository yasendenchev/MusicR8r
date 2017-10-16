using MusicR8r.Contracts.Services;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services.Providers;
using System;
using System.Linq;

namespace MusicR8r.Services
{
    public class ArtistService : IArtistService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Artist> artistRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IAlbumService albumService;


        public ArtistService(IEfRepository<Artist> artistRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider, IAlbumService albumService)
        {
            if (artistRepository == null)
            {
                throw new ArgumentNullException(String.Format("ArtistRepository" + argNullMessage));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(String.Format("UnitOfWork" + argNullMessage));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(String.Format("DateTimeProvider" + argNullMessage));
            }

            if (albumService == null)
            {
                throw new ArgumentNullException(String.Format("AlbumService" + argNullMessage));
            }

            this.artistRepository = artistRepository;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
            this.albumService = albumService;
        }

        public IQueryable<Artist> GetAll()
        {
            IQueryable<Artist> artists = this.artistRepository.All;
            return artists;
        }

        public void AddArtist(string name, string countryOfOrigin, string bio)
        {
            var artist = new Artist(name, countryOfOrigin, bio);
            this.artistRepository.Add(artist);
            this.unitOfWork.Commit();
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
            this.unitOfWork.Commit();
        }

        public void DeleteById(Guid artistId)
        {
            var artist = this.artistRepository.GetById(artistId);
            var dateDeleted = this.dateTimeProvider.Now();

            if (artist.Albums.Count > 0)
            {
                foreach (var album in artist.Albums)
                {
                    albumService.DeleteById(album.Id);
                }
            }

            if (artist != null)
            {
                artist.IsDeleted = true;
                artist.DeletedOn = this.dateTimeProvider.Now();

                this.artistRepository.Update(artist);
                this.unitOfWork.Commit();
            }
        }
    }
}
