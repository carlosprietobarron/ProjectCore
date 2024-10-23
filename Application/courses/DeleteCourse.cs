using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.errorHandlers;
using MediatR;
using Persistence;

namespace Application.courses
{
    public class DeleteCourse
    {
         public class Execute: IRequest{
            public Guid Id { get; set; }
        }


        public class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesContext _context;
            public Handler(CoursesContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var teachers = _context.CourseTeacher.Where(l => l.CourseId == request.Id).ToList();
                foreach (var teacher in teachers)
                {
                    _context.CourseTeacher.Remove(teacher);
                }

                var comentaries = _context.Comentary.Where(l => l.CourseId == request.Id).ToList();
                foreach (var comentary in comentaries)
                {
                    _context.Comentary.Remove(comentary);
                }

                var price = _context.Price.Where(l => l.CourseId == request.Id).FirstOrDefault();
                if(price != null) _context.Price.Remove(price);

                var course = await _context.Course.FindAsync(request.Id);


                if (course == null) {
                    //throw new Exception("Course not found, nor deleted");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new {curso = "not found" });    
                }

                //delete course
                
                _context.Remove(course);

               var affectedRows = await _context.SaveChangesAsync();

               if (affectedRows > 0) return Unit.Value;

               throw new Exception("Course not deleted");
            }
        }

    }
}