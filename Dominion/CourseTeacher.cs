using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominion
{
    public class CourseTeacher
    {
        public Guid CourseId { get; set; }
         public required Course Course { get; set; }
        public Guid TeacherId { get; set; }
        public required Teacher Teacher { get; set; }
       
    }
}