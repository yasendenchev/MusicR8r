using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TelerikAcademy.ForumSystem.Web.Infrastructure;
using AutoMapper;

namespace MusicR8r.Models
{
    public class GenreViewModel : IMapFrom<Genre>, ICustomMappings
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Genre, GenreViewModel>()
                .ForMember(genreViewModel => genreViewModel.Name, cfg => cfg.MapFrom(genre => genre.Name))
                .ForMember(genreViewModel => genreViewModel.Id, cfg => cfg.MapFrom(genre => genre.Id));

        }
    }
}