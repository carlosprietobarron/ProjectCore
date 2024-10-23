using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.courses
{
    public class ComentaryDTO
    {
        public Guid ComentaryId { get; set; }
        public required string StudentName { get; set; }
        public int Rating { get; set; }
        public string? ComentaryText { get; set; }
        public Guid CourseId { get; set; }
        
    }
}