using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.courses
{
    public class CourseTeacherDTO
    {
        public Guid CourseId { get; set; }
        // public Course? Course { get; set; }
        public Guid TeacherId { get; set; }
        //public Teacher? Teacher { get; set; }
    }
}