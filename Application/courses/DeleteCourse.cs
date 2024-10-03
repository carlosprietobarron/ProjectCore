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
            public int Id { get; set; }
        }


        public class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesContext _context;
            public Handler(CoursesContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
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