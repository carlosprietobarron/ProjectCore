using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.security
{
    public class DeleteRole
    {
        public class Execute: IRequest{
            public string Name { get; set; }
        }

         public class ValidateExecute : AbstractValidator<Execute> {
            public ValidateExecute(){
                RuleFor(x=>x.Name).NotEmpty();
            }
         }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly RoleManager<IdentityRole> _roleManager;

            public Handler(RoleManager<IdentityRole> roleManager){
                _roleManager = roleManager;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
               var role = await _roleManager.FindByIdAsync(request.Name);
               if(role == null){
                    throw new Exception("Role not founnd");
               }

               var  result = await _roleManager.DeleteAsync(role);

               if (result.Succeeded){ return Unit.Value; }
               throw new Exception("Role not deleted");
            }
        }
    }

   
}