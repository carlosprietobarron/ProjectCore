using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Dominion;
using FluentValidation;
using MediatR;
using Persistence;

namespace Application.courses
{
    public class NewCourse
    {
        public class Execute: IRequest{
            //parameters 
            // 
            //[Required(ErrorMessage="Title is required")]
            public required string Title { get; set; }
            public string? Description { get; set; }
            public DateTime PublishedDate { get; set; }
            //public byte[]? FrontPhoto  { get; set; }
        }

        public class ValidationExecute: AbstractValidator<Execute>{
            public ValidationExecute(){
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly CoursesContext _context;
            public Handler(CoursesContext context) {
                _context = context;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var course = new Course{
                    Title = request.Title,
                    Description = request.Description,
                    PublishedDate = request.PublishedDate,
                };

                _context.Course.Add(course);
                var affectedRows = await _context.SaveChangesAsync();

                if (affectedRows > 0) return Unit.Value;

                throw new Exception("No Rows were affected");

            }
        }


    }
}