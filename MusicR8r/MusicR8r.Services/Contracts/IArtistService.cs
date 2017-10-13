using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using System;
using System.Linq;

namespace MusicR8r.Services
{
    public interface IArtistService : IService
    {
        void Add(Artist artist);

        IQueryable<Artist> GetAll();

        IQueryable<Artist> GetAllAndDeleted();

        Artist GetById(Guid artistId);

        void Update(Guid artistId, string artistName, string artistCountry, string artistBio);

        void DeleteById(Guid artistId);
    }
}