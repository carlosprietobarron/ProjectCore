using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.security
{
    public class ListRole
    {
        public class Execute: IRequest<List<IdentityRole>>{}

        public class Handler : IRequestHandler<Execute, List<IdentityRole>>
        {
            private readonly CoursesContext _context;

            public Handler(CoursesContext context){
                _context = context;
            }

            public async Task<List<IdentityRole>> Handle(Execute request, CancellationToken cancellationToken)
            {
               var roles = await _context.Roles.ToListAsync();
               return roles;
            }
        }
    }
}