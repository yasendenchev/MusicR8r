using MusicR8r.Data.Model.Models;
using MusicR8r.Data.Repositories;
using MusicR8r.Data.SaveContext;
using MusicR8r.Services.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicR8r.Services
{
    public class ArtistService
    {
        private const string argNullMessage = "cannot be null.";
        private readonly IEfRepository<Artist> genreRepository;
        private readonly ISaveContext saveContext;
        private readonly IDateTimeProvider dateTimeProvider;


        public ArtistService(IEfRepository<Artist> genreRepository, ISaveContext saveContext, IDateTimeProvider dateTimeProvider)
        {
            if (genreRepository == null)
            {
                throw new ArgumentNullException(String.Format("GenreRepository" + argNullMessage));
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
            this.saveContext = saveContext;
            this.dateTimeProvider = dateTimeProvider;
        }
        //        add

        //update

        //delete(by id)

        //get all

        //get by id

    }
}
