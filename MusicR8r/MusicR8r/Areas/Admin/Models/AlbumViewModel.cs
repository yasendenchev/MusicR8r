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
    public class AlbumViewModel : IMapFrom<Album>, ICustomMappings
    {
        public Guid Id { get; set; }

        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*${3,15}", ErrorMessage = "The name can contain only letters, digits and whitespaces")]
        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Range(1850, 2020, ErrorMessage = "Please enter valid Year")]
        [Display(Name = "Year")]
        public int Year { get; set; }

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