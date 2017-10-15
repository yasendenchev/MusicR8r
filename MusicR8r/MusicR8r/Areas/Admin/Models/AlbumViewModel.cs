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

        //[RegularExpression("^[A-Z0-9 _]*[A-Z0-9][A-Z0-9 _]*${3,15}", ErrorMessage = "Album name should contain only letters")]
        [Required(ErrorMessage = "Name is required!")]
        [AllowHtml]
        public string Name { get; set; }

        [Required(ErrorMessage = "Year is required!")]
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