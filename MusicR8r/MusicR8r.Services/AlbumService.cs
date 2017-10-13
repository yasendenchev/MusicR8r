using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services.Providers
{
    public class AlbumService : IAlbumService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Album> albumRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;
        private readonly IArtistService artistService;



        public AlbumService(IEfRepository<Album> albumRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider, IArtistService artistService)
        {
            if (albumRepository == null)
            {
                throw new ArgumentNullException(String.Format("AlbumRepository" + argNullMessage));
            }

            if (saveContext == null)
            {
                throw new ArgumentNullException(String.Format("SaveContext" + argNullMessage));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(String.Format("DateTimeProvider" + argNullMessage));
            }

            if (artistService == null)
            {
                throw new ArgumentNullException(String.Format("ArtistService" + argNullMessage));
            }

            this.albumRepository = albumRepository;
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
            this.artistService = artistService;
        }

        //get all

        public IQueryable<Album> GetAll()
        {
            IQueryable<Album> albums = this.albumRepository.All;
            return albums;
        }

        //add

        public void Add(string name, int year, Guid albumId)
        {
            //new Album 
            //this.albumRepository.Add(album);
            this.saveContext.Commit();
        }

        //get by id

        public Album GetById(Guid albumId)
        {
            return this.albumRepository.GetById(albumId);
        }

        //update

        public void Update(Guid albumId, string albumName, int albumYear)
        {
            var album = this.albumRepository.GetById(albumId);

            album.Name = albumName;
            album.Year = albumYear;

            this.albumRepository.Update(album);
            this.saveContext.Commit();
        }

        //delete(by id)

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
                this.saveContext.Commit();
            }
        }

        //get by artist
        public IQueryable<Album> GetByArtist(Guid artistId)
        {
            return this.albumRepository.All.Where(album => album.Artist.Id.Equals(artistId));
        }

        //search(by albumName, by ARTIST, by genre)
        public void Search(string searchParameter)
        {
            //var albumsByGenre = this.albumRepository.All.Where(p => p.Songs.Any(s => s.Genres.Any(g => g.Name == parameter)));
            var albums = this.albumRepository.All
                .Where(album => album.Artist.Name.Equals(searchParameter));
        }
    }
}
