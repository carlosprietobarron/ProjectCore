using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence.DapperConnection.Teacher;

namespace Application.Teachers
{
    public class TeacherQuery
    {
        public class TeacherList : IRequest<IEnumerable<TeacherModel>>{}

        class Handler : IRequestHandler<TeacherList, IEnumerable<TeacherModel>>{
            public readonly ITeacher _teacherrepo;

            public Handler(ITeacher teacherrepo){
                _teacherrepo = teacherrepo;
            }
            public async Task<IEnumerable<TeacherModel>> Handle(TeacherList request, CancellationToken cancellationToken)
            {
                return await _teacherrepo.GetTeacherLIST();
            }
        }
    }
}