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

namespace Application.Rooms
{
    public class ChangeDisplayName
    {
        public class Command : IRequest<ResponseForHub<ChangeDisplayNameResponseDto>>
        {
            public ChangeDisplayNameRequestDto Dto { get; set; } = default!;
        }

        public class Handler : IRequestHandler<Command, ResponseForHub<ChangeDisplayNameResponseDto>>
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

            public async Task<ResponseForHub<ChangeDisplayNameResponseDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userName = _userAccessor
                    .GetCurrentlyLoggedUserName();

                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

                if (user is null || !user.Id.Equals(request.Dto.UserId))
                    return ResponseForHub<ChangeDisplayNameResponseDto>
                        .Failure(new List<string> { "Access denied" });

                var roomApplicationUser = await _context.RoomApplicationUsers
                    .FirstOrDefaultAsync(x => x.RoomId.Equals(request.Dto.RoomId) && x.UserId.Equals(request.Dto.UserId));

                if (roomApplicationUser is null)
                    return ResponseForHub<ChangeDisplayNameResponseDto>
                        .Failure(new List<string> { "Access denied" });

                var message = new Message
                {
                    Body = $"{roomApplicationUser.DisplayName} changed name to {request.Dto.DisplayName}",
                    CreatedAt = DateTime.Now,
                    RoomId = request.Dto.RoomId,
                };
                _context.Messages.Add(message);

                roomApplicationUser.DisplayName = request.Dto.DisplayName;
                var result = await _context.SaveChangesAsync();

                if (result == 0)
                    return ResponseForHub<ChangeDisplayNameResponseDto>
                        .Failure(new List<string> { "Unable to join to room" });

                var responseDto = new ChangeDisplayNameResponseDto
                {
                    Id = roomApplicationUser.RoomId,
                    User = _mapper.Map<UserForJoinRoomResponseDto>(roomApplicationUser),
                    Message = _mapper.Map<MessageForJoinRoomResponseDto>(message),
                };

                return ResponseForHub<ChangeDisplayNameResponseDto>
                    .Success(responseDto);
            }
        }
    }
}