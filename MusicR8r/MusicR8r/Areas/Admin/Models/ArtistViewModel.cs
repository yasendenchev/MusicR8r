using AutoMapper;
using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MusicR8r.Areas.Admin.Models
{
    public class ArtistViewModel : IMapFrom<Artist>, ICustomMappings
    {
        public Guid Id { get; set; }

        [RegularExpression("[a-zA0]{3,15}", ErrorMessage = "Album name should contain only letters")]
        [Required(ErrorMessage = "Name is required!")]
        [AllowHtml]
        public string Name { get; set; }

        public string CountryOfOrigin { get; set; }

        public string Bio { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Artist, ArtistViewModel>()
                .ForMember(artistViewModel => artistViewModel.Name, cfg => cfg.MapFrom(artist => artist.Name))
                .ForMember(artistViewModel => artistViewModel.CountryOfOrigin, cfg => cfg.MapFrom(artist => artist.CountryOfOrigin))
                .ForMember(artistViewModel => artistViewModel.Bio, cfg => cfg.MapFrom(artist => artist.Bio));
        }
    }
}