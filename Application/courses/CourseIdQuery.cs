using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.errorHandlers;
using AutoMapper;
using Dominion;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace Application.courses
{
    public class CourseIdQuery
    {
        public class CourseById: IRequest<CourseDTO>{
            private object value;

            public CourseById(object value)
            {
                this.Id = (Guid)value;
            }

            public Guid Id { get; set;}
        }

        public class Handler : IRequestHandler<CourseById, CourseDTO>
        {
            private readonly CoursesContext _courseContext;
            private readonly    IMapper _mapper;

            public Handler(CoursesContext context,  IMapper mapper)
            {
                _courseContext = context;
                _mapper = mapper;
            }

            public async Task<CourseDTO> Handle(CourseById request, CancellationToken cancellationToken)
            {
                //var course = await _courseContext.Course.FindAsync(request.Id);
                var course = await _courseContext.Course
                                .Include(x => x.Comentaries)
                                .Include(x => x.SalesPrice)
                                .Include(x => x.TeacherLinks!)
                                .ThenInclude(l => l.Teacher).FirstOrDefaultAsync(x => x.CourseId == request.Id);

                if (course == null) {
                    //throw new Exception("Course not found, nor deleted");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new {curso = "not found" });    
                }
                var courseDTO = _mapper.Map<Course, CourseDTO>(course);
                return courseDTO;
            }
        }

    }
}