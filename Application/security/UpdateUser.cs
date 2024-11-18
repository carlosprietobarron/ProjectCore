using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence;
using Dominion;
using Microsoft.AspNetCore.Identity;
using Application.contracts;
using Application.errorHandlers;
using Microsoft.EntityFrameworkCore;

namespace Application.security
{
    public class UpdateUser
    {
            public class Execute: IRequest<UserData> {
            public string FullName { get; set; }
            public string Email{ get; set; }
            public string Password { get; set; }
            //public byte[]? FrontPhoto  { get; set; }
            public string UserName { get; set; }
        }

            public class ValidationExecute: AbstractValidator<Execute>{
            public ValidationExecute(){
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
                RuleFor(x => x.UserName).NotEmpty();
            }
        }
        
        public class Handler : IRequestHandler<Execute, UserData>
        {
            private readonly CoursesContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly IPasswordHasher<User> _passwordHasher;
            public Handler(CoursesContext context, 
                            UserManager<User> userManager, 
                            IJwtGenerator jwtGenerator,
                            IPasswordHasher<User> passwordHasher){
                _context = context;
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _passwordHasher = passwordHasher;
            }
            public async Task<UserData> Handle(Execute request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByNameAsync(request.UserName);
                //var email = await _userManager.FindByEmailAsync(request.Email);

                if (user == null) throw new ExceptionHandler(HttpStatusCode.NotFound, new {curso = "not found" });
                var result = await _context.Users.Where(u => u.Email == request.Email && u.UserName != request.UserName).AnyAsync();
               
                if (result) throw new ExceptionHandler(HttpStatusCode.InternalServerError, new {message = "Email already in use"});
                //update User
                
                user.FullName = request.FullName;
                user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
                user.Email = request.Email;
                var response = await _userManager.UpdateAsync(user);
                var resultlist = await _userManager.GetRolesAsync(user);
                var roles = new List<string>(resultlist);

               
               if (response.Succeeded) {
                return new UserData{
                    Email = user.Email,
                    UserName = user.UserName,
                    FullName = user.FullName,
                    Token = _jwtGenerator.CreateToken(user, roles)
               };
               }

               throw new Exception("User not updated");
            }
        }

    }
}