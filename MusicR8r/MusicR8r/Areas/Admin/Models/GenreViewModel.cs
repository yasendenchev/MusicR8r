using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicR8r.Areas.Admin.Models
{
    public class GenreViewModel : IMapFrom<Genre>, ICustomMappings
    {
        public string Id { get; set; }

        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*${3,15}", ErrorMessage = "The name can contain only letters, digits and whitespaces")]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public int SongsCount { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Genre, GenreViewModel>()
                .ForMember(genreViewModel => genreViewModel.Name, cfg => cfg.MapFrom(genre => genre.Name))
                .ForMember(genreViewModel => genreViewModel.Id, cfg => cfg.MapFrom(genre => genre.Id))
                .ForMember(genreViewModel => genreViewModel.SongsCount, cfg => cfg.MapFrom(genre => genre.Songs.Count()));
        }
    }
}