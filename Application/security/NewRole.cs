using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Math.EC.Rfc7748;

namespace Application.security
{
    public class NewRole
    {
        public class Execute: IRequest {
            public string Name { get; set;}
        }

        public class ValidateExecute : AbstractValidator <Execute> {
            public ValidateExecute(){
                RuleFor(x=>x.Name).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Execute>{
            private readonly RoleManager<IdentityRole> _roleManager;
            public Handler(RoleManager<IdentityRole> roleManager){
                _roleManager = roleManager;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var role = await _roleManager.FindByIdAsync(request.Name);
                if (role != null){
                    throw new Exception("Problems creating role");
                }

                var result = await _roleManager.CreateAsync(new IdentityRole(request.Name));

                if (result.Succeeded){
                    return Unit.Value;
                }

                throw new Exception("Role creation failed");
            }
        }
    }
}