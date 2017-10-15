﻿using MusicR8r.Contracts.Services;
using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Providers;
using System;
using System.Linq;

namespace MusicR8r.Services
{
    public class AlbumService : IAlbumService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Album> albumRepository;
        private readonly IEfRepository<Artist> artistRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;

        public AlbumService(IEfRepository<Album> albumRepository, IEfRepository<Artist> artistRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider)
        {
            if (albumRepository == null)
            {
                throw new ArgumentNullException(String.Format("AlbumRepository" + argNullMessage));
            }

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

            this.albumRepository = albumRepository;
            this.artistRepository = artistRepository;
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
        }

        //get all

        public IQueryable<Album> GetAll()
        {
            IQueryable<Album> albums = this.albumRepository.All;
            return albums;
        }

        //add

        public void AddAlbum(string name, int year, Guid artistId)
        {
            //
            //
            //
            //
            var artist = this.artistRepository.GetById(artistId);
            var album = new Album(name, year, artist);
            this.albumRepository.Add(album);
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
        public IQueryable<Album> Search(string searchParameter)
        {
            //var albumsByGenre = this.albumRepository.All.Where(p => p.Songs.Any(s => s.Genres.Any(g => g.Name == parameter)));
            //var albums = this.albumRepository.All
            //    .Where(album =>
            //        album.Artist.Name.Contains(searchParameter))
            //    .Where(album =>
            //        album.Songs.Any(song =>
            //            song.Genres.Any(genre =>
            //                genre.Name.Contains(searchParameter))))
            //    .Where(album =>
            //        album.Name.Contains(searchParameter));

            return null;
        }
    }
}