using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominion;
using Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.courses
{
    public class CourseQuery
    {
        public class CoursesList: IRequest<List<Course>>{

        }

        public class Handler : IRequestHandler<CoursesList, List<Course>>
        {
            private readonly CoursesContext _context;
            public Handler(CoursesContext context)
            {
                _context = context;
            }

            public async Task<List<Course>> Handle(CoursesList request, CancellationToken cancellationToken)
            {
                var courses = await _context.Course.ToListAsync();

                return courses;
            }
        }

    }
}