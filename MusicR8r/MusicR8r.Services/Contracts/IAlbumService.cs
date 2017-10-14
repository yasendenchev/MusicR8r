using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using System;
using System.Linq;

namespace MusicR8r.Services.Providers
{
    public interface IAlbumService : IService
    {
        IQueryable<Album> GetAll();

        void AddAlbum(string name, int year, Guid artistId);

        Album GetById(Guid albumId);

        void Update(Guid albumId, string albumName, int albumYear);

        void DeleteById(Guid albumId);

        IQueryable<Album> GetByArtist(Guid artistId);
    }
}