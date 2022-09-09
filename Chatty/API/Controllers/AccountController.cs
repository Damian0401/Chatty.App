using Application.Account;
using Application.Dtos.Account;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator  mediator) 
        {
            _mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var response = await _mediator.Send(new Register.Command { Dto = dto});

            return SendResponse(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var response = await _mediator.Send(new Login.Command { Dto = dto});

            return SendResponse(response);
        }
    }
}
