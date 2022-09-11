using Application.Core;
using Application.Dtos.Room;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Presistance;

namespace Application.Rooms;

public class GetRoomDetails
{
    public class Query : IRequest<ResponseForHub<GetRoomDetailsResponseDto>>
    {
        public Guid RoomId { get; set; }
    }

    public class Handler : IRequestHandler<Query, ResponseForHub<GetRoomDetailsResponseDto>>
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

        public async Task<ResponseForHub<GetRoomDetailsResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor
                .GetCurrentlyLoggedUserName();

            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.UserName.Equals(userName));

            if (user is null)
                return ResponseForHub<GetRoomDetailsResponseDto>
                    .Failure(new List<string> { "Access denied" });

            var room = await _context.Rooms
                .Include(x => x.Messages)
                .Include(x => x.Users)
                .FirstOrDefaultAsync(x => x.Id.Equals(request.RoomId));

            if (room is null)
                return ResponseForHub<GetRoomDetailsResponseDto>
                    .Failure(new List<string> { "Room not found" });

            var responseDto = _mapper.Map<GetRoomDetailsResponseDto>(room);

            return ResponseForHub<GetRoomDetailsResponseDto>
                .Success(responseDto);
        }
    }
}