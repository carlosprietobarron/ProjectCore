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
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Application.security
{
    public class Login
    {
        public class ValidationExecute: AbstractValidator<Execute>{
            public ValidationExecute(){
                RuleFor(X => X.Email).NotEmpty();
                RuleFor(X => X.Password).NotEmpty();
            }
        }
        
        public class Execute: IRequest<UserData>{
            //public string UserName { get; set;}
            public string Email { get; set;}
            public string Password { get; set;}
        }

public class Handler : IRequestHandler<Execute, UserData>
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtGenerator _jwtGenerator;

    public Handler(UserManager<User> userManager, SignInManager<User> signInManager, IJwtGenerator jwtGenerator)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtGenerator = jwtGenerator;
    }

    public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user == null)
        {
            throw new ExceptionHandler(HttpStatusCode.Unauthorized, "User not found");
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
        var resultlist = await _userManager.GetRolesAsync(user);
        var roles = new List<string>(resultlist);
    

        if (result.Succeeded)
        {
            // Mapear de User a UserData
            var userData = new UserData
            {
                FullName = user.FullName,
                Email = user.Email,
                UserName = user.UserName,
                Token = _jwtGenerator.CreateToken(user, roles )
                // Puedes añadir más campos si los necesitas
            };
            return userData;
        }

        throw new ExceptionHandler(HttpStatusCode.Unauthorized, "Invalid credentials");
    }
}


        }
}