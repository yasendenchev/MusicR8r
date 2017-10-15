using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Contracts;
using MusicR8r.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services
{
    public class SongService : ISongService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Song> songRepository;
        private readonly IEfRepository<Album> albumRepository;
        private readonly IEfRepository<Artist> artistRepository;
        private readonly IEfRepository<Genre> genreRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;


        public SongService(IEfRepository<Song> songRepository, IEfRepository<Album> albumRepository, IEfRepository<Genre> genreRepository, IEfRepository<Artist> artistRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider)
        {
            if (songRepository == null)
            {
                throw new ArgumentNullException(String.Format("SongRepository" + argNullMessage));
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
            this.songRepository = songRepository;
            this.albumRepository = albumRepository;
            this.artistRepository = artistRepository;
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IQueryable<Song> GetAll()
        {
            IQueryable<Song> songs = this.songRepository.All;
            return songs;
        }

        public IQueryable<Song> GetAllAndDeleted()
        {
            IQueryable<Song> songs = this.songRepository.AllAndDeleted;
            return songs;
        }

        public void AddSong(string name, int duration, Guid genreId, Guid albumId)
        {
            var genre = this.genreRepository.GetById(genreId);
            var album = this.albumRepository.GetById(albumId);
            var artist = album.Artist;
            var song = new Song(name, duration, artist, album, genre);
            this.songRepository.Add(song);
            this.saveContext.Commit();
        }

        public Song GetById(Guid songId)
        {
            return this.songRepository.GetById(songId);
        }

        public IQueryable<Song> GetByAlbum(Guid albumId)
        {
            return this.songRepository.All.Where(song => song.Album.Id.Equals(albumId));
        }

        //Not sure if it will be put to use
        public void Update(Guid songId, Guid genreId, string songName, int songDuration)
        {
            var genre = this.genreRepository.GetById(genreId);
            var song = this.songRepository.GetById(songId);
            song.Name = songName;
            song.Duration = songDuration;
            song.Genre = genre;

            this.songRepository.Update(song);
            this.saveContext.Commit();
        }

        public void DeleteById(Guid songId)
        {
            var song = this.songRepository.GetById(songId);
            var dateDeleted = this.dateTimeProvider.Now();

            if (song != null)
            {
                song.IsDeleted = true;
                song.DeletedOn = this.dateTimeProvider.Now();

                this.songRepository.Update(song);
                this.saveContext.Commit();
            }
        }
    }
}
