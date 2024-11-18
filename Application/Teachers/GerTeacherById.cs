using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominion;
using MediatR;
using Persistence.DapperConnection.Teacher;

namespace Application.Teachers
{
    public class GetTeacherById
    {
        public class Execute :IRequest<TeacherModel>{
            public Guid teacherId { get; set;}
        }

        public class Handler : IRequestHandler<Execute, TeacherModel>
        {
            private readonly ITeacher _teacherRepo;
            public Handler(ITeacher teacherRepo){
                _teacherRepo = teacherRepo;
            }

            public Task<TeacherModel> Handle(Execute request, CancellationToken cancellationToken)
            {
                var teacher = _teacherRepo.GetTeacherById(request.teacherId);
                if (teacher == null){
                    throw new Exception("Teacher not found");
                }
                return teacher;
            }
        }
    }
}