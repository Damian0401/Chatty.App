using Application.Account;
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
        public async Task<IActionResult> Register(Register.Command command)
        {
            var response = await Mediator.Send(command);

            return SendResponse(response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(Login.Command command)
        {
            var response = await Mediator.Send(command);

            return SendResponse(response);
        }
    }
}
