using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;

namespace Persistence.DapperConnection.Teacher
{
    public class TeacherRepo : ITeacher
    {
        private readonly IFactioryConnection _factoryConnection;

        public TeacherRepo(IFactioryConnection factoryConnection){
            _factoryConnection = factoryConnection;
        }
        public Task<int> deleteTeacher(TeacherModel model)
        {
            throw new NotImplementedException();
        }

        public Task<TeacherModel> GetTeacherById()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TeacherModel>> GetTeacherLIST()
        {
            IEnumerable<TeacherModel> teacherList = null;
            var storeProcedure = "GetTeachers";
            try
            {
                var connection = _factoryConnection.GetConnection();
                var teachers = await connection.QueryAsync<TeacherModel>(storeProcedure, null, commandType: CommandType.StoredProcedure);
            }
            catch (System.Exception e)
            {
                
                throw new Exception("Error on retrieving data");
            } finally {

            }
            return teacherList;
        }

        public Task<int> newTeacher(TeacherModel model)
        {
            throw new NotImplementedException();
        }

        public Task<int> uodateTEacher(TeacherModel model)
        {
            throw new NotImplementedException();
        }
    }
}