using System.Net;
using Application.Core;
using Application.Dtos.Account;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Account;

public class Login
{
    public class Command : IRequest<ResponseForController<LoginResponseDto>>
    {
        public LoginRequestDto Dto { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, ResponseForController<LoginResponseDto>>
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

        public async Task<ResponseForController<LoginResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager
                .FindByEmailAsync(request.Dto.Email);

            if (user is null)
                return new ResponseForController<LoginResponseDto>(HttpStatusCode.BadRequest, new List<string> { "Invalid email or password." });

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Dto.Password, false);

            if (!result.Succeeded)
                return new ResponseForController<LoginResponseDto>(HttpStatusCode.BadRequest, new List<string> { "Invalid email or password." });

            var responseDto = _mapper.Map<LoginResponseDto>(user);

            responseDto.Token = _jwtGenerator.CreateToken(user);

            return new ResponseForController<LoginResponseDto>(HttpStatusCode.OK, responseDto);
        }
    }
}