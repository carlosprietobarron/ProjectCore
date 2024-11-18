using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.contracts;
using Dominion;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.security
{
    public class CurrentUser
    {
        public class Execute: IRequest<UserData>{

        }

        public class Handler : IRequestHandler<Execute, UserData> {

            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IUserSesion _userSesion;
            public Handler(UserManager<User> userManager, IJwtGenerator jwtGenerator, IUserSesion userSesion){
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _userSesion = userSesion;
            }
            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(_userSesion.GetUserSesion());
                var resultlist = await _userManager.GetRolesAsync(user);
                var roles = new List<string>(resultlist);

                return  new UserData{
                    FullName = user.FullName,
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _jwtGenerator.CreateToken(user, roles)
                };
            }
        }
    }
}
