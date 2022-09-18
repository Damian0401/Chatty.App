using System.Linq;
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
            CreateMap<ApplicationUser, CurrentResponseDto>();
            CreateMap<RoomApplicationUser, UserForJoinRoomResponseDto>()
                .ForMember(x => x.Id, a =>
                    a.MapFrom(s => s.UserId));
            CreateMap<RoomApplicationUser, UserForCreateRoomResponseDto>()
                .ForMember(x => x.Id, a =>
                    a.MapFrom(s => s.UserId));
            CreateMap<RoomApplicationUser, UserForGetRoomDetailsResponseDto>()
                .ForMember(x => x.Id, a =>
                    a.MapFrom(s => s.UserId));
        }

        private void MapsForMessage()
        {
            var deletedMessageBody = "This message has been deleted";

            CreateMap<Message, SendMessageResponseDto>()
                .ForMember(x => x.Body, a =>
                    a.MapFrom(s => s.IsDeleted ? deletedMessageBody : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
            CreateMap<Message, MessageForCreateRoomResponseDto>()
                .ForMember(x => x.Body, a =>
                    a.MapFrom(s => s.IsDeleted ? deletedMessageBody : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
            CreateMap<Message, DeleteMessageResponseDto>()
                .ForMember(x => x.Body, a =>
                    a.MapFrom(s => s.IsDeleted ? deletedMessageBody : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
            CreateMap<Message, MessageForGetRoomDetailsResponseDto>()
                .ForMember(x => x.Body, a =>
                    a.MapFrom(s => s.IsDeleted ? deletedMessageBody : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
            CreateMap<Message, MessageForJoinRoomResponseDto>()
                .ForMember(x => x.Body, a =>
                    a.MapFrom(s => s.IsDeleted ? deletedMessageBody : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
            CreateMap<Message, MessageForConnectToChatResponseDto>()
                .ForMember(x => x.Body, a =>
                    a.MapFrom(s => s.IsDeleted ? deletedMessageBody : s.Body))
                .ForMember(x => x.AuthorId, a => a.Condition(s => !s.IsDeleted));
            CreateMap<SendMessageRequestDto, Message>();
        }

        private void MapsForRoom()
        {
            CreateMap<Room, RoomForConnectToChatResponseDto>()
                .ForMember(x => x.Messages, m => 
                    m.MapFrom(s => s.Messages.OrderByDescending(d => 
                        d.CreatedAt).Take(1)));
            CreateMap<Room, CreateRoomResponseDto>();
            CreateMap<Room, GetRoomDetailsResponseDto>()
                .ForMember(x => x.Messages, m => m.MapFrom(s => 
                    s.Messages.OrderByDescending(d => d.CreatedAt)));
            CreateMap<Room, CallerResponseForJoinRoomResponseDto>();
        }
    }
}