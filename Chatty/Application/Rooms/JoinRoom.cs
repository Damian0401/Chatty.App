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
using AutoMapper.QueryableExtensions;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Rooms
{
    public class JoinRoom
    {
        public class Command : IRequest<ResponseForHub<JoinRoomResponseDto>>
        {
            public Guid RoomId { get; set; }
        }

        public class Handler : IRequestHandler<Command, ResponseForHub<JoinRoomResponseDto>>
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

            public async Task<ResponseForHub<JoinRoomResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userName = _userAccessor
                    .GetCurrentlyLoggedUserName();

                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

                if (user is null)
                    return ResponseForHub<JoinRoomResponseDto>
                        .Failure(new List<string> { "Access denied" });

                var room = await _context.Rooms
                    .Include(x => x.Messages)
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.RoomId));

                if (room is null)
                    return ResponseForHub<JoinRoomResponseDto>
                        .Failure(new List<string> { "Room not found" });

                if (await _context.RoomApplicationUsers.AnyAsync(x => x.UserId.Equals(user.Id) && x.RoomId.Equals(room.Id)))
                    return ResponseForHub<JoinRoomResponseDto>
                        .Failure(new List<string> { "You already joined to this room" });

                var roomApplicationUser = new RoomApplicationUser
                {
                    Room = room,
                    User = user,
                    DisplayName = user.UserName,
                    IsAdministrator = false,
                    JoinDate = DateTime.Now
                };

                _context.RoomApplicationUsers
                    .Add(roomApplicationUser);

                var message = new Message
                {
                    Body = $"Welcome to the room: {user.UserName}",
                    CreatedAt = DateTime.Now
                };
                room.Messages.Add(message);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                    return ResponseForHub<JoinRoomResponseDto>
                        .Failure(new List<string> { "Unable to join to room" });

                var users = await _context.RoomApplicationUsers
                    .Where(x => x.RoomId.Equals(room.Id))
                    .ProjectTo<UserForJoinRoomResponseDto>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                var callerResponse = _mapper.Map<CallerResponseForJoinRoomResponseDto>(room);
                callerResponse.Users = users;

                var clientsResponse = new ClientsResponseForJoinRoomResponseDto
                {
                    User = _mapper.Map<UserForJoinRoomResponseDto>(roomApplicationUser),
                    Message = _mapper.Map<MessageForJoinRoomResponseDto>(message)
                };

                var responseDto = new JoinRoomResponseDto
                {
                    CallerResponse = callerResponse,
                    ClientsResponse = clientsResponse
                };

                return ResponseForHub<JoinRoomResponseDto>
                    .Success(responseDto);
            }
        }
    }
}