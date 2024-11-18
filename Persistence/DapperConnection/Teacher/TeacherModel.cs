using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.DapperConnection.Teacher
{
    public class TeacherModel
    {
        public Guid TeacherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Level { get; set; }
    }
}