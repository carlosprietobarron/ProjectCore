using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.errorHandlers;
using Dominion;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.courses
{
    public class UpdateCourse
    {
        public class Execute: IRequest{
            public int CourseId { get; set; }
            public required string Title { get; set; }
            public string? Description { get; set; }
            public DateTime? PublishedDate { get; set; }
            //public byte[]? FrontPhoto  { get; set; }
        }

public class ValidationExecute: AbstractValidator<Execute>{
            public ValidationExecute(){
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.PublishedDate).NotEmpty();
            }
        }
        
        public class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesContext _context;
            public Handler(CoursesContext context){
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var course = await _context.Course.FindAsync(request.CourseId);

                //if (course == null) {
                //    throw new Exception("Course not found");
                //}

                if (course == null) {
                    //throw new Exception("Course not found, nor deleted");
                    throw new ExceptionHandler(HttpStatusCode.NotFound, new {curso = "not found" });    
                }

                //update course
                course.Title = request.Title ?? course.Title;
                course.Description = request.Description ?? course.Description;
                course.PublishedDate = request.PublishedDate ?? course.PublishedDate;
                

               var affectedRows = await _context.SaveChangesAsync();

               if (affectedRows > 0) return Unit.Value;

               throw new Exception("Course not updated");
            }
        }

    }
}