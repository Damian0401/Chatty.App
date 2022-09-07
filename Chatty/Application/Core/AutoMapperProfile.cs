using Application.Dtos.Account;
using Application.Dtos.Message;
using Application.Dtos.Room;
using AutoMapper;
using Domain.Models;

namespace Application.Core
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            MapsForUser();
            MapsForMessage();
            MapsForRoom();
        }

        private void MapsForUser()
        {
            CreateMap<ApplicationUser, LoginResponseDto>();
            CreateMap<ApplicationUser, RegisterResponseDto>();
            CreateMap<RegisterRequestDto, ApplicationUser>();
            CreateMap<RoomApplicationUser, ApplicationUserDto>()
                .ForMember(x => x.Id, a => 
                    a.MapFrom(s => s.UserId));
        }

        private void MapsForMessage()
        {
            CreateMap<Message, MessageDto>()
                .ForMember(x => x.Body, a => 
                    a.MapFrom(s => s.IsDeleted ? "This message has been deleted" : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
        }

        private void MapsForRoom()
        {
            CreateMap<Room, CreateRoomResponseDto>();
            CreateMap<Room, JoinRoomResponseForCallerDto>();
        }
    }
}