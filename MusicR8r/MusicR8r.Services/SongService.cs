using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.UnitOfWork;
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
        private readonly IUnitOfWork unitOfWork;
        private readonly IDateTimeProvider dateTimeProvider;


        public SongService(IEfRepository<Song> songRepository, IEfRepository<Album> albumRepository, IEfRepository<Genre> genreRepository, IEfRepository<Artist> artistRepository, IUnitOfWork unitOfWork, IDateTimeProvider dateTimeProvider)
        {
            if (songRepository == null)
            {
                throw new ArgumentNullException(String.Format("SongRepository" + argNullMessage));
            }

            if (albumRepository == null)
            {
                throw new ArgumentNullException(String.Format("SongRepository" + argNullMessage));
            }

            if (genreRepository == null)
            {
                throw new ArgumentNullException(String.Format("SongRepository" + argNullMessage));
            }

            if (artistRepository == null)
            {
                throw new ArgumentNullException(String.Format("SongRepository" + argNullMessage));
            }

            if (unitOfWork == null)
            {
                throw new ArgumentNullException(String.Format("UnitOfWork" + argNullMessage));
            }

            if (dateTimeProvider == null)
            {
                throw new ArgumentNullException(String.Format("DateTimeProvider" + argNullMessage));
            }

            this.genreRepository = genreRepository;
            this.songRepository = songRepository;
            this.albumRepository = albumRepository;
            this.artistRepository = artistRepository;
            this.unitOfWork = unitOfWork;
            this.dateTimeProvider = dateTimeProvider;
        }

        public IQueryable<Song> GetAll()
        {
            IQueryable<Song> songs = this.songRepository.All;
            return songs;
        }

        public void AddSong(string name, int duration, Guid genreId, Guid albumId)
        {
            var genre = this.genreRepository.GetById(genreId);
            var album = this.albumRepository.GetById(albumId);
            var artist = album.Artist;
            var song = new Song(name, duration, artist, album, genre);
            this.songRepository.Add(song);
            this.unitOfWork.Commit();
        }

        public Song GetById(Guid songId)
        {
            var song = this.songRepository.GetById(songId);
            return song;
        }

        public IQueryable<Song> GetByAlbum(Guid albumId)
        {
            var songs = this.songRepository.All.Where(song => song.Album.Id.Equals(albumId));
            return songs;
        }
        
        public void Update(Guid songId, Guid genreId, string songName, int songDuration)
        {
            var genre = this.genreRepository.GetById(genreId);
            var song = this.songRepository.GetById(songId);
            song.Name = songName;
            song.Duration = songDuration;
            song.Genre = genre;

            this.songRepository.Update(song);
            this.unitOfWork.Commit();
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
                this.unitOfWork.Commit();
            }
        }
    }
}
