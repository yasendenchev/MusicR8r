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
    public class SongViewModel : IMapFrom<Song>, ICustomMappings
    {
        public Guid Id { get; set; }

        // TODO: remove

        [Required(ErrorMessage = "Name is required!")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*${3,15}", ErrorMessage = "Song name should contain only letters")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Range(0, 20, ErrorMessage = "Please enter valid Minutes value")]
        [Display(Name = "Minutes")]
        public int Minutes { get; set; }

        [Required]
        [Range(0, 59, ErrorMessage = "Please enter valid Seconds value")]
        [Display(Name = "Seconds")]
        public int Seconds { get; set; }

        public string Genre { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Song, SongViewModel>()
            .ForMember(songViewModel => songViewModel.Name, cfg => cfg.MapFrom(song => song.Name))
            .ForMember(songViewModel => songViewModel.Genre, cfg => cfg.MapFrom(song => song.Genre.Name))
                .ForMember(songViewModel => songViewModel.Minutes, cfg => cfg.MapFrom(song => song.Duration / 60))
                .ForMember(songViewModel => songViewModel.Seconds, cfg => cfg.MapFrom(song => song.Duration % 60));
        }
    }
}