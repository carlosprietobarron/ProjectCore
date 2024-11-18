using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Identity.Client;
using Persistence.DapperConnection.Teacher;

namespace Application.Teachers
{
    public class DeleteTeacher
    {
       public class Execute: IRequest{
        public Guid teacherId { get; set; }

       }

        public class Handler : IRequestHandler<Execute>
        {

            private readonly ITeacher _teacherRepo;
            public Handler(ITeacher teacherRepo){
                _teacherRepo = teacherRepo;
            }
            public async Task<Unit> Handle(Execute request, CancellationToken cancellationToken)
            {
                var result = await _teacherRepo.deleteTeacher(request.teacherId);

                if (result > 0) return Unit.Value;

                throw new Exception("Teacher not deleted ");
            }
        }
    }
}