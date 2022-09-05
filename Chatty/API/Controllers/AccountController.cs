using Application.Account;
using Application.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        public AccountController(IServiceProvider serviceProvider) 
            : base(serviceProvider)
        {
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto dto)
        {
            var response = await Mediator.Send(new Register.Command { Dto = dto});

            return SendResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto dto)
        {
            var response = await Mediator.Send(new Login.Command { Dto = dto});

            return SendResponse(response);
        }
    }
}
