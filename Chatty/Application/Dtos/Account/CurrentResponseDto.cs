using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Account;

public class CurrentResponseDto
{
    public string Id { get; set; } = default!;
    public string UserName { get; set; } = default!;
    public string Token { get; set; } = default!;
}