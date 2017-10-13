using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MusicR8r.Models;

namespace MusicR8r.Areas.Admin.Models
{
    public class ArtistViewModel : IMapFrom<Artist>, ICustomMappings
    {
        public Guid Id { get; set; }

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