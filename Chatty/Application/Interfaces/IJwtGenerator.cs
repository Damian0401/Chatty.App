using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(ApplicationUser applicationUser);
    }
}