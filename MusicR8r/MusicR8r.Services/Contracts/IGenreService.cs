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
        void Add(Genre genre);

        IQueryable<Genre> GetAll();

        IQueryable<Genre> GetAllAndDeleted();
        
        Genre GetById(Guid genreId);

        void DeleteById(Guid genreId);
    }
}
