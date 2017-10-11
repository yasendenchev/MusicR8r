using MusicR8r.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services.Contracts
{
    public interface IGenreService : IService
    {
        IQueryable<Genre> GetAllAndDeleted();

        void AddGenre(string name);

        Genre GetById(Guid genreId);

        void DeleteById(Guid genreId);
    }
}
