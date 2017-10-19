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

        [Required(ErrorMessage = "Name is required")]
        [RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*${3,15}", ErrorMessage = "The name can contain only letters, digits and whitespaces")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Country name is required")]
        [RegularExpression("^[A-Za-z]*${3,15}", ErrorMessage = "The country name can contain only letters")]
        [Display(Name = "Country")]
        public string CountryOfOrigin { get; set; }

        //[RegularExpression("^[A-Za-z0-9 _]*[A-Za-z0-9][A-Za-z0-9 _]*${0, 40}", ErrorMessage = "The bio can contain only letters, digits and whitespaces")]
        [Display(Name = "Bio")]
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