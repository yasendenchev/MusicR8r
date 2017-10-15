using AutoMapper;
using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicR8r.Areas.Admin.Models
{
    public class SongSubmitViewModel: IMapFrom<Song>, ICustomMappings
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required!")]
        public string Name { get; set; }

        public int Minutes { get; set; }

        public int Seconds { get; set; }

        public Guid GenreId { get; set; }

        public ICollection<GenreViewModel> Genres { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Song, SongSubmitViewModel>()
            .ForMember(songViewModel => songViewModel.Name, cfg => cfg.MapFrom(song => song.Name))
            .ForMember(songViewModel => songViewModel.GenreId, cfg => cfg.MapFrom(song => song.Genre.Id))
                .ForMember(songViewModel => songViewModel.Minutes, cfg => cfg.MapFrom(song => song.Duration / 60))
                .ForMember(songViewModel => songViewModel.Seconds, cfg => cfg.MapFrom(song => song.Duration % 60));
        }
    }
}