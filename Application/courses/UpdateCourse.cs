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
            public Guid CourseId { get; set; }
            public required string Title { get; set; }
            public string? Description { get; set; }
            public DateTime? PublishedDate { get; set; }
            //public byte[]? FrontPhoto  { get; set; }
            public List<Guid>? TeacherList { get; set; }

            public decimal? Price { get; set; }
            public decimal? Promotion { get; set; }
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

                var PriceToUpdate = _context.Price.Where(x => x.CourseId == course.CourseId).FirstOrDefault();
                if (PriceToUpdate != null) {
                    PriceToUpdate.PromotionalPrice = request.Promotion ?? PriceToUpdate.PromotionalPrice ;
                    PriceToUpdate.SalePrice = request.Price ?? PriceToUpdate.SalePrice;

                } else {
                    var Priceobj = new Price
                {
                    CourseId = course.CourseId,
                    SalePrice = (decimal)request.Price,
                    PromotionalPrice = (decimal)request.Promotion,
                    PriceId = Guid.NewGuid()
                };

                _context.Price.Add(Priceobj);
                }
                
                if (request.TeacherList != null && request.TeacherList.Count > 0) {
                    //delete teacher list
                    var teacherList = _context.CourseTeacher.Where(t => t.CourseId == request.CourseId).ToList();
                    foreach (var id in teacherList) 
                    {
                        _context.CourseTeacher.Remove(id);
                    }
                    //add new teachelist
                    foreach ( var item in request.TeacherList)
                    {
                        var newTeacher = new CourseTeacher{
                            CourseId = request.CourseId,
                            TeacherId = item
                        };
                    }
                }

               var affectedRows = await _context.SaveChangesAsync();

               if (affectedRows > 0) return Unit.Value;

               throw new Exception("Course not updated");
            }
        }

    }
}