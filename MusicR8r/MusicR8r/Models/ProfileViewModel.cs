using AutoMapper;
using MusicR8r.Data.Model.Models;
using MusicR8r.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicR8r.Models
{
    public class ProfileViewModel : IMapFrom<User>, ICustomMapping
    {
        public Guid Id { get; set; }

        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Display(Name = "First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [Display(Name = "Bio")]
        public string Bio { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<User, ProfileViewModel>()
                .ForMember(profileViewModel => profileViewModel.Id, cfg => cfg.MapFrom(user => user.Id))
                .ForMember(profileViewModel => profileViewModel.Email, cfg => cfg.MapFrom(user => user.Email))
                .ForMember(profileViewModel => profileViewModel.UserName, cfg => cfg.MapFrom(user => user.UserName))
                .ForMember(profileViewModel => profileViewModel.FirstName, cfg => cfg.MapFrom(user => user.FirstName))
                .ForMember(profileViewModel => profileViewModel.LastName, cfg => cfg.MapFrom(user => user.LastName))
                .ForMember(profileViewModel => profileViewModel.FullName, cfg => cfg.MapFrom(user => String.Format("{0} {1}",user.FirstName, user.LastName)))
                .ForMember(profileViewModel => profileViewModel.Bio, cfg => cfg.MapFrom(user => user.Bio));
        }
    }
}