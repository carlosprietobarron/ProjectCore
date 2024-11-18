using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominion;

namespace Application.contracts
{
    public interface IJwtGenerator
    {
        string CreateToken(User user, List<string> roles) ;
    }
}