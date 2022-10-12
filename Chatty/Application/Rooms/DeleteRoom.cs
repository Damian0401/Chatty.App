using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Core;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.Rooms;

public class DeleteRoom
{
    public class Command : IRequest<ResponseForHub<Unit>>
    {
        public Guid RoomId { get; set; }
    }

    public class Handler : IRequestHandler<Command, ResponseForHub<Unit>>
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

        public async Task<ResponseForHub<Unit>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor.GetCurrentlyLoggedUserName();

            var roomApplicationUser = await _context.RoomApplicationUsers
                .Include(x => x.User)
                .Include(x => x.Room)
                .FirstOrDefaultAsync(x => x.User.UserName.Equals(userName) && x.RoomId.Equals(request.RoomId));

            if (roomApplicationUser is null || !roomApplicationUser.IsAdministrator)
                return ResponseForHub<Unit>
                    .Failure(new List<string> { "Access denied" });

            _context.Remove(roomApplicationUser.Room);

            var result = await _context.SaveChangesAsync();

            return result > 0
                ? ResponseForHub<Unit>.Success(Unit.Value)
                : ResponseForHub<Unit>.Failure(new List<string> { "Unable to delete room" });
        }
    }
}