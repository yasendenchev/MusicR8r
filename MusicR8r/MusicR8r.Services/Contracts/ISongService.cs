using MusicR8r.Data.Model.Models;
using MusicR8r.Services.Contracts;
using System;
using System.Linq;

namespace MusicR8r.Services.Contracts
{
     public interface ISongService : IService
    {
          IQueryable<Song> GetAll();

          void AddSong(string name, int duration, Guid genreId, Guid albumId);

          Song GetById(Guid songId);

          IQueryable<Song> GetByAlbum(Guid albumId);

          void Update(Guid songId, Guid genreId, string songName, int songDuration);

          void DeleteById(Guid songId);

    }
}