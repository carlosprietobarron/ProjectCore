using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.errorHandlers;
using Dominion;
using MediatR;
using Persistence;

namespace Application.courses
{
    public class CourseIdQuery
    {
        public class CourseById: IRequest<Course>{
            private object value;

            public CourseById(object value)
            {
                this.Id = (int)value;
            }

            public int Id { get; set;}
        }

        public class Handler : IRequestHandler<CourseById, Course>
        {
            private readonly CoursesContext _courseContext;

            public Handler(CoursesContext context)
            {
                _courseContext = context;
            }

            public async Task<Course> Handle(CourseById request, CancellationToken cancellationToken)
            {
                var course = await _courseContext.Course.FindAsync(request.Id);

                if (course == null) {
                    //throw new Exception("Course not found, nor deleted");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new {curso = "not found" });    
                }

                return course;
            }
        }

    }
}