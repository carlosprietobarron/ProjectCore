using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using MediatR;
using Org.BouncyCastle.Bcpg.OpenPgp;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Persistence.DapperConnection.Teacher;

namespace Application.Teachers
{
    public class NewTeacher
    {
        public class Execute: IRequest {
            //public Guid TeacherId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Level { get; set; }
        }

        public class ExecuteValidation : AbstractValidator<Execute>{
            public  ExecuteValidation(){
                RuleFor(x => x.FirstName).NotEmpty();
                RuleFor(x => x.LastName).NotEmpty();
            }
            
            
        }

        public class Handler : IRequestHandler<Execute>
        {
            private readonly ITeacher _teacherRepo;

            public Handler(ITeacher teacherRepo){
                _teacherRepo = teacherRepo;
            }
            public  async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var result = await _teacherRepo.newTeacher(request.FirstName, request.LastName, request.Level);
               if (result > 0){
                return Unit.Value;
               } 
               throw new Exception ("Teacher insetion failed: ");
            }
        }
    }
}