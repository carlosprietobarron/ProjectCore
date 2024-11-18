using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.contracts;
using Application.errorHandlers;
using Dominion;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.security
{
    public class UserRegister
    {
        public class Execute: IRequest<UserData>{
            public string? FullName { get; set; }
            public required string UserName { get; set; }
            public required string Password { get; set; }
            public required string Email { get; set; }
            //public required string Token { get; set; }
        }

        public class ExecuteValidation: AbstractValidator<Execute>{
            public ExecuteValidation(){
                RuleFor(x => x.UserName).NotEmpty();
                RuleFor(x => x.FullName).NotEmpty();
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute, UserData>
        {
            private readonly CoursesContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator  _jwtGenerator;

            public Handler(CoursesContext context, UserManager<User> userManager, IJwtGenerator jwtGenerator){
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
            }

            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var exists = await _context.Users.Where( x => x.Email == request.Email ).AnyAsync();
                var existUN = await _context.Users.Where( x => x.UserName == request.UserName ).AnyAsync();
                if (exists || existUN) {
                    throw new ExceptionHandler(HttpStatusCode.BadRequest, new {mensaje = "User already exists"} );
                }

                var user = new User{
                    FullName = request.FullName,
                    UserName = request.UserName,
                    Email = request.Email 
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                var resultlist = await _userManager.GetRolesAsync(user);
                var roles = new List<string>(resultlist);

                if (result.Succeeded) {
                        return new UserData
                                {
                                    FullName = user.FullName,
                                    UserName = user.UserName,
                                    Email = user.Email,
                                    Token = _jwtGenerator.CreateToken(user, roles)
                                };
                }

                throw new ExceptionHandler(HttpStatusCode.BadRequest, new {mensaje="Error Creating User"});
            }
        }
    }
}