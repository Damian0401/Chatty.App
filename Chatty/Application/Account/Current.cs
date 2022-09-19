using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos.Account;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account;

public class Current
{
    public record Query : IRequest<ResponseForController<CurrentResponseDto>>;

    public class Handler : IRequestHandler<Query, ResponseForController<CurrentResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserAccessor _userAccessor;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;

        public Handler(UserManager<ApplicationUser> userManager, IUserAccessor userAccessor, 
            IMapper mapper, IJwtGenerator jwtGenerator)
        {
            _mapper = mapper;
            _jwtGenerator = jwtGenerator;
            _userAccessor = userAccessor;
            _userManager = userManager;
        }

        public async Task<ResponseForController<CurrentResponseDto>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userName = _userAccessor.GetCurrentlyLoggedUserName();

            var user = await _userManager.FindByNameAsync(userName);

            if (user is null)
                return new ResponseForController<CurrentResponseDto>(HttpStatusCode.Unauthorized);

            var responseDto = _mapper.Map<CurrentResponseDto>(user);

            responseDto.Token = _jwtGenerator.CreateToken(user);

            return new ResponseForController<CurrentResponseDto>(HttpStatusCode.OK, responseDto);
        }
    }
}