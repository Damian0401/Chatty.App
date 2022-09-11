using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos.Message;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Messages;

public class DeleteMessage
{
    public class Command : IRequest<ResponseForHub<DeleteMessageResponseDto>>
    {
        public Guid MessageId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResponseForHub<DeleteMessageResponseDto>>
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

        public async Task<ResponseForHub<DeleteMessageResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor
                .GetCurrentlyLoggedUserName();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            if (user is null)
                return ResponseForHub<DeleteMessageResponseDto>
                    .Failure(new List<string> { "Access denied" });

            var message = await _context.Messages
                .FindAsync(request.MessageId);

            if (message is null)
                return ResponseForHub<DeleteMessageResponseDto>
                    .Failure(new List<string> { "Message not found" });

            var roomApplicationUser = await _context.RoomApplicationUsers
                .FirstOrDefaultAsync(x => x.UserId.Equals(user.Id)
                    && x.RoomId.Equals(message.RoomId));

            if (roomApplicationUser is null)
                return ResponseForHub<DeleteMessageResponseDto>
                    .Failure(new List<string> { "You do not have access to this room" });


            if (!roomApplicationUser.IsAdministrator && !user.Id.Equals(message.AuthorId))
                return ResponseForHub<DeleteMessageResponseDto>
                    .Failure(new List<string> { "You are not allowed to delete this message" });

            message.IsDeleted = true;

            var result = await _context.SaveChangesAsync();

            if (result == 0)
                return ResponseForHub<DeleteMessageResponseDto>
                    .Failure(new List<string> { "Unable to delete message" });

            var responseDto = _mapper.Map<DeleteMessageResponseDto>(message);

            return ResponseForHub<DeleteMessageResponseDto>
                .Success(responseDto);
        }
    }
}
