using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Account
{
    public class ApplicationUserDto
    {
        public string Id { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        public bool IsAdministrator { get; set; }
    }
}