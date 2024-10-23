using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace Persistence.DapperConnection.Teacher
{
    public interface ITeacher
    {
        Task<IList<TeacherModel>> GetTeacherLIST();
        Task<TeacherModel> GetTeacherById();
        Task<int> newTeacher(TeacherModel model);
        Task<int> uodateTEacher(TeacherModel model);
        Task<int> deleteTeacher(TeacherModel model);
    }
}