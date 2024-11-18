using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using Dapper;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Persistence.DapperConnection.Teacher
{
    public class TeacherRepo : ITeacher
    {
        private readonly IFactioryConnection _factoryConnection;

        public TeacherRepo(IFactioryConnection factoryConnection){
            _factoryConnection = factoryConnection;
        }
        public async Task<int> deleteTeacher(Guid id)
        {
            var storeProcedure = "usp_delete_Teacher";
           object? result = null;
           try
           {
             var connection = _factoryConnection.GetConnection();
             result = await connection.ExecuteAsync(storeProcedure, new {
                teacherId = id
             },
                commandType: CommandType.StoredProcedure);

           }
           catch (System.Exception e)
           {
            
            throw new Exception("Error on delketing teacher", e);
           }
           finally
           {
                _factoryConnection.CloseConnection();
           }
           return (int)result;
        }

        public async Task<IList<TeacherModel>> GetTeacherLIST()
        {
            List<TeacherModel> teacherList = null;
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

        public async Task<List<TeacherModel>> GetTeacherLIST_old()
        {
            List<TeacherModel> teacherList = null;
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

        public async Task<int> newTeacher(string FirstName, string LastName, string Level)
        {
           var storeProcedure = "usp_new_Teacher";
           object? result = null;
           try
           {
             var connection = _factoryConnection.GetConnection();
             result = await connection.ExecuteAsync(storeProcedure, new {
                TeacherId = new Guid(),
                FirstName =  FirstName,
                LastName = LastName,
                Level = Level
             }, 
             commandType: CommandType.StoredProcedure);

           }
           catch (System.Exception e)
           {
            
            throw new Exception("Error on creating teacher", e);
           }
           finally
           {
                _factoryConnection.CloseConnection();
           }
           return (int)result;
        }

        public async Task<int> uodateTeacher(Guid TeacherId, string FirstName, string LastName)
        {
           var storeProcedure = "usp_update_Teacher";
           object? result = null;
           try
           {
             var connection = _factoryConnection.GetConnection();
             result = await connection.ExecuteAsync(storeProcedure, new {
                TeacherId = new Guid(),
                FirstName =  FirstName,
                LastName = LastName,
                //Level = Level
             }, 
             commandType: CommandType.StoredProcedure);

           }
           catch (System.Exception e)
           {
            
            throw new Exception("Error on creating teacher", e);
           }
           finally
           {
                _factoryConnection.CloseConnection();
           }
           return (int)result;
        }

        public async Task<TeacherModel > GetTeacherById(Guid id)
        {
            var storeProcedure = "usp_get_Teacher_by_Id"; 

            object? result = null;
           try
           {
             var connection = _factoryConnection.GetConnection();
             result = await connection.ExecuteAsync(storeProcedure, new {
                teacherId = id
             },
                commandType: CommandType.StoredProcedure);

           }
           catch (System.Exception e)
           {
            
            throw new Exception("Teachre not found", e);
           }
           finally
           {
                _factoryConnection.CloseConnection();
           }
           return (TeacherModel)result;

        }
    }
}