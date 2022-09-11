using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos.Account;
using Application.Dtos.Message;
using Application.Dtos.Room;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Rooms;
public class CreateRoom
{
    public class Command : IRequest<ResponseForHub<CreateRoomResponseDto>>
    {
        public string RoomName { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, ResponseForHub<CreateRoomResponseDto>>
    {
        private readonly DataContext _context;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _userAccessor = userAccessor;
            _context = context;
        }

        public async Task<ResponseForHub<CreateRoomResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor
                .GetCurrentlyLoggedUserName();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            if (user is null)
                return ResponseForHub<CreateRoomResponseDto>
                    .Failure(new List<string> { "Access denied" });

            var message = new Message
            {
                Body = $"Welcome to the room: {user.UserName}",
                CreatedAt = DateTime.Now
            };

            var room = new Room
            {
                Name = request.RoomName,
                Messages = new List<Message>{ message }
            };

            var roomApplicationUser = new RoomApplicationUser
            {
                Room = room,
                UserId = user.Id,
                DisplayName = user.UserName,
                IsAdministrator = true,
                JoinDate = DateTime.Now
            };

            _context.RoomApplicationUsers
                .Add(roomApplicationUser);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return ResponseForHub<CreateRoomResponseDto>
                    .Failure(new List<string> { "Unable to create new room" });

            var responseDto = _mapper.Map<CreateRoomResponseDto>(room);
            
            responseDto.Users = new List<UserForCreateRoomResponseDto>
            { 
                _mapper.Map<UserForCreateRoomResponseDto>(roomApplicationUser) 
            };

            return ResponseForHub<CreateRoomResponseDto>
                .Success(responseDto);
        }
    }
}