using Application.Dtos.Account;
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
            CreateMap<RegisterRequestDto, ApplicationUser>();
        }
    }
}