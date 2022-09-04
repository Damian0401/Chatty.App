using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos
{
    public class LoginResponseDto
    {
        public string UserName { get; set; } = default!;
        public string Token { get; set; } = default!;
    }
}