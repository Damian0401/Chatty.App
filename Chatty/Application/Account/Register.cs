using System.Net;
using Application.Core;
using Application.Dtos.Account;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Account;

public class Register
{
    public class Command : IRequest<ResponseForController<RegisterResponseDto>>
    {
        public RegisterRequestDto Dto { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, ResponseForController<RegisterResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IJwtGenerator _jwtGenerator;
        
        public Handler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            IMapper mapper, IJwtGenerator jwtGenerator)
        {
            _jwtGenerator = jwtGenerator;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<ResponseForController<RegisterResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            if (await _userManager.Users.AnyAsync(x => x.Email.Equals(request.Dto.Email)))
                return new ResponseForController<RegisterResponseDto>(HttpStatusCode.BadRequest, new List<string> { "Email is already in use." });

            if (await _userManager.Users.AnyAsync(x => x.UserName.Equals(request.Dto.UserName)))
                return new ResponseForController<RegisterResponseDto>(HttpStatusCode.BadRequest, new List<string> { "UserName is already in use." });

            var user = _mapper
                .Map<ApplicationUser>(request.Dto);

            var result = await _userManager
                .CreateAsync(user, request.Dto.Password);

            if (!result.Succeeded)
            {
                var errors = result
                    .Errors
                    .Select(x => x.Description)
                    .ToList();
                return new ResponseForController<RegisterResponseDto>(HttpStatusCode.BadRequest, errors);
            }

            var responseDto = _mapper.Map<RegisterResponseDto>(user);

            responseDto.Token = _jwtGenerator.CreateToken(user);

            return new ResponseForController<RegisterResponseDto>(HttpStatusCode.OK, responseDto);  
        }
    }
}