using AutoMapper;
using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MusicR8r.Models
{
    

    public class AlbumViewModel : IMapFrom<Album>, ICustomMappings
    {
        public Guid Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Year")]
        public int Year { get; set; }

        [Display(Name = "Artist")]
        public string ArtistName { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Album, AlbumViewModel>()
               .ForMember(albumViewModel => albumViewModel.Id, cfg => cfg.MapFrom(album => album.Id))
               .ForMember(albumViewModel => albumViewModel.Name, cfg => cfg.MapFrom(album => album.Name))
               .ForMember(albumViewModel => albumViewModel.Year, cfg => cfg.MapFrom(album => album.Year))
               .ForMember(albumViewModel => albumViewModel.ArtistName, cfg => cfg.MapFrom(album => album.Artist.Name));
        }
    }
}