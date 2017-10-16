using AutoMapper;
using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicR8r.Models
{
    public class SongViewModel : IMapFrom<Song>, ICustomMappings
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Minutes")]
        public int Minutes { get; set; }

        [Display(Name = "Seconds")]
        public int Seconds { get; set; }

        [Display(Name = "Duration")]
        public double Duration
        {
            get
            {
                return (double)this.Minutes * 60 + this.Seconds;
            }
        }

        [Display(Name = "Genre")]
        public string Genre { get; set; }

        [Display(Name = "Artist")]
        public string Artist { get; set; }

        [Display(Name = "Album")]
        public string Album { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Song, SongViewModel>()
            .ForMember(songViewModel => songViewModel.Name, cfg => cfg.MapFrom(song => song.Name))
            .ForMember(songViewModel => songViewModel.Genre, cfg => cfg.MapFrom(song => song.Genre.Name))
             .ForMember(songViewModel => songViewModel.Artist, cfg => cfg.MapFrom(song => song.Artist.Name))
             .ForMember(songViewModel => songViewModel.Album, cfg => cfg.MapFrom(song => song.Album.Name))
                .ForMember(songViewModel => songViewModel.Minutes, cfg => cfg.MapFrom(song => song.Duration / 60))
                .ForMember(songViewModel => songViewModel.Seconds, cfg => cfg.MapFrom(song => song.Duration % 60));
        }
    }
}