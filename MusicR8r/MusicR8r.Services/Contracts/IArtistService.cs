using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using System;
using System.Linq;

namespace MusicR8r.Contracts.Services
{
    public interface IArtistService : IService
    {
        void AddArtist(string name, string countryOfOrigin, string bio);

        IQueryable<Artist> GetAll();

        Artist GetById(Guid artistId);

        void Update(Guid artistId, string artistName, string artistCountry, string artistBio);

        void DeleteById(Guid artistId);
    }
}