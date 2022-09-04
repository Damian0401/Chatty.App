using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Security
{
    public class AuthenticationSettings
    {
        public string Key { get; set; } = default!;
        public int ExpireDays { get; set; }
    }
}