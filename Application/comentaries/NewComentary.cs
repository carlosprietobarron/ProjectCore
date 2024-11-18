using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.courses;
using Dominion;
using FluentValidation;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Persistence;

namespace Application.comentaries
{
    public class NewComentary
    {
        public class Execute: IRequest{
            public string Student{ get; set; }
            public int Rating{ get; set; } 
            public string Comentary{ get; set; }
            public Guid CourseId{ get; set; }
        }

        public class ExecureValidator : AbstractValidator<Execute>{
             public ExecureValidator(){
                RuleFor(x => x.Student).NotEmpty();
                RuleFor(x => x.Rating).NotEmpty();
                RuleFor(x => x.Comentary).NotEmpty();
                RuleFor(x => x.CourseId).NotEmpty();
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
                var comentary = new Comentary{
                    ComentaryId = new Guid(),
                    StudentName = request.Student,
                    Rating = request.Rating,
                    ComentaryText = request.Comentary,
                    CourseId = request.CourseId,
                };

                _context.Comentary.Add(comentary);
                var result = await _context.SaveChangesAsync();
                if (result > 0){
                    return Unit.Value;
                }

                throw new Exception("Could not save changes");
            }
        }
    }
    }
