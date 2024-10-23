using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.security
{
    public class UserData
        {
            public required string? FullName { get; set; }
            public required string UserName { get; set; }
            public required string Email { get; set; }
            public string? Token { get; set; }
        }
}
