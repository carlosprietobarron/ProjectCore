using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dominion;

namespace Application.courses
{
    public class TeacherDTO
    {
        public Guid TeacherId { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public string? Level { get; set; }
        public byte[]? Photos { get; set; }

        public ICollection<Course>? Courses { get; set; }
    }
}