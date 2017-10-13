using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using System;
using System.Linq;

namespace MusicR8r.Services.Contracts
{
     public interface ISongService : IService
    {
          IQueryable<Song> GetAll();

          IQueryable<Song> GetAllAndDeleted();

          void Add(Song song);

          Song GetById(Guid songId);

          IQueryable<Song> GetByAlbum(Guid albumId);

          void Update(Guid songId, string songName, int songDuration, string songBio);

          void DeleteById(Guid songId);

    }
}