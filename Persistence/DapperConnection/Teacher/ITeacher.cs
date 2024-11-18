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
        Task<TeacherModel> GetTeacherById(Guid techerId);
        Task<int> newTeacher(string FirstName, string LastName, string Level);
        Task<int> uodateTeacher(Guid TeacherId, string FirstName, string LastNHame);
        Task<int> deleteTeacher(Guid techerId);
    }
}