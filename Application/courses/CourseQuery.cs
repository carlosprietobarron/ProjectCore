using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Dominion;
using Persistence;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Application.courses
{
    public class CourseQuery
    {
        public class CoursesList: IRequest<List<CourseDTO>>{

        }

        public class Handler : IRequestHandler<CoursesList, List<CourseDTO>>
        {
            private readonly CoursesContext _context;
            private readonly IMapper _mapper;
            public Handler(CoursesContext context, IMapper mapper) 
            {
                _context = context;
                _mapper = mapper;
            }

            public async Task<List<CourseDTO>> Handle(CoursesList request, CancellationToken cancellationToken)
            {
                var courses = await _context.Course
                                    .Include(x => x.Comentaries)
                                    .Include(x => x.SalesPrice)
                                    .Include(x => x.TeacherLinks!)
                                    .ThenInclude(l => l.Teacher).ToListAsync();

                var cursoesDTO = _mapper.Map<List<Course>, List<CourseDTO>>(courses);

                return cursoesDTO;
            }
        }

    }
}