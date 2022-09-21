using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos.Room;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Rooms;

public class ExitRoom
{
    public class Command : IRequest<ResponseForHub<ExitRoomResponseDto>>
    {
        public ExitRoomRequestDto Dto { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, ResponseForHub<ExitRoomResponseDto>>
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

        public async Task<ResponseForHub<ExitRoomResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor.GetCurrentlyLoggedUserName();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            if (user is null)
                return ResponseForHub<ExitRoomResponseDto>
                    .Failure(new List<string> { "Access denied" });

            var room = await _context.Rooms
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id.Equals(request.Dto.RoomId));

            if (room is null)
                return ResponseForHub<ExitRoomResponseDto>
                    .Failure(new List<string> { "Access denied" });

            var userToRemove = room.Users
                .FirstOrDefault(x => x.UserId.Equals(request.Dto.UserId));

            if (userToRemove is null)
                return ResponseForHub<ExitRoomResponseDto>
                    .Failure(new List<string> { "User not found" });

            if (!room.Users.Any(x => x.IsAdministrator && x.UserId.Equals(user.Id)) && !user.Id.Equals(userToRemove.UserId))
                return ResponseForHub<ExitRoomResponseDto>
                    .Failure(new List<string> { "Access denied" });

            room.Users.Remove(userToRemove);

            var responseDto = new ExitRoomResponseDto();

            if (room.Users.Count == 0)
            {
                _context.Rooms.Remove(room);
                responseDto.IsRoomDeleted = true;

                var deleteResult = await _context.SaveChangesAsync();
                return deleteResult > 0
                    ? ResponseForHub<ExitRoomResponseDto>.Success(responseDto)
                    : ResponseForHub<ExitRoomResponseDto>.Failure(new List<string> { "Unable to remove user from room" });
            }

            var message = new Message
            {
                RoomId = room.Id,
                CreatedAt = DateTime.Now,
                Body = $"{userToRemove.DisplayName} has leave the room"
            };
            _context.Messages.Add(message);

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return ResponseForHub<ExitRoomResponseDto>
                    .Failure(new List<string> { "Unable to remove user from room"});
            
            var clientsResponse = new ClientsResponseForExitRoomResponseDto
            {
                RoomId = room.Id,
                UserId = request.Dto.UserId,
                Message = _mapper.Map<MessageForExitRoomResponseDto>(message)
            };
            responseDto.ClientsResponse = clientsResponse;

            return ResponseForHub<ExitRoomResponseDto>
                .Success(responseDto);
        }
    }
}