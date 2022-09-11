using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos.Message;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Messages
{
    public class SendMessage
    {
        public class Command : IRequest<ResponseForHub<MessageDto>>
        {
            public SendMessageRequestDto Dto { get; set; } = default!;
        }

        public class Handler : IRequestHandler<Command, ResponseForHub<MessageDto>>
        {
            private readonly DataContext _context;
            private readonly IUserAccessor _userAccessor;
            private readonly IMapper _mapper;

            public Handler(DataContext context, IUserAccessor userAccessor, IMapper mapper)
            {
                _mapper = mapper;
                _context = context;
                _userAccessor = userAccessor;
            }

            public async Task<ResponseForHub<MessageDto>> Handle(Command request, CancellationToken cancellationToken)
            {
                var userName = _userAccessor
                    .GetCurrentlyLoggedUserName();

                var user = await _context.Users
                    .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

                if (user is null)
                    return ResponseForHub<MessageDto>
                        .Failure(new List<string> { "Access denied" });

                var room = await _context.Rooms
                    .Include(x => x.Messages)
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id.Equals(request.Dto.RoomId));

                if (room is null)
                    return ResponseForHub<MessageDto>
                        .Failure(new List<string> { "Room not found" });

                if (!room.Users.Where(u => u.UserId.Equals(user.Id)).Any())
                    return ResponseForHub<MessageDto>
                        .Failure(new List<string> { "You cannot to this room" });

                var message = _mapper.Map<Message>(request.Dto);
                message.Author = user;
                message.Room = room;
                message.CreatedAt = DateTime.Now;

                _context.Messages.Add(message);

                var result = await _context.SaveChangesAsync();

                if (result == 0)
                    return ResponseForHub<MessageDto>
                        .Failure(new List<string> { "Unable to send message" });

                var responseDto = _mapper.Map<MessageDto>(message);

                return ResponseForHub<MessageDto>
                    .Success(responseDto);
            }
        }
    }
}