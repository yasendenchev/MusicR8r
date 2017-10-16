using MusicR8r.Contracts.Services;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.UnitOfWork;
using MusicR8r.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicR8r.Services
{
    public class AlbumService : IAlbumService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Album> albumRepository;
        private readonly IEfRepository<Artist> artistRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;

        public AlbumService(IEfRepository<Album> albumRepository, IEfRepository<Artist> artistRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            if (albumRepository == null)
            {
                throw new ArgumentNullException(String.Format("AlbumRepository" + argNullMessage));
            }

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

            this.albumRepository = albumRepository;
            this.artistRepository = artistRepository;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
        }

        //get all

        public IQueryable<Album> GetAll()
        {
            return this.albumRepository.All;
        }

        public void AddAlbum(string name, int year, Guid artistId)
        {
            var artist = this.artistRepository.GetById(artistId);
            var album = new Album(name, year, artist);
            this.albumRepository.Add(album);
            this.unitOfWork.Commit();
        }

        public Album GetById(Guid albumId)
        {
            return this.albumRepository.GetById(albumId);
        }

        public void Update(Guid albumId, string albumName, int albumYear)
        {
            var album = this.albumRepository.GetById(albumId);

            album.Name = albumName;
            album.Year = albumYear;

            this.albumRepository.Update(album);
            this.unitOfWork.Commit();
        }

        public void DeleteById(Guid albumId)
        {
            var album = this.albumRepository.GetById(albumId);
            var dateDeleted = this.dateTimeProvider.Now();

            if (album.Songs.Count > 0)
            {
                foreach (var song in album.Songs)
                {
                    song.IsDeleted = true;
                    song.DeletedOn = dateDeleted;
                }
            }

            if (album != null)
            {
                album.IsDeleted = true;
                album.DeletedOn = dateDeleted;

                this.albumRepository.Update(album);
                this.unitOfWork.Commit();
            }
        }

        public IQueryable<Album> GetByArtist(Guid artistId)
        {
            var albums = this.albumRepository.All.Where(album => album.Artist.Id.Equals(artistId));
            return albums;
        }

        public ICollection<Song> GetSongs(Guid albumId)
        {
            var album = this.albumRepository.GetById(albumId);

            return album.Songs;
        }
    }
}
