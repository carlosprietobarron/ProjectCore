using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominion;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.security
{
    public class UserAssignRole
    {
        public class Execute: IRequest {
            public string UserName { get; set; }
            public string RoleName { get; set;}
        }

        public class ValidateExecute : AbstractValidator <Execute> {
            public ValidateExecute(){
                RuleFor(x=>x.UserName).NotEmpty();
                RuleFor(x=>x.RoleName).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Execute>{
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly UserManager<User> _userManager;
            public Handler(UserManager<User> userManager, RoleManager<IdentityRole> roleManager){
                _userManager = userManager;
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByIdAsync(request.RoleName);
                if (role != null){
                    throw new Exception("role does not exists");
                }

                var userId = await _userManager.FindByNameAsync(request.UserName);
                if (userId != null){
                    throw new Exception("user does not exists");
                }

                var result = await _userManager.AddToRoleAsync(userId, request.RoleName);

                if (result.Succeeded){
                    return Unit.Value;
                }

                throw new Exception("Role creation failed");
            }
        }
    }
}