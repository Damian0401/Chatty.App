using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Core;
using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.Account;

public class Login
{
    public class Command : IRequest<Response<LoginResponseDto>>
    {
        public LoginRequestDto Dto { get; set; } = default!;
    }

    public class Handler : IRequestHandler<Command, Response<LoginResponseDto>>
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

        public async Task<Response<LoginResponseDto>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = await _userManager
                .FindByEmailAsync(request.Dto.Email);

            if (user is null)
                return new Response<LoginResponseDto>(HttpStatusCode.Unauthorized);

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, request.Dto.Password, false);

            if (!result.Succeeded)
                return new Response<LoginResponseDto>(HttpStatusCode.Unauthorized);

            var responseDto = _mapper.Map<LoginResponseDto>(user);

            responseDto.Token = _jwtGenerator.CreateToken(user);

            return new Response<LoginResponseDto>(HttpStatusCode.OK, responseDto);
        }
    }
}