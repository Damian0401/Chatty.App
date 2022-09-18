using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos.Room;
using Application.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Rooms;

public class ConnectToChat
{
    public record Query : IRequest<ResponseForHub<ConnectToChatResponseDto>>;

    public class Handler : IRequestHandler<Query, ResponseForHub<ConnectToChatResponseDto>>
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

        public async Task<ResponseForHub<ConnectToChatResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor
                .GetCurrentlyLoggedUserName();

            var user = await _context.Users
                .Include(x => x.Rooms)
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            if (user is null)
                return ResponseForHub<ConnectToChatResponseDto>
                    .Failure(new List<string> { "Access denied" });

            var roomIds = user.Rooms
                .Select(x => x.RoomId)
                .ToList();

            var rooms = await _context.Rooms
                .Where(x => roomIds.Contains(x.Id))
                .ProjectTo<RoomForConnectToChatResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var responseDto = new ConnectToChatResponseDto
            {
                Rooms = rooms
            };

            return ResponseForHub<ConnectToChatResponseDto>
                .Success(responseDto);
        }
    }
}