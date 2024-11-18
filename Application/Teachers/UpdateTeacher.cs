using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Persistence.DapperConnection.Teacher;

namespace Application.Teachers
{
    public class UpdateTeacher
    {
        public class Execute : IRequest {
            public Guid TeacherId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>{
            public  ExecuteValidation(){
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
                        
        }

        public class Handler: IRequest<Execute> {
            private readonly ITeacher _teacherrepo;

            public Handler(ITeacher teacherrepo) {
                _teacherrepo = teacherrepo;
            }

            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken) {
               var result = await _teacherrepo.uodateTeacher(request.TeacherId, request.FirstName, request.LastName);
               if (result > 0){
                return Unit.Value;
               }   
               throw new Exception("Unexpected result");
            }
                
        }

    }
}