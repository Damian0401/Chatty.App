using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Account;
using Application.Dtos;
using AutoMapper;
using Domain.Models;

namespace Application.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForUser();
        }

        private void MapsForUser()
        {
            CreateMap<ApplicationUser, LoginResponseDto>();
            CreateMap<ApplicationUser, RegisterResponseDto>();
            CreateMap<Register.Command, ApplicationUser>();
        }
    }
}