using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Dominion
{
    public class Course
    {
        public Guid CourseId { get; set; }
        public required string Title { get; set; }
        public string? Description { get; set; }
        public DateTime? PublishedDate { get; set; }
        public byte[]? FrontPhoto  { get; set; }

        public Price? SalesPrice { get; set; }
        public ICollection<Comentary>? Comentaries { get; set; } 
        public ICollection<CourseTeacher>? TeacherLinks { get; set; }
        
    }
}